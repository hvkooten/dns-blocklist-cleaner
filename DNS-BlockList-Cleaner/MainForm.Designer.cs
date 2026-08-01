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
            lblPreset = new Label();
            cmbPreset = new ComboBox();
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
            btnAbout = new Button();
            btnHelp = new Button();
            btnCopyToPattern = new Button();
            btnCopyToFilter = new Button();
            splitMain = new SplitContainer();
            grpRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            SuspendLayout();
            // 
            // lblPreset
            // 
            lblPreset.AutoSize = true;
            lblPreset.Location = new Point(17, 25);
            lblPreset.Margin = new Padding(4, 0, 4, 0);
            lblPreset.Name = "lblPreset";
            lblPreset.Size = new Size(100, 25);
            lblPreset.TabIndex = 0;
            lblPreset.Text = "HaGeZi list:";
            // 
            // cmbPreset
            // 
            cmbPreset.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbPreset.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPreset.FormattingEnabled = true;
            cmbPreset.Location = new Point(124, 20);
            cmbPreset.Margin = new Padding(4, 5, 4, 5);
            cmbPreset.Name = "cmbPreset";
            cmbPreset.Size = new Size(909, 33);
            cmbPreset.TabIndex = 1;
            cmbPreset.SelectedIndexChanged += cmbPreset_SelectedIndexChanged;
            // 
            // lblSourceUrl
            // 
            lblSourceUrl.AutoSize = true;
            lblSourceUrl.Location = new Point(17, 80);
            lblSourceUrl.Margin = new Padding(4, 0, 4, 0);
            lblSourceUrl.Name = "lblSourceUrl";
            lblSourceUrl.Size = new Size(106, 25);
            lblSourceUrl.TabIndex = 2;
            lblSourceUrl.Text = "Source URL:";
            // 
            // txtSourceUrl
            // 
            txtSourceUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSourceUrl.Location = new Point(124, 75);
            txtSourceUrl.Margin = new Padding(4, 5, 4, 5);
            txtSourceUrl.Name = "txtSourceUrl";
            txtSourceUrl.Size = new Size(635, 31);
            txtSourceUrl.TabIndex = 3;
            // 
            // btnDownload
            // 
            btnDownload.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDownload.Location = new Point(769, 73);
            btnDownload.Margin = new Padding(4, 5, 4, 5);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(129, 42);
            btnDownload.TabIndex = 4;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // btnLoadFile
            // 
            btnLoadFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadFile.Location = new Point(906, 73);
            btnLoadFile.Margin = new Padding(4, 5, 4, 5);
            btnLoadFile.Name = "btnLoadFile";
            btnLoadFile.Size = new Size(129, 42);
            btnLoadFile.TabIndex = 5;
            btnLoadFile.Text = "Load file...";
            btnLoadFile.UseVisualStyleBackColor = true;
            btnLoadFile.Click += btnLoadFile_Click;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInfo.AutoEllipsis = true;
            lblInfo.Location = new Point(17, 127);
            lblInfo.Margin = new Padding(4, 0, 4, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(1018, 57);
            lblInfo.TabIndex = 6;
            lblInfo.Text = "No blocklist loaded.";
            // 
            // lblDomainsInfo
            // 
            lblDomainsInfo.AutoSize = true;
            lblDomainsInfo.Location = new Point(0, 43);
            lblDomainsInfo.Margin = new Padding(4, 0, 4, 0);
            lblDomainsInfo.Name = "lblDomainsInfo";
            lblDomainsInfo.Size = new Size(157, 25);
            lblDomainsInfo.TabIndex = 5;
            lblDomainsInfo.Text = "Domains (filtered):";
            // 
            // txtDomainFilter
            // 
            txtDomainFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDomainFilter.Location = new Point(107, 0);
            txtDomainFilter.Margin = new Padding(4, 5, 4, 5);
            txtDomainFilter.Name = "txtDomainFilter";
            txtDomainFilter.PlaceholderText = "Filter domains...";
            txtDomainFilter.Size = new Size(382, 31);
            txtDomainFilter.TabIndex = 6;
            txtDomainFilter.TextChanged += txtDomainFilter_TextChanged;
            // 
            // lstDomains
            // 
            lstDomains.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstDomains.FormattingEnabled = true;
            lstDomains.IntegralHeight = false;
            lstDomains.Location = new Point(0, 73);
            lstDomains.Margin = new Padding(4, 5, 4, 5);
            lstDomains.Name = "lstDomains";
            lstDomains.Size = new Size(575, 352);
            lstDomains.TabIndex = 7;
            lstDomains.DoubleClick += lstDomains_DoubleClick;
            // 
            // grpRules
            // 
            grpRules.Controls.Add(lblPattern);
            grpRules.Controls.Add(txtPattern);
            grpRules.Controls.Add(lblMatchType);
            grpRules.Controls.Add(cmbMatchType);
            grpRules.Controls.Add(btnAddRule);
            grpRules.Controls.Add(lstRules);
            grpRules.Controls.Add(btnEditRule);
            grpRules.Controls.Add(btnRemoveRule);
            grpRules.Controls.Add(btnPreview);
            grpRules.Dock = DockStyle.Fill;
            grpRules.Location = new Point(0, 0);
            grpRules.Margin = new Padding(4, 5, 4, 5);
            grpRules.Name = "grpRules";
            grpRules.Padding = new Padding(4, 5, 4, 5);
            grpRules.Size = new Size(429, 428);
            grpRules.TabIndex = 8;
            grpRules.TabStop = false;
            grpRules.Text = "Removal rules";
            // 
            // lblPattern
            // 
            lblPattern.AutoSize = true;
            lblPattern.Location = new Point(17, 32);
            lblPattern.Margin = new Padding(4, 0, 4, 0);
            lblPattern.Name = "lblPattern";
            lblPattern.Size = new Size(71, 25);
            lblPattern.TabIndex = 0;
            lblPattern.Text = "Pattern:";
            // 
            // txtPattern
            // 
            txtPattern.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPattern.Location = new Point(17, 62);
            txtPattern.Margin = new Padding(4, 5, 4, 5);
            txtPattern.Name = "txtPattern";
            txtPattern.Size = new Size(393, 31);
            txtPattern.TabIndex = 1;
            txtPattern.KeyPress += txtPattern_KeyPress;
            // 
            // lblMatchType
            // 
            lblMatchType.AutoSize = true;
            lblMatchType.Location = new Point(17, 118);
            lblMatchType.Margin = new Padding(4, 0, 4, 0);
            lblMatchType.Name = "lblMatchType";
            lblMatchType.Size = new Size(105, 25);
            lblMatchType.TabIndex = 2;
            lblMatchType.Text = "Match type:";
            // 
            // cmbMatchType
            // 
            cmbMatchType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbMatchType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMatchType.FormattingEnabled = true;
            cmbMatchType.Location = new Point(124, 110);
            cmbMatchType.Margin = new Padding(4, 5, 4, 5);
            cmbMatchType.Name = "cmbMatchType";
            cmbMatchType.Size = new Size(190, 33);
            cmbMatchType.TabIndex = 3;
            // 
            // btnAddRule
            // 
            btnAddRule.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddRule.Location = new Point(330, 110);
            btnAddRule.Margin = new Padding(4, 5, 4, 5);
            btnAddRule.Name = "btnAddRule";
            btnAddRule.Size = new Size(81, 42);
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
            lstRules.Location = new Point(17, 162);
            lstRules.Margin = new Padding(4, 5, 4, 5);
            lstRules.Name = "lstRules";
            lstRules.Size = new Size(393, 184);
            lstRules.TabIndex = 5;
            lstRules.ItemCheck += lstRules_ItemCheck;
            lstRules.MouseUp += lstRules_MouseUp;
            // 
            // btnEditRule
            // 
            btnEditRule.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditRule.Location = new Point(17, 363);
            btnEditRule.Margin = new Padding(4, 5, 4, 5);
            btnEditRule.Name = "btnEditRule";
            btnEditRule.Size = new Size(129, 45);
            btnEditRule.TabIndex = 6;
            btnEditRule.Text = "Edit";
            btnEditRule.UseVisualStyleBackColor = true;
            btnEditRule.Click += btnEditRule_Click;
            // 
            // btnRemoveRule
            // 
            btnRemoveRule.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRemoveRule.Location = new Point(154, 363);
            btnRemoveRule.Margin = new Padding(4, 5, 4, 5);
            btnRemoveRule.Name = "btnRemoveRule";
            btnRemoveRule.Size = new Size(129, 45);
            btnRemoveRule.TabIndex = 7;
            btnRemoveRule.Text = "Remove";
            btnRemoveRule.UseVisualStyleBackColor = true;
            btnRemoveRule.Click += btnRemoveRule_Click;
            // 
            // btnPreview
            // 
            btnPreview.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPreview.Location = new Point(263, 363);
            btnPreview.Margin = new Padding(4, 5, 4, 5);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(149, 45);
            btnPreview.TabIndex = 8;
            btnPreview.Text = "Preview";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += btnPreview_Click;
            // 
            // lblOutput
            // 
            lblOutput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(17, 636);
            lblOutput.Margin = new Padding(4, 0, 4, 0);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(101, 25);
            lblOutput.TabIndex = 9;
            lblOutput.Text = "Output file:";
            // 
            // txtOutputFile
            // 
            txtOutputFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtOutputFile.Location = new Point(136, 631);
            txtOutputFile.Margin = new Padding(4, 5, 4, 5);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(386, 31);
            txtOutputFile.TabIndex = 10;
            // 
            // btnGenerate
            // 
            btnGenerate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGenerate.Location = new Point(806, 626);
            btnGenerate.Margin = new Padding(4, 5, 4, 5);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(229, 45);
            btnGenerate.TabIndex = 11;
            btnGenerate.Text = "Generate cleaned file";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.Location = new Point(17, 681);
            txtLog.Margin = new Padding(4, 5, 4, 5);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(1016, 177);
            txtLog.TabIndex = 12;
            // 
            // lblDomainFilter
            // 
            lblDomainFilter.AutoSize = true;
            lblDomainFilter.Location = new Point(0, 5);
            lblDomainFilter.Margin = new Padding(4, 0, 4, 0);
            lblDomainFilter.Name = "lblDomainFilter";
            lblDomainFilter.Size = new Size(54, 25);
            lblDomainFilter.TabIndex = 13;
            lblDomainFilter.Text = "Filter:";
            // 
            // btnAbout
            // 
            btnAbout.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAbout.Location = new Point(669, 626);
            btnAbout.Margin = new Padding(4, 5, 4, 5);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(129, 45);
            btnAbout.TabIndex = 14;
            btnAbout.Text = "About...";
            btnAbout.UseVisualStyleBackColor = true;
            btnAbout.Click += btnAbout_Click;
            // 
            // btnHelp
            // 
            btnHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnHelp.Location = new Point(532, 626);
            btnHelp.Margin = new Padding(4, 5, 4, 5);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(129, 45);
            btnHelp.TabIndex = 17;
            btnHelp.Text = "Help...";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // btnCopyToPattern
            // 
            btnCopyToPattern.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCopyToPattern.Location = new Point(500, 0);
            btnCopyToPattern.Margin = new Padding(4, 5, 4, 5);
            btnCopyToPattern.Name = "btnCopyToPattern";
            btnCopyToPattern.Size = new Size(37, 38);
            btnCopyToPattern.TabIndex = 15;
            btnCopyToPattern.Text = "→";
            btnCopyToPattern.UseVisualStyleBackColor = true;
            btnCopyToPattern.Click += btnCopyToPattern_Click;
            // 
            // btnCopyToFilter
            // 
            btnCopyToFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCopyToFilter.Location = new Point(540, 0);
            btnCopyToFilter.Margin = new Padding(4, 5, 4, 5);
            btnCopyToFilter.Name = "btnCopyToFilter";
            btnCopyToFilter.Size = new Size(37, 38);
            btnCopyToFilter.TabIndex = 16;
            btnCopyToFilter.Text = "←";
            btnCopyToFilter.UseVisualStyleBackColor = true;
            btnCopyToFilter.Click += btnCopyToFilter_Click;
            // 
            // splitMain
            // 
            splitMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitMain.Location = new Point(17, 188);
            splitMain.Margin = new Padding(4, 5, 4, 5);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(lstDomains);
            splitMain.Panel1.Controls.Add(txtDomainFilter);
            splitMain.Panel1.Controls.Add(btnCopyToPattern);
            splitMain.Panel1.Controls.Add(btnCopyToFilter);
            splitMain.Panel1.Controls.Add(lblDomainFilter);
            splitMain.Panel1.Controls.Add(lblDomainsInfo);
            splitMain.Panel1MinSize = 200;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(grpRules);
            splitMain.Panel2MinSize = 300;
            splitMain.Size = new Size(1018, 428);
            splitMain.SplitterDistance = 580;
            splitMain.SplitterWidth = 9;
            splitMain.TabIndex = 15;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1052, 881);
            Controls.Add(lblPreset);
            Controls.Add(cmbPreset);
            Controls.Add(lblSourceUrl);
            Controls.Add(txtSourceUrl);
            Controls.Add(btnDownload);
            Controls.Add(btnLoadFile);
            Controls.Add(lblInfo);
            Controls.Add(splitMain);
            Controls.Add(lblOutput);
            Controls.Add(txtOutputFile);
            Controls.Add(btnGenerate);
            Controls.Add(txtLog);
            Controls.Add(btnAbout);
            Controls.Add(btnHelp);
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(991, 851);
            Name = "MainForm";
            Text = "DNS-BlockList-Cleaner";
            Load += MainForm_Load;
            grpRules.ResumeLayout(false);
            grpRules.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel1.PerformLayout();
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPreset;
        private ComboBox cmbPreset;
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
        private Button btnAbout;
        private Button btnHelp;
        private Button btnCopyToPattern;
        private Button btnCopyToFilter;
        private SplitContainer splitMain;
    }
}
