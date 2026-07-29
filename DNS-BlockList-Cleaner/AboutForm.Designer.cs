namespace DNS_BlockList_Cleaner
{
    partial class AboutForm
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
            lblProduct = new Label();
            lblVersion = new Label();
            lblCopyright = new Label();
            lnkWebsite = new LinkLabel();
            lblLicenseIntro = new Label();
            lnkLicense = new LinkLabel();
            btnOk = new Button();
            SuspendLayout();
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = true;
            lblProduct.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblProduct.Location = new Point(12, 15);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(140, 21);
            lblProduct.TabIndex = 0;
            lblProduct.Text = "DNS-BlockList-Cleaner";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(12, 48);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(48, 15);
            lblVersion.TabIndex = 1;
            lblVersion.Text = "Version";
            // 
            // lblCopyright
            // 
            lblCopyright.AutoSize = true;
            lblCopyright.Location = new Point(12, 75);
            lblCopyright.Name = "lblCopyright";
            lblCopyright.Size = new Size(120, 15);
            lblCopyright.TabIndex = 2;
            lblCopyright.Text = "Copyright Nodorum Solutio";
            // 
            // lnkWebsite
            // 
            lnkWebsite.AutoSize = true;
            lnkWebsite.Location = new Point(12, 100);
            lnkWebsite.Name = "lnkWebsite";
            lnkWebsite.Size = new Size(160, 15);
            lnkWebsite.TabIndex = 3;
            lnkWebsite.TabStop = true;
            lnkWebsite.Text = "www.nodorumsolutio.com";
            lnkWebsite.LinkClicked += lnkWebsite_LinkClicked;
            // 
            // lblLicenseIntro
            // 
            lblLicenseIntro.AutoSize = true;
            lblLicenseIntro.Location = new Point(12, 135);
            lblLicenseIntro.Name = "lblLicenseIntro";
            lblLicenseIntro.Size = new Size(320, 15);
            lblLicenseIntro.TabIndex = 4;
            lblLicenseIntro.Text = "Licensed under the GNU General Public License v3.0.";
            // 
            // lnkLicense
            // 
            lnkLicense.AutoSize = true;
            lnkLicense.Location = new Point(12, 158);
            lnkLicense.Name = "lnkLicense";
            lnkLicense.Size = new Size(120, 15);
            lnkLicense.TabIndex = 5;
            lnkLicense.TabStop = true;
            lnkLicense.Text = "View license terms";
            lnkLicense.LinkClicked += lnkLicense_LinkClicked;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(324, 190);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(88, 27);
            btnOk.TabIndex = 6;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // AboutForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnOk;
            ClientSize = new Size(424, 229);
            Controls.Add(lblProduct);
            Controls.Add(lblVersion);
            Controls.Add(lblCopyright);
            Controls.Add(lnkWebsite);
            Controls.Add(lblLicenseIntro);
            Controls.Add(lnkLicense);
            Controls.Add(btnOk);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About DNS-BlockList-Cleaner";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProduct;
        private Label lblVersion;
        private Label lblCopyright;
        private LinkLabel lnkWebsite;
        private Label lblLicenseIntro;
        private LinkLabel lnkLicense;
        private Button btnOk;
    }
}
