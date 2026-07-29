using DNS_BlockList_Cleaner.Models;
using DNS_BlockList_Cleaner.Services;

namespace DNS_BlockList_Cleaner
{
    public partial class MainForm : Form
    {
        private const int MaxDomainsShown = 5000;

        private readonly string _dataDirectory;
        private readonly string _configPath;
        private readonly string _statePath;
        private readonly HaGeZiUpdateService _updateService = new();

        private AppConfig _config = new();
        private AppState _state = new();
        private BlocklistDocument? _document;
        private bool _suppressItemCheck;
        private int _editingIndex = -1;

        public MainForm()
        {
            InitializeComponent();

            _dataDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "DNS-BlockList-Cleaner");
            _configPath = Path.Combine(_dataDirectory, "config.json");
            _statePath = Path.Combine(_dataDirectory, "state.json");
        }

        private async void MainForm_Load(object? sender, EventArgs e)
        {
            cmbMatchType.DataSource = Enum.GetValues<MatchType>();

            try
            {
                Directory.CreateDirectory(_dataDirectory);
                _config = await ConfigService.LoadAsync(_configPath).ConfigureAwait(true);
                _state = await StateService.LoadAsync(_statePath).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                Log($"Could not load saved settings: {ex.Message}");
            }

            txtSourceUrl.Text = _config.SourceUrl;
            txtOutputFile.Text = _config.OutputFileName;
            RefreshRules();

            if (!string.IsNullOrEmpty(_state.LastProcessedVersion))
                Log($"Last processed version: {_state.LastProcessedVersion} " +
                    $"({_state.PreviousDomains.Count} domains remembered).");
        }

