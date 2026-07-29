namespace DNS_BlockList_Cleaner
{
    partial class HelpForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webHelp = new WebBrowser();
            SuspendLayout();
            // 
            // webHelp
            // 
            webHelp.Dock = DockStyle.Fill;
            webHelp.Location = new Point(0, 0);
            webHelp.MinimumSize = new Size(20, 20);
            webHelp.Name = "webHelp";
            webHelp.Size = new Size(784, 661);
            webHelp.TabIndex = 0;
            // 
            // HelpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 661);
            Controls.Add(webHelp);
            MinimumSize = new Size(500, 400);
            Name = "HelpForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Help - HaGeZi DNS Blocklists";
            Load += HelpForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private WebBrowser webHelp;
    }
}
