namespace DNS_BlockList_Cleaner
{
    partial class ExclusionsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblInfo = new Label();
            lstExclusions = new ListBox();
            btnRemove = new Button();
            btnClear = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(12, 12);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(120, 15);
            lblInfo.TabIndex = 0;
            lblInfo.Text = "Excluded domains";
            // 
            // lstExclusions
            // 
            lstExclusions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstExclusions.FormattingEnabled = true;
            lstExclusions.IntegralHeight = false;
            lstExclusions.Location = new Point(12, 36);
            lstExclusions.Name = "lstExclusions";
            lstExclusions.SelectionMode = SelectionMode.MultiExtended;
            lstExclusions.Size = new Size(300, 320);
            lstExclusions.TabIndex = 1;
            lstExclusions.SelectedIndexChanged += lstExclusions_SelectedIndexChanged;
            // 
            // btnRemove
            // 
            btnRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRemove.Location = new Point(324, 36);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(120, 27);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "Remove selected";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.Location = new Point(324, 69);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(120, 27);
            btnClear.TabIndex = 3;
            btnClear.Text = "Clear all";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.OK;
            btnClose.Location = new Point(324, 329);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 27);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // ExclusionsForm
            // 
            AcceptButton = btnClose;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(456, 368);
            Controls.Add(lblInfo);
            Controls.Add(lstExclusions);
            Controls.Add(btnRemove);
            Controls.Add(btnClear);
            Controls.Add(btnClose);
            MinimizeBox = false;
            MinimumSize = new Size(400, 300);
            Name = "ExclusionsForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Manage exclusions";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblInfo;
        private ListBox lstExclusions;
        private Button btnRemove;
        private Button btnClear;
        private Button btnClose;
    }
}