        private async void btnDownload_Click(object? sender, EventArgs e)
        {
            string url = txtSourceUrl.Text.Trim();
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show(this, "Please enter a source URL first.",
                    "Download", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await RunBusyAsync(async () =>
            {
                Log($"Downloading {url} ...");
                var document = await _updateService.DownloadAsync(url)
                    .ConfigureAwait(true);
                SetDocument(document);
                Log($"Downloaded {document.Domains.Count} domains.");
            });
        }

        private async void btnLoadFile_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialog = new()
            {
                Title = "Select a blocklist file",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            await RunBusyAsync(async () =>
            {
                Log($"Loading {dialog.FileName} ...");
                var document = await BlocklistService
                    .LoadAsync(dialog.FileName).ConfigureAwait(true);
                SetDocument(document);
                Log($"Loaded {document.Domains.Count} domains.");
            });
        }

        private void btnAddRule_Click(object? sender, EventArgs e)
        {
            string pattern = txtPattern.Text.Trim();
            if (string.IsNullOrWhiteSpace(pattern))
            {
                MessageBox.Show(this, "Please enter a pattern.",
                    "Add rule", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MatchType matchType = cmbMatchType.SelectedItem is MatchType mt
                ? mt : MatchType.Contains;

            bool exists = _config.RemovalRules
                .Where((_, i) => i != _editingIndex)
                .Any(r =>
                    string.Equals(r.Pattern, pattern, StringComparison.OrdinalIgnoreCase)
                    && r.MatchType == matchType);

            if (exists)
            {
                Log("Rule already exists.");
                return;
            }

            if (_editingIndex >= 0 && _editingIndex < _config.RemovalRules.Count)
            {
                RemovalRule original = _config.RemovalRules[_editingIndex];
                _config.RemovalRules[_editingIndex] = original with
                {
                    Pattern = pattern,
                    MatchType = matchType
                };
                Log($"Updated rule: {pattern} ({matchType}).");
                EndEdit();
                RefreshRules();
                SaveConfig();
                return;
            }

            _config.RemovalRules.Add(new RemovalRule
            {
                Pattern = pattern,
                MatchType = matchType
            });

            txtPattern.Clear();
            RefreshRules();
            SaveConfig();
            Log($"Added rule: {pattern} ({matchType}).");
        }

        private void btnEditRule_Click(object? sender, EventArgs e)
        {
            if (lstRules.SelectedIndex < 0
                || lstRules.SelectedIndex >= _config.RemovalRules.Count)
                return;

            _editingIndex = lstRules.SelectedIndex;
            RemovalRule rule = _config.RemovalRules[_editingIndex];
            txtPattern.Text = rule.Pattern;
            cmbMatchType.SelectedItem = rule.MatchType;
            btnAddRule.Text = "Update";
            txtPattern.Focus();
        }

        private void EndEdit()
        {
            _editingIndex = -1;
            btnAddRule.Text = "Add";
            txtPattern.Clear();
        }

        private void btnRemoveRule_Click(object? sender, EventArgs e)
        {
            if (lstRules.SelectedIndex < 0
                || lstRules.SelectedIndex >= _config.RemovalRules.Count)
                return;

            var rule = _config.RemovalRules[lstRules.SelectedIndex];
            _config.RemovalRules.RemoveAt(lstRules.SelectedIndex);
            EndEdit();
            RefreshRules();
            SaveConfig();
            Log($"Removed rule: {rule.Pattern} ({rule.MatchType}).");
        }

        private void btnPreview_Click(object? sender, EventArgs e)
        {
            if (_document is null)
            {
                MessageBox.Show(this, "Load or download a blocklist first.",
                    "Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<string> matches = BlocklistService.FindMatches(
                _document.Domains, _config.RemovalRules);

            ShowDomains(matches, $"Matched by rules: {matches.Count}");
            Log($"{matches.Count} domain(s) would be removed by the current rules.");
        }

        private async void btnGenerate_Click(object? sender, EventArgs e)
        {
            if (_document is null)
            {
                MessageBox.Show(this, "Load or download a blocklist first.",
                    "Generate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string fileName = txtOutputFile.Text.Trim();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                MessageBox.Show(this, "Please enter an output file name.",
                    "Generate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog dialog = new()
            {
                Title = "Save cleaned blocklist",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FileName = fileName
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            await RunBusyAsync(async () =>
            {
                List<string> kept = BlocklistService.ApplyRemovals(
                    _document, _config.RemovalRules);

                await BlocklistService.WriteAsync(dialog.FileName, kept)
                    .ConfigureAwait(true);

                int removed = _document.Domains.Count - kept.Count;
                Log($"Wrote {kept.Count} domains to {dialog.FileName} " +
                    $"({removed} removed).");

                _state = _state with
                {
                    LastProcessedVersion = _document.Version,
                    LastProcessedModified = _document.LastModified,
                    PreviousDomains = [.. _document.Domains]
                };

                _config = _config with { OutputFileName = fileName };
                await ConfigService.SaveAsync(_configPath, _config).ConfigureAwait(true);
                await StateService.SaveAsync(_statePath, _state).ConfigureAwait(true);
            });
        }

        private void txtDomainFilter_TextChanged(object? sender, EventArgs e)
        {
            if (_document is null)
                return;

            string filter = txtDomainFilter.Text.Trim();
            IEnumerable<string> source;
            if (string.IsNullOrEmpty(filter))
            {
                source = _document.Domains;
            }
            else if (filter.Contains('*') || filter.Contains('?'))
            {
                source = _document.Domains.Where(d =>
                    DomainMatcher.Matches(d, filter, MatchType.Wildcard));
            }
            else
            {
                source = _document.Domains.Where(d =>
                    d.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            ShowDomains(source.ToList(), null);
        }

        private void SetDocument(BlocklistDocument document)
        {
            _document = document;

            string version = string.IsNullOrEmpty(document.Version)
                ? "unknown" : document.Version;
            string modified = string.IsNullOrEmpty(document.LastModified)
                ? "unknown" : document.LastModified;

            lblInfo.Text = $"Version: {version}   |   Last modified: {modified}   " +
                $"|   {document.Domains.Count} domains";

            if (!string.IsNullOrEmpty(_state.LastProcessedVersion)
                && !string.Equals(_state.LastProcessedVersion, document.Version,
                    StringComparison.OrdinalIgnoreCase))
            {
                var previous = new HashSet<string>(
                    _state.PreviousDomains, StringComparer.OrdinalIgnoreCase);
                int added = document.Domains.Count(d => !previous.Contains(d));
                int removedFromSource = _state.PreviousDomains
                    .Count(d => !document.DomainSet.Contains(d));
                Log($"Changes since {_state.LastProcessedVersion}: " +
                    $"+{added} new, -{removedFromSource} gone.");
            }

            txtDomainFilter.Clear();
            ShowDomains([.. document.Domains], null);
        }

        private void ShowDomains(List<string> domains, string? label)
        {
            lstDomains.BeginUpdate();
            lstDomains.Items.Clear();

            IEnumerable<string> shown = domains.Take(MaxDomainsShown);
            lstDomains.Items.AddRange([.. shown.Cast<object>()]);
            lstDomains.EndUpdate();

            string suffix = domains.Count > MaxDomainsShown
                ? $" (showing first {MaxDomainsShown})" : string.Empty;
            lblDomainsInfo.Text = label is null
                ? $"Domains: {domains.Count}{suffix}"
                : $"{label}{suffix}";
        }

        private void RefreshRules()
        {
            _suppressItemCheck = true;
            lstRules.BeginUpdate();
            lstRules.Items.Clear();
            foreach (RemovalRule rule in _config.RemovalRules)
            {
                int index = lstRules.Items.Add($"{rule.Pattern}  [{rule.MatchType}]");
                lstRules.SetItemChecked(index, rule.Enabled);
            }
            lstRules.EndUpdate();
            _suppressItemCheck = false;
        }

        private void lstRules_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            if (_suppressItemCheck
                || e.Index < 0 || e.Index >= _config.RemovalRules.Count)
                return;

            bool enabled = e.NewValue == CheckState.Checked;
            RemovalRule rule = _config.RemovalRules[e.Index];
            _config.RemovalRules[e.Index] = rule with { Enabled = enabled };
            SaveConfig();
            Log($"{(enabled ? "Enabled" : "Disabled")} rule: {rule.Pattern} ({rule.MatchType}).");
        }

        private async Task RunBusyAsync(Func<Task> action)
        {
            UseWaitCursor = true;
            Enabled = false;
            try
            {
                await action().ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
                MessageBox.Show(this, ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
                UseWaitCursor = false;
            }
        }

        private async void SaveConfig()
        {
            try
            {
                await ConfigService.SaveAsync(_configPath, _config).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                Log($"Could not save config: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            txtLog.AppendText($"{DateTime.Now:HH:mm:ss}  {message}{Environment.NewLine}");
        }

        private void btnAbout_Click(object? sender, EventArgs e)
        {
            using AboutForm about = new();
            about.ShowDialog(this);
        }
    }
}
