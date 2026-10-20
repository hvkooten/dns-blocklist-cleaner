namespace DNS_BlockList_Cleaner
{
    /// <summary>
    /// Lets the user review and remove domains that are excluded from removal.
    /// </summary>
    public partial class ExclusionsForm : Form
    {
        public ExclusionsForm(IEnumerable<string> excludedDomains)
        {
            ArgumentNullException.ThrowIfNull(excludedDomains);

            InitializeComponent();

            foreach (string domain in excludedDomains.OrderBy(d => d, StringComparer.OrdinalIgnoreCase))
                lstExclusions.Items.Add(domain);

            UpdateInfo();
        }

        /// <summary>The domains that remain excluded after the dialog is closed.</summary>
        public IReadOnlyList<string> ExcludedDomains =>
            [.. lstExclusions.Items.Cast<string>()];

        private void btnRemove_Click(object? sender, EventArgs e)
        {
            List<string> selected = [.. lstExclusions.SelectedItems.Cast<string>()];
            foreach (string domain in selected)
                lstExclusions.Items.Remove(domain);

            UpdateInfo();
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            if (lstExclusions.Items.Count == 0)
                return;

            DialogResult confirm = MessageBox.Show(this,
                "Remove all excluded domains? They will be subject to the removal rules again.",
                "Clear exclusions", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            lstExclusions.Items.Clear();
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            lblInfo.Text = $"{lstExclusions.Items.Count} excluded domain(s).";
            btnRemove.Enabled = lstExclusions.SelectedItems.Count > 0;
            btnClear.Enabled = lstExclusions.Items.Count > 0;
        }

        private void lstExclusions_SelectedIndexChanged(object? sender, EventArgs e)
            => btnRemove.Enabled = lstExclusions.SelectedItems.Count > 0;
    }
}
