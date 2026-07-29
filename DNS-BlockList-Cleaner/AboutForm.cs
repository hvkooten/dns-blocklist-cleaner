using System.Diagnostics;
using System.Reflection;

namespace DNS_BlockList_Cleaner
{
    public partial class AboutForm : Form
    {
        private const string WebsiteUrl = "https://www.nodorumsolutio.com";
        private const string LicenseUrl = "https://www.gnu.org/licenses/gpl-3.0.html";

        public AboutForm()
        {
            InitializeComponent();

            lblProduct.Text = "DNS-BlockList-Cleaner";
            lblVersion.Text = $"Version {GetVersion()}";
            lblCopyright.Text = $"Copyright \u00A9 {DateTime.Now.Year} Nodorum Solutio";
        }

        private static string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string? informational = assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                .InformationalVersion;

            if (!string.IsNullOrWhiteSpace(informational))
            {
                int plusIndex = informational.IndexOf('+');
                return plusIndex >= 0 ? informational[..plusIndex] : informational;
            }

            Version? version = assembly.GetName().Version;
            return version?.ToString() ?? "1.0.0";
        }

        private void lnkWebsite_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
            => OpenUrl(WebsiteUrl);

        private void lnkLicense_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
            => OpenUrl(LicenseUrl);

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                // Opening the default browser is a convenience; surface the reason
                // instead of crashing the dialog when no handler is registered.
                MessageBox.Show(this, $"Could not open the link:\n{ex.Message}",
                    "About", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
