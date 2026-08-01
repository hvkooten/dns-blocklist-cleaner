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
                _appUpdateService?.Dispose();
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
            btnCheckUpdate = new Button();
            prgUpdate = new ProgressBar();
            chkAutoUpdate = new CheckBox();
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
            lblPreset.Location = new Point(12, 15);
            lblPreset.Name = "lblPreset";
            lblPreset.Size = new Size(67, 15);
            lblPreset.TabIndex = 0;
            lblPreset.Text = "HaGeZi list:";
            // 
            // cmbPreset
            // 
            cmbPreset.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbPreset.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPreset.FormattingEnabled = true;
            cmbPreset.Location = new Point(87, 12);
            cmbPreset.Name = "cmbPreset";
            cmbPreset.Size = new Size(638, 23);
            cmbPreset.TabIndex = 1;
            cmbPreset.SelectedIndexChanged += cmbPreset_SelectedIndexChanged;
            // 
            // lblSourceUrl
            // 
            lblSourceUrl.AutoSize = true;
            lblSourceUrl.Location = new Point(12, 48);
            lblSourceUrl.Name = "lblSourceUrl";
            lblSourceUrl.Size = new Size(70, 15);
            lblSourceUrl.TabIndex = 2;
            lblSourceUrl.Text = "Source URL:";
            // 
            // txtSourceUrl
            // 
            txtSourceUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSourceUrl.Location = new Point(87, 45);
            txtSourceUrl.Name = "txtSourceUrl";
            txtSourceUrl.Size = new Size(446, 23);
            txtSourceUrl.TabIndex = 3;
            // 
            // btnDownload
            // 
            btnDownload.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDownload.Location = new Point(538, 44);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(90, 25);
            btnDownload.TabIndex = 4;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // btnLoadFile
            // 
            btnLoadFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadFile.Location = new Point(634, 44);
            btnLoadFile.Name = "btnLoadFile";
            btnLoadFile.Size = new Size(90, 25);
            btnLoadFile.TabIndex = 5;
            btnLoadFile.Text = "Load file...";
            btnLoadFile.UseVisualStyleBackColor = true;
            btnLoadFile.Click += btnLoadFile_Click;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInfo.AutoEllipsis = true;
            lblInfo.Location = new Point(12, 76);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(713, 34);
            lblInfo.TabIndex = 6;
            lblInfo.Text = "No blocklist loaded.";
            // 
            // lblDomainsInfo
            // 
            lblDomainsInfo.AutoSize = true;
            lblDomainsInfo.Location = new Point(0, 26);
            lblDomainsInfo.Name = "lblDomainsInfo";
            lblDomainsInfo.Size = new Size(105, 15);
            lblDomainsInfo.TabIndex = 5;
            lblDomainsInfo.Text = "Domains (filtered):";
            // 
            // txtDomainFilter
            // 
            txtDomainFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDomainFilter.Location = new Point(75, 0);
            txtDomainFilter.Name = "txtDomainFilter";
            txtDomainFilter.PlaceholderText = "Filter domains...";
            txtDomainFilter.Size = new Size(269, 23);
            txtDomainFilter.TabIndex = 6;
            txtDomainFilter.TextChanged += txtDomainFilter_TextChanged;
            // 
            // lstDomains
            // 
            lstDomains.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstDomains.FormattingEnabled = true;
            lstDomains.IntegralHeight = false;
            lstDomains.Location = new Point(0, 44);
            lstDomains.Name = "lstDomains";
            lstDomains.Size = new Size(404, 306);
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
            grpRules.Name = "grpRules";
            grpRules.Size = new Size(301, 353);
            grpRules.TabIndex = 8;
            grpRules.TabStop = false;
            grpRules.Text = "Removal rules";
            // 
            // lblPattern
            // 
            lblPattern.AutoSize = true;
            lblPattern.Location = new Point(12, 19);
            lblPattern.Name = "lblPattern";
            lblPattern.Size = new Size(48, 15);
            lblPattern.TabIndex = 0;
            lblPattern.Text = "Pattern:";
            // 
            // txtPattern
            // 
            txtPattern.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPattern.Location = new Point(12, 37);
            txtPattern.Name = "txtPattern";
            txtPattern.Size = new Size(277, 23);
            txtPattern.TabIndex = 1;
            txtPattern.KeyPress += txtPattern_KeyPress;
            // 
            // lblMatchType
            // 
            lblMatchType.AutoSize = true;
            lblMatchType.Location = new Point(12, 71);
            lblMatchType.Name = "lblMatchType";
            lblMatchType.Size = new Size(70, 15);
            lblMatchType.TabIndex = 2;
            lblMatchType.Text = "Match type:";
            // 
            // cmbMatchType
            // 
            cmbMatchType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbMatchType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMatchType.FormattingEnabled = true;
            cmbMatchType.Location = new Point(87, 66);
            cmbMatchType.Name = "cmbMatchType";
            cmbMatchType.Size = new Size(135, 23);
            cmbMatchType.TabIndex = 3;
            // 
            // btnAddRule
            // 
            btnAddRule.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddRule.Location = new Point(232, 66);
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
            lstRules.Location = new Point(12, 97);
            lstRules.Name = "lstRules";
            lstRules.Size = new Size(277, 208);
            lstRules.TabIndex = 5;
            lstRules.ItemCheck += lstRules_ItemCheck;
            lstRules.MouseUp += lstRules_MouseUp;
            // 
            // btnEditRule
            // 
            btnEditRule.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditRule.Location = new Point(12, 314);
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
            btnRemoveRule.Location = new Point(108, 314);
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
            btnPreview.Location = new Point(185, 314);
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
            lblOutput.Location = new Point(12, 511);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(67, 15);
            lblOutput.TabIndex = 9;
            lblOutput.Text = "Output file:";
            // 
            // txtOutputFile
            // 
            txtOutputFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtOutputFile.Location = new Point(95, 508);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(271, 23);
            txtOutputFile.TabIndex = 10;
            // 
            // btnGenerate
            // 
            btnGenerate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGenerate.Location = new Point(564, 505);
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
            txtLog.Location = new Point(12, 538);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(712, 108);
            txtLog.TabIndex = 12;
            // 
            // lblDomainFilter
            // 
            lblDomainFilter.AutoSize = true;
            lblDomainFilter.Location = new Point(0, 3);
            lblDomainFilter.Name = "lblDomainFilter";
            lblDomainFilter.Size = new Size(36, 15);
            lblDomainFilter.TabIndex = 13;
            lblDomainFilter.Text = "Filter:";
            // 
            // btnAbout
            // 
            btnAbout.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAbout.Location = new Point(468, 505);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(90, 27);
            btnAbout.TabIndex = 14;
            btnAbout.Text = "About...";
            btnAbout.UseVisualStyleBackColor = true;
            btnAbout.Click += btnAbout_Click;
            // 
            // btnHelp
            // 
            btnHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnHelp.Location = new Point(372, 505);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(90, 27);
            btnHelp.TabIndex = 17;
            btnHelp.Text = "Help...";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // btnCheckUpdate
            // 
            btnCheckUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCheckUpdate.Location = new Point(224, 473);
            btnCheckUpdate.Name = "btnCheckUpdate";
            btnCheckUpdate.Size = new Size(112, 25);
            btnCheckUpdate.TabIndex = 18;
            btnCheckUpdate.Text = "Check for updates...";
            btnCheckUpdate.UseVisualStyleBackColor = true;
            btnCheckUpdate.Click += btnCheckUpdate_Click;
            // 
            // prgUpdate
            // 
            prgUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prgUpdate.Location = new Point(347, 476);
            prgUpdate.Name = "prgUpdate";
            prgUpdate.Size = new Size(377, 19);
            prgUpdate.TabIndex = 19;
            prgUpdate.Visible = false;
            // 
            // chkAutoUpdate
            // 
            chkAutoUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkAutoUpdate.AutoSize = true;
            chkAutoUpdate.Checked = true;
            chkAutoUpdate.CheckState = CheckState.Checked;
            chkAutoUpdate.Location = new Point(12, 474);
            chkAutoUpdate.Name = "chkAutoUpdate";
            chkAutoUpdate.Size = new Size(179, 19);
            chkAutoUpdate.TabIndex = 20;
            chkAutoUpdate.Text = "Check for updates on startup";
            chkAutoUpdate.UseVisualStyleBackColor = true;
            chkAutoUpdate.CheckedChanged += chkAutoUpdate_CheckedChanged;
            // 
            // btnCopyToPattern
            // 
            btnCopyToPattern.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCopyToPattern.Location = new Point(350, 0);
            btnCopyToPattern.Name = "btnCopyToPattern";
            btnCopyToPattern.Size = new Size(26, 23);
            btnCopyToPattern.TabIndex = 15;
            btnCopyToPattern.Text = "→";
            btnCopyToPattern.UseVisualStyleBackColor = true;
            btnCopyToPattern.Click += btnCopyToPattern_Click;
            // 
            // btnCopyToFilter
            // 
            btnCopyToFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCopyToFilter.Location = new Point(378, 0);
            btnCopyToFilter.Name = "btnCopyToFilter";
            btnCopyToFilter.Size = new Size(26, 23);
            btnCopyToFilter.TabIndex = 16;
            btnCopyToFilter.Text = "←";
            btnCopyToFilter.UseVisualStyleBackColor = true;
            btnCopyToFilter.Click += btnCopyToFilter_Click;
            // 
            // splitMain
            // 
            splitMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitMain.Location = new Point(12, 113);
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
            splitMain.Size = new Size(713, 353);
            splitMain.SplitterDistance = 406;
            splitMain.SplitterWidth = 6;
            splitMain.TabIndex = 15;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(736, 658);
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
            Controls.Add(btnCheckUpdate);
            Controls.Add(prgUpdate);
            Controls.Add(chkAutoUpdate);
            MinimumSize = new Size(698, 526);
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
        private Button btnCheckUpdate;
        private ProgressBar prgUpdate;
        private CheckBox chkAutoUpdate;
        private Button btnCopyToPattern;
        private Button btnCopyToFilter;
        private SplitContainer splitMain;
    }
}
