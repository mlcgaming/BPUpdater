namespace MLCModpackLauncher
{
    partial class OlderVersionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblVersionToDownload = new System.Windows.Forms.Label();
            this.cmbVersions = new System.Windows.Forms.ComboBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpVersion = new System.Windows.Forms.GroupBox();
            this.lblVersionName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblForgeRequirementValue = new System.Windows.Forms.Label();
            this.lblVersionNameValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusProgressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpVersion.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVersionToDownload
            // 
            this.lblVersionToDownload.AutoSize = true;
            this.lblVersionToDownload.Location = new System.Drawing.Point(82, 16);
            this.lblVersionToDownload.Name = "lblVersionToDownload";
            this.lblVersionToDownload.Size = new System.Drawing.Size(108, 13);
            this.lblVersionToDownload.TabIndex = 0;
            this.lblVersionToDownload.Text = "Version to Download:";
            // 
            // cmbVersions
            // 
            this.cmbVersions.FormattingEnabled = true;
            this.cmbVersions.Location = new System.Drawing.Point(196, 13);
            this.cmbVersions.Name = "cmbVersions";
            this.cmbVersions.Size = new System.Drawing.Size(49, 21);
            this.cmbVersions.TabIndex = 1;
            this.cmbVersions.SelectedIndexChanged += new System.EventHandler(this.cmbVersions_SelectedIndexChanged);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(188, 132);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(269, 132);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpVersion
            // 
            this.grpVersion.Controls.Add(this.label2);
            this.grpVersion.Controls.Add(this.lblVersionNameValue);
            this.grpVersion.Controls.Add(this.lblForgeRequirementValue);
            this.grpVersion.Controls.Add(this.label1);
            this.grpVersion.Controls.Add(this.lblVersionName);
            this.grpVersion.Controls.Add(this.lblVersionToDownload);
            this.grpVersion.Controls.Add(this.cmbVersions);
            this.grpVersion.Location = new System.Drawing.Point(12, 12);
            this.grpVersion.Name = "grpVersion";
            this.grpVersion.Size = new System.Drawing.Size(332, 114);
            this.grpVersion.TabIndex = 4;
            this.grpVersion.TabStop = false;
            // 
            // lblVersionName
            // 
            this.lblVersionName.AutoSize = true;
            this.lblVersionName.Location = new System.Drawing.Point(6, 47);
            this.lblVersionName.Name = "lblVersionName";
            this.lblVersionName.Size = new System.Drawing.Size(68, 13);
            this.lblVersionName.TabIndex = 2;
            this.lblVersionName.Text = "Version Title:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Forge Version Required:";
            // 
            // lblForgeRequirementValue
            // 
            this.lblForgeRequirementValue.AutoSize = true;
            this.lblForgeRequirementValue.Location = new System.Drawing.Point(123, 64);
            this.lblForgeRequirementValue.Name = "lblForgeRequirementValue";
            this.lblForgeRequirementValue.Size = new System.Drawing.Size(160, 13);
            this.lblForgeRequirementValue.TabIndex = 4;
            this.lblForgeRequirementValue.Text = "1.12.2-forge1.12.2-14.23.5.2847";
            // 
            // lblVersionNameValue
            // 
            this.lblVersionNameValue.AutoSize = true;
            this.lblVersionNameValue.Location = new System.Drawing.Point(70, 47);
            this.lblVersionNameValue.Name = "lblVersionNameValue";
            this.lblVersionNameValue.Size = new System.Drawing.Size(138, 13);
            this.lblVersionNameValue.TabIndex = 5;
            this.lblVersionNameValue.Text = "Might and Magic Expansion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.label2.Location = new System.Drawing.Point(11, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(311, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Old Versions of BuddyPals Modpacks do NOT Include Forge Files";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgressbar,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 160);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(356, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusDownload";
            // 
            // statusProgressbar
            // 
            this.statusProgressbar.Name = "statusProgressbar";
            this.statusProgressbar.Size = new System.Drawing.Size(300, 16);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // OlderVersionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 182);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpVersion);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDownload);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OlderVersionsForm";
            this.Text = "Version Downloader";
            this.grpVersion.ResumeLayout(false);
            this.grpVersion.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersionToDownload;
        private System.Windows.Forms.ComboBox cmbVersions;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVersionNameValue;
        private System.Windows.Forms.Label lblForgeRequirementValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVersionName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar statusProgressbar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}