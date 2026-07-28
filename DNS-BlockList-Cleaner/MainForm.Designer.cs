namespace DNS_BlockList_Cleaner
{
    partial class MainForm
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
                _updateService?.Dispose();
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
            lblSourceUrl = new Label();
            txtSourceUrl = new TextBox();
            btnDownload = new Button();
            btnLoadFile = new Button();
            lblInfo = new Label();
            lblDomainsInfo = new Label();
            txtDomainFilter = new TextBox();
            lstDomains = new ListBox();
            grpRules = new GroupBox();
            lblPattern = new Label();
            txtPattern = new TextBox();
            lblMatchType = new Label();
            cmbMatchType = new ComboBox();
            btnAddRule = new Button();
            lstRules = new CheckedListBox();
            btnEditRule = new Button();
            btnRemoveRule = new Button();
            btnPreview = new Button();
            lblOutput = new Label();
            txtOutputFile = new TextBox();
            btnGenerate = new Button();
            txtLog = new TextBox();
            lblDomainFilter = new Label();
            grpRules.SuspendLayout();
            SuspendLayout();
            // 
            // lblSourceUrl
            // 
            lblSourceUrl.AutoSize = true;
            lblSourceUrl.Location = new Point(12, 15);
            lblSourceUrl.Name = "lblSourceUrl";
            lblSourceUrl.Size = new Size(70, 15);
            lblSourceUrl.TabIndex = 0;
            lblSourceUrl.Text = "Source URL:";
            // 
            // txtSourceUrl
            // 
            txtSourceUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSourceUrl.Location = new Point(87, 12);
            txtSourceUrl.Name = "txtSourceUrl";
            txtSourceUrl.Size = new Size(505, 23);
            txtSourceUrl.TabIndex = 1;
            // 
            // btnDownload
            // 
            btnDownload.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDownload.Location = new Point(598, 11);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(90, 25);
            btnDownload.TabIndex = 2;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // btnLoadFile
            // 
            btnLoadFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadFile.Location = new Point(694, 11);
            btnLoadFile.Name = "btnLoadFile";
            btnLoadFile.Size = new Size(90, 25);
            btnLoadFile.TabIndex = 3;
            btnLoadFile.Text = "Load file...";
            btnLoadFile.UseVisualStyleBackColor = true;
            btnLoadFile.Click += btnLoadFile_Click;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInfo.AutoEllipsis = true;
            lblInfo.Location = new Point(12, 43);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(772, 20);
            lblInfo.TabIndex = 4;
            lblInfo.Text = "No blocklist loaded.";
            // 
            // lblDomainsInfo
            // 
            lblDomainsInfo.AutoSize = true;
            lblDomainsInfo.Location = new Point(12, 94);
            lblDomainsInfo.Name = "lblDomainsInfo";
            lblDomainsInfo.Size = new Size(105, 15);
            lblDomainsInfo.TabIndex = 5;
            lblDomainsInfo.Text = "Domains (filtered):";
            // 
            // txtDomainFilter
            // 
            txtDomainFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDomainFilter.Location = new Point(87, 67);
            txtDomainFilter.Name = "txtDomainFilter";
            txtDomainFilter.PlaceholderText = "Filter domains...";
            txtDomainFilter.Size = new Size(363, 23);
            txtDomainFilter.TabIndex = 6;
            txtDomainFilter.TextChanged += txtDomainFilter_TextChanged;
            // 
            // lstDomains
            // 
            lstDomains.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstDomains.FormattingEnabled = true;
            lstDomains.IntegralHeight = false;
            lstDomains.Location = new Point(12, 112);
            lstDomains.Name = "lstDomains";
            lstDomains.Size = new Size(438, 433);
            lstDomains.TabIndex = 7;
            // 
            // grpRules
            // 
            grpRules.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpRules.Controls.Add(lblPattern);
            grpRules.Controls.Add(txtPattern);
            grpRules.Controls.Add(lblMatchType);
            grpRules.Controls.Add(cmbMatchType);
            grpRules.Controls.Add(btnAddRule);
            grpRules.Controls.Add(lstRules);
            grpRules.Controls.Add(btnEditRule);
            grpRules.Controls.Add(btnRemoveRule);
            grpRules.Controls.Add(btnPreview);
            grpRules.Location = new Point(462, 67);
            grpRules.Name = "grpRules";
            grpRules.Size = new Size(322, 478);
            grpRules.TabIndex = 8;
            grpRules.TabStop = false;
            grpRules.Text = "Removal rules";
            // 
            // lblPattern
            // 
            lblPattern.AutoSize = true;
            lblPattern.Location = new Point(12, 27);
            lblPattern.Name = "lblPattern";
            lblPattern.Size = new Size(48, 15);
            lblPattern.TabIndex = 0;
            lblPattern.Text = "Pattern:";
            // 
            // txtPattern
            // 
            txtPattern.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPattern.Location = new Point(12, 45);
            txtPattern.Name = "txtPattern";
            txtPattern.Size = new Size(298, 23);
            txtPattern.TabIndex = 1;
            // 
            // lblMatchType
            // 
            lblMatchType.AutoSize = true;
            lblMatchType.Location = new Point(12, 78);
            lblMatchType.Name = "lblMatchType";
            lblMatchType.Size = new Size(70, 15);
            lblMatchType.TabIndex = 2;
            lblMatchType.Text = "Match type:";
            // 
            // cmbMatchType
            // 
            cmbMatchType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMatchType.FormattingEnabled = true;
            cmbMatchType.Location = new Point(87, 75);
            cmbMatchType.Name = "cmbMatchType";
            cmbMatchType.Size = new Size(160, 23);
            cmbMatchType.TabIndex = 3;
            // 
            // btnAddRule
            // 
            btnAddRule.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddRule.Location = new Point(253, 74);
            btnAddRule.Name = "btnAddRule";
            btnAddRule.Size = new Size(57, 25);
            btnAddRule.TabIndex = 4;
            btnAddRule.Text = "Add";
            btnAddRule.UseVisualStyleBackColor = true;
            btnAddRule.Click += btnAddRule_Click;
            // 
            // lstRules
            // 
            lstRules.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstRules.CheckOnClick = true;
            lstRules.FormattingEnabled = true;
            lstRules.IntegralHeight = false;
            lstRules.Location = new Point(12, 110);
            lstRules.Name = "lstRules";
            lstRules.Size = new Size(298, 320);
            lstRules.TabIndex = 5;
            lstRules.ItemCheck += lstRules_ItemCheck;
            // 
            // btnEditRule
            // 
            btnEditRule.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditRule.Location = new Point(12, 439);
            btnEditRule.Name = "btnEditRule";
            btnEditRule.Size = new Size(90, 27);
            btnEditRule.TabIndex = 6;
            btnEditRule.Text = "Edit";
            btnEditRule.UseVisualStyleBackColor = true;
            btnEditRule.Click += btnEditRule_Click;
            // 
            // btnRemoveRule
            // 
            btnRemoveRule.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRemoveRule.Location = new Point(108, 439);
            btnRemoveRule.Name = "btnRemoveRule";
            btnRemoveRule.Size = new Size(90, 27);
            btnRemoveRule.TabIndex = 7;
            btnRemoveRule.Text = "Remove";
            btnRemoveRule.UseVisualStyleBackColor = true;
            btnRemoveRule.Click += btnRemoveRule_Click;
            // 
            // btnPreview
            // 
            btnPreview.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPreview.Location = new Point(206, 439);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(104, 27);
            btnPreview.TabIndex = 8;
            btnPreview.Text = "Preview";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += btnPreview_Click;
            // 
            // lblOutput
            // 
            lblOutput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(12, 557);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(67, 15);
            lblOutput.TabIndex = 9;
            lblOutput.Text = "Output file:";
            // 
            // txtOutputFile
            // 
            txtOutputFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtOutputFile.Location = new Point(95, 554);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(355, 23);
            txtOutputFile.TabIndex = 10;
            // 
            // btnGenerate
            // 
            btnGenerate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGenerate.Location = new Point(624, 551);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(160, 27);
            btnGenerate.TabIndex = 11;
            btnGenerate.Text = "Generate cleaned file";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.Location = new Point(12, 584);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(772, 108);
            txtLog.TabIndex = 12;
            // 
            // lblDomainFilter
            // 
            lblDomainFilter.AutoSize = true;
            lblDomainFilter.Location = new Point(12, 70);
            lblDomainFilter.Name = "lblDomainFilter";
            lblDomainFilter.Size = new Size(36, 15);
            lblDomainFilter.TabIndex = 13;
            lblDomainFilter.Text = "Filter:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 704);
            Controls.Add(lblDomainFilter);
            Controls.Add(lblSourceUrl);
            Controls.Add(txtSourceUrl);
            Controls.Add(btnDownload);
            Controls.Add(btnLoadFile);
            Controls.Add(lblInfo);
            Controls.Add(lblDomainsInfo);
            Controls.Add(txtDomainFilter);
            Controls.Add(lstDomains);
            Controls.Add(grpRules);
            Controls.Add(lblOutput);
            Controls.Add(txtOutputFile);
            Controls.Add(btnGenerate);
            Controls.Add(txtLog);
            MinimumSize = new Size(700, 500);
            Name = "MainForm";
            Text = "DNS-BlockList-Cleaner";
            Load += MainForm_Load;
            grpRules.ResumeLayout(false);
            grpRules.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSourceUrl;
        private TextBox txtSourceUrl;
        private Button btnDownload;
        private Button btnLoadFile;
        private Label lblInfo;
        private Label lblDomainsInfo;
        private TextBox txtDomainFilter;
        private ListBox lstDomains;
        private GroupBox grpRules;
        private Label lblPattern;
        private TextBox txtPattern;
        private Label lblMatchType;
        private ComboBox cmbMatchType;
        private Button btnAddRule;
        private CheckedListBox lstRules;
        private Button btnEditRule;
        private Button btnRemoveRule;
        private Button btnPreview;
        private Label lblOutput;
        private TextBox txtOutputFile;
        private Button btnGenerate;
        private TextBox txtLog;
        private Label lblDomainFilter;
    }
}
