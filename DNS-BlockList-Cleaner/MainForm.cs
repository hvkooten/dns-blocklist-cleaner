using DNS_BlockList_Cleaner.Models;
using DNS_BlockList_Cleaner.Services;
using System.Diagnostics;

namespace DNS_BlockList_Cleaner
{
    public partial class MainForm : Form
    {
        private const int MaxDomainsShown = 5000;

        private const string HaGeZiWildcardBase =
            "https://raw.githubusercontent.com/hagezi/dns-blocklists/main/wildcard/";

        private static readonly BlocklistPreset[] HaGeZiPresets =
        [
            new("Light (lite) - minimal blocking", $"{HaGeZiWildcardBase}light-onlydomains.txt"),
            new("Multi NORMAL (pro) - recommended", $"{HaGeZiWildcardBase}pro-onlydomains.txt"),
            new("Multi PLUS (pro++) - aggressive", $"{HaGeZiWildcardBase}pro.plus-onlydomains.txt"),
            new("Multi ULTIMATE - maximum blocking", $"{HaGeZiWildcardBase}ultimate-onlydomains.txt"),
            new("Fake - anti fraud/phishing", $"{HaGeZiWildcardBase}fake-onlydomains.txt"),
        ];

        private readonly string _dataDirectory;
        private readonly string _configPath;
        private readonly string _statePath;
        private readonly HaGeZiUpdateService _updateService = new();
        private readonly AppUpdateService _appUpdateService = new();

        private AppConfig _config = new();
        private AppState _state = new();
        private BlocklistDocument? _document;
        private bool _suppressItemCheck;
        private bool _suppressPresetChange;
        private int _editingIndex = -1;
        private bool _previewActive;
        private bool _suppressAutoUpdateChange;
        private int _lastLoggedProgress = -1;

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
            InitializePresets();
            RefreshRules();

            if (!string.IsNullOrEmpty(_state.LastProcessedVersion))
                Log($"Last processed version: {_state.LastProcessedVersion} " +
                    $"({_state.PreviousDomains.Count} domains remembered).");

            _suppressAutoUpdateChange = true;
            chkAutoUpdate.Checked = _config.CheckForUpdatesOnStartup;
            _suppressAutoUpdateChange = false;

            if (_config.CheckForUpdatesOnStartup)
                await CheckForUpdatesAsync(silent: true).ConfigureAwait(true);
        }

