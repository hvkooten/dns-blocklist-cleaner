using System.Diagnostics;
using System.Net;
using Markdig;

namespace DNS_BlockList_Cleaner
{
    public partial class HelpForm : Form
    {
        private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();

        public HelpForm()
        {
            InitializeComponent();
            webHelp.Navigating += WebHelp_Navigating;
        }

        private void HelpForm_Load(object? sender, EventArgs e)
        {
            try
            {
                string helpDir = Path.Combine(AppContext.BaseDirectory, "Help");
                string markdownPath = Path.Combine(helpDir, "hagezi-help.md");
                string markdown = File.ReadAllText(markdownPath);

                string body = Markdown.ToHtml(markdown, Pipeline);
                string html = BuildHtmlDocument(body);

                // Write the rendered page next to the images so relative image
                // references (e.g. blocking-level.png) resolve correctly.
                string htmlPath = Path.Combine(helpDir, "hagezi-help.generated.html");
                File.WriteAllText(htmlPath, html);

                webHelp.Url = new Uri(htmlPath);
            }
            catch (Exception ex)
            {
                webHelp.DocumentText =
                    $"<html><body style='font-family:Segoe UI'>" +
                    $"<p>Could not load the help page:</p>" +
                    $"<pre>{WebUtility.HtmlEncode(ex.Message)}</pre></body></html>";
            }
        }

        private void WebHelp_Navigating(object? sender, WebBrowserNavigatingEventArgs e)
        {
            // Allow the initial local file navigation; open web links (http/https)
            // in the user's default browser instead of the embedded control.
            if (e.Url.IsFile || e.Url.Scheme == "about")
                return;

            e.Cancel = true;
            try
            {
                Process.Start(new ProcessStartInfo(e.Url.AbsoluteUri) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Could not open the link:\n{ex.Message}",
                    "Help", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static string BuildHtmlDocument(string body) =>
            $$"""
            <!DOCTYPE html>
            <html>
            <head>
            <meta charset="utf-8" />
            <meta http-equiv="X-UA-Compatible" content="IE=edge" />
            <style>
                body { font-family: 'Segoe UI', Arial, sans-serif; font-size: 14px;
                       color: #1f1f1f; margin: 16px 24px; line-height: 1.5; }
                h1 { font-size: 22px; } h2 { font-size: 18px; margin-top: 24px; }
                img { max-width: 100%; height: auto; border: 1px solid #d0d0d0;
                      border-radius: 4px; margin: 8px 0; }
                table { border-collapse: collapse; margin: 12px 0; }
                th, td { border: 1px solid #c8c8c8; padding: 6px 10px; text-align: left; }
                th { background: #f0f0f0; }
                blockquote { border-left: 4px solid #4a90d9; margin: 12px 0;
                             padding: 4px 12px; background: #f4f8fc; color: #333; }
                a { color: #0b61c4; }
                code, pre { background: #f4f4f4; border-radius: 3px; padding: 1px 4px; }
            </style>
            </head>
            <body>
            {{body}}
            </body>
            </html>
            """;
    }
}