        private void chkAutoUpdate_CheckedChanged(object? sender, EventArgs e)
        {
            if (_suppressAutoUpdateChange)
                return;

            _config = _config with { CheckForUpdatesOnStartup = chkAutoUpdate.Checked };
            SaveConfig();
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

        private void txtPattern_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '*')
                cmbMatchType.SelectedItem = MatchType.Wildcard;
        }

        private void lstDomains_DoubleClick(object? sender, EventArgs e)
        {
            if (lstDomains.SelectedItem is not string domain || string.IsNullOrWhiteSpace(domain))
                return;

            txtPattern.Text = domain;
            txtPattern.Focus();
        }

        private void btnCopyToPattern_Click(object? sender, EventArgs e)
        {
            txtPattern.Text = txtDomainFilter.Text;
        }

        private void btnCopyToFilter_Click(object? sender, EventArgs e)
        {
            txtDomainFilter.Text = txtPattern.Text;
        }

        private void btnEditRule_Click(object? sender, EventArgs e)
        {
            if (_editingIndex >= 0)
            {
                EndEdit();
                return;
            }

            if (lstRules.SelectedIndex < 0
                || lstRules.SelectedIndex >= _config.RemovalRules.Count)
                return;

            BeginEditRule(lstRules.SelectedIndex);
        }

        private void lstRules_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            int index = lstRules.IndexFromPoint(e.Location);
            if (index < 0 || index >= _config.RemovalRules.Count)
                return;

            if (index == _editingIndex)
            {
                EndEdit();
                return;
            }

            lstRules.SelectedIndex = index;
            BeginEditRule(index);
        }

        private void BeginEditRule(int index)
        {
            _editingIndex = index;
            RemovalRule rule = _config.RemovalRules[index];
            txtPattern.Text = rule.Pattern;
            cmbMatchType.SelectedItem = rule.MatchType;
            btnAddRule.Text = "Update";
            btnEditRule.Text = "Cancel edit";
            txtPattern.Focus();
        }

        private void EndEdit()
        {
            _editingIndex = -1;
            btnAddRule.Text = "Add";
            btnEditRule.Text = "Edit";
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

            if (_previewActive)
            {
                ExitPreview();
                return;
            }

            _previewActive = true;
            btnPreview.Text = "Back to filter";
            ShowPreview();
        }

        private void ShowPreview()
        {
            if (_document is null)
                return;

            List<string> matches = BlocklistService.FindMatches(
                _document.Domains, _config.RemovalRules, _config.ExcludedDomains);

            string label = $"Matched by rules: {matches.Count}";
            if (_config.ExcludedDomains.Count > 0)
                label += $"  |  Excluded: {_config.ExcludedDomains.Count}";

            ShowDomains(matches, label);
            Log($"{matches.Count} domain(s) would be removed by the current rules.");
        }

        private void ctxDomains_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (lstDomains.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            HashSet<string> excluded = new(_config.ExcludedDomains, StringComparer.OrdinalIgnoreCase);
            List<string> selected = [.. lstDomains.SelectedItems.Cast<string>()];

            miExclude.Enabled = selected.Any(d => !excluded.Contains(d));
            miInclude.Enabled = selected.Any(excluded.Contains);
        }

        private void miExclude_Click(object? sender, EventArgs e)
        {
            List<string> selected = [.. lstDomains.SelectedItems.Cast<string>()];
            if (selected.Count == 0)
                return;

            HashSet<string> excluded = new(_config.ExcludedDomains, StringComparer.OrdinalIgnoreCase);
            int added = selected.Count(excluded.Add);
            if (added == 0)
                return;

            _config = _config with { ExcludedDomains = [.. excluded] };
            SaveConfig();
            Log($"Excluded {added} domain(s) from removal.");

            lstDomains.Invalidate();
            if (_previewActive)
                ShowPreview();
        }

        private void miInclude_Click(object? sender, EventArgs e)
        {
            List<string> selected = [.. lstDomains.SelectedItems.Cast<string>()];
            if (selected.Count == 0)
                return;

            HashSet<string> excluded = new(_config.ExcludedDomains, StringComparer.OrdinalIgnoreCase);
            int removed = selected.Count(excluded.Remove);
            if (removed == 0)
                return;

            _config = _config with { ExcludedDomains = [.. excluded] };
            SaveConfig();
            Log($"Removed {removed} domain(s) from exclusions.");

            lstDomains.Invalidate();
            if (_previewActive)
                ShowPreview();
        }

        private void lstDomains_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            e.DrawBackground();

            string domain = lstDomains.Items[e.Index]?.ToString() ?? string.Empty;
            bool isExcluded = _config.ExcludedDomains.Contains(domain, StringComparer.OrdinalIgnoreCase);
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            Color foreColor = isExcluded
                ? (isSelected ? Color.Gold : Color.DarkOrange)
                : e.ForeColor;

            using Font font = isExcluded
                ? new Font(e.Font!, FontStyle.Strikeout)
                : new Font(e.Font!, FontStyle.Regular);

            TextRenderer.DrawText(e.Graphics, domain, font, e.Bounds, foreColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            e.DrawFocusRectangle();
        }

        private void btnManageExclusions_Click(object? sender, EventArgs e)
        {
            using ExclusionsForm dialog = new(_config.ExcludedDomains);
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            List<string> remaining = [.. dialog.ExcludedDomains];
            if (remaining.Count == _config.ExcludedDomains.Count &&
                remaining.All(d => _config.ExcludedDomains.Contains(d, StringComparer.OrdinalIgnoreCase)))
            {
                return;
            }

            int removed = _config.ExcludedDomains.Count - remaining.Count;
            _config = _config with { ExcludedDomains = remaining };
            SaveConfig();
            Log($"Removed {removed} domain(s) from exclusions.");

            lstDomains.Invalidate();
            if (_previewActive)
                ShowPreview();
        }

        private void ExitPreview()
        {
            _previewActive = false;
            btnPreview.Text = "Preview";
            ApplyDomainFilter();
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
                    _document, _config.RemovalRules, _config.ExcludedDomains);

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
            if (_previewActive)
            {
                _previewActive = false;
                btnPreview.Text = "Preview";
            }

            ApplyDomainFilter();
        }

        private void ApplyDomainFilter()
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

        private void btnHelp_Click(object? sender, EventArgs e)
        {
            using HelpForm help = new();
            help.ShowDialog(this);
        }

        private async void btnCheckUpdate_Click(object? sender, EventArgs e)
        {
            await CheckForUpdatesAsync(silent: false).ConfigureAwait(true);
        }

        private async Task CheckForUpdatesAsync(bool silent)
        {
            btnCheckUpdate.Enabled = false;
            try
            {
                Log("Checking for updates...");

                AppRelease? release = await _appUpdateService
                    .GetLatestReleaseAsync()
                    .ConfigureAwait(true);

                if (release is null)
                {
                    Log("Could not determine the latest release.");
                    if (!silent)
                        MessageBox.Show(this,
                            "Could not determine the latest release. Please try again later.",
                            "Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Version current = AppUpdateService.GetCurrentVersion();
                if (release.Version <= current)
                {
                    Log($"You are running the latest version ({current}).");
                    if (!silent)
                        MessageBox.Show(this,
                            $"You already have the latest version ({current}).",
                            "Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Log($"A newer version is available: {release.Version} (current: {current}).");

                DialogResult choice = MessageBox.Show(this,
                    $"A newer version is available.\n\n" +
                    $"Current version: {current}\n" +
                    $"New version: {release.Version}\n\n" +
                    "Download and install it now? The application will restart.",
                    "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (choice != DialogResult.Yes)
                    return;

                await DownloadAndInstallAsync(release).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                Log($"Update check failed: {ex.Message}");
                if (!silent)
                    MessageBox.Show(this,
                        $"The update could not be completed:\n{ex.Message}",
                        "Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckUpdate.Enabled = true;
            }
        }

        private async Task DownloadAndInstallAsync(AppRelease release)
        {
            string? currentExe = Environment.ProcessPath;
            if (string.IsNullOrWhiteSpace(currentExe))
            {
                MessageBox.Show(this,
                    "The running executable could not be located, so the update cannot be installed automatically.",
                    "Update available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string directory = Path.GetDirectoryName(currentExe)!;
            string newExe = Path.Combine(directory, release.AssetName);

            Log($"Downloading {release.AssetName}...");

            Progress<double> progress = new(ReportDownloadProgress);
            prgUpdate.Value = 0;
            prgUpdate.Style = ProgressBarStyle.Blocks;
            prgUpdate.Visible = true;
            _lastLoggedProgress = -1;
            try
            {
                await _appUpdateService.DownloadAsync(release, newExe, progress).ConfigureAwait(true);
            }
            finally
            {
                prgUpdate.Visible = false;
            }

            Log("Download complete. Restarting to apply the update...");

            LaunchUpdater(currentExe, newExe);
            Application.Exit();
        }

        private void ReportDownloadProgress(double fraction)
        {
            if (fraction < 0)
            {
                // Content length was unknown; show an indeterminate animation instead.
                prgUpdate.Style = ProgressBarStyle.Marquee;
                return;
            }

            prgUpdate.Style = ProgressBarStyle.Blocks;
            int percent = Math.Clamp((int)(fraction * 100), 0, 100);
            prgUpdate.Value = percent;

            // Log every 10% milestone (and 100%) to keep the log readable.
            int bucket = percent / 10;
            if (bucket > _lastLoggedProgress / 10 || percent == 100)
            {
                Log($"Downloading update... {percent}%");
                _lastLoggedProgress = percent;
            }
        }

        private static void LaunchUpdater(string oldExe, string newExe)
        {
            string scriptPath = Path.Combine(Path.GetTempPath(),
                $"dns-blocklist-cleaner-update-{Guid.NewGuid():N}.cmd");

            bool sameFile = string.Equals(
                Path.GetFullPath(oldExe), Path.GetFullPath(newExe), StringComparison.OrdinalIgnoreCase);

            // Wait for this process to release its file lock, remove the old executable
            // (unless the new download reused the same path), then start the new version.
            string script;
            if (sameFile)
            {
                script =
                    "@echo off" + Environment.NewLine +
                    "timeout /t 2 /nobreak >nul" + Environment.NewLine +
                    $"start \"\" \"{newExe}\"" + Environment.NewLine +
                    "del \"%~f0\" >nul 2>&1" + Environment.NewLine;
            }
            else
            {
                script =
                    "@echo off" + Environment.NewLine +
                    ":wait" + Environment.NewLine +
                    "timeout /t 1 /nobreak >nul" + Environment.NewLine +
                    $"del \"{oldExe}\" >nul 2>&1" + Environment.NewLine +
                    $"if exist \"{oldExe}\" goto wait" + Environment.NewLine +
                    $"start \"\" \"{newExe}\"" + Environment.NewLine +
                    "del \"%~f0\" >nul 2>&1" + Environment.NewLine;
            }

            File.WriteAllText(scriptPath, script);

            ProcessStartInfo startInfo = new()
            {
                FileName = "cmd.exe",
                Arguments = $"/c \"{scriptPath}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            Process.Start(startInfo);
        }

        private void InitializePresets()
        {
            _suppressPresetChange = true;
            cmbPreset.DisplayMember = nameof(BlocklistPreset.Name);
            cmbPreset.DataSource = HaGeZiPresets;

            int match = Array.FindIndex(
                HaGeZiPresets,
                p => string.Equals(p.Url, txtSourceUrl.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase));
            cmbPreset.SelectedIndex = match >= 0 ? match : -1;
            _suppressPresetChange = false;
        }

        private void cmbPreset_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_suppressPresetChange
                || cmbPreset.SelectedItem is not BlocklistPreset preset)
                return;

            txtSourceUrl.Text = preset.Url;
            _config = _config with { SourceUrl = preset.Url };
            SaveConfig();
            Log($"Selected HaGeZi list: {preset.Name}.");
        }

        private sealed record BlocklistPreset(string Name, string Url);
    }
}
