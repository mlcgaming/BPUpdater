namespace MLCModpackLauncher
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minecraftDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pTRDownloadDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadOlderVersionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAppFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitAnIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modModpackIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updaterIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalInquiryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutBuddyPalsUpdaterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblModpackVersion = new System.Windows.Forms.Label();
            this.lblModpackStatusText = new System.Windows.Forms.Label();
            this.lblUpdateAvailable = new System.Windows.Forms.Label();
            this.lblUpdateToVersion = new System.Windows.Forms.Label();
            this.btnApplyUpdate = new System.Windows.Forms.Button();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(192, 24);
            this.menuMain.TabIndex = 10;
            this.menuMain.Text = "menuMain";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDirectoryToolStripMenuItem,
            this.downloadOlderVersionsToolStripMenuItem,
            this.openAppFolderToolStripMenuItem,
            this.closeProgramToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // changeDirectoryToolStripMenuItem
            // 
            this.changeDirectoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minecraftDirectoryToolStripMenuItem,
            this.pTRDownloadDirectoryToolStripMenuItem});
            this.changeDirectoryToolStripMenuItem.Name = "changeDirectoryToolStripMenuItem";
            this.changeDirectoryToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.changeDirectoryToolStripMenuItem.Text = "Change Directory";
            this.changeDirectoryToolStripMenuItem.Click += new System.EventHandler(this.changeMinecraftDirectoryToolStripMenuItem_Click);
            // 
            // minecraftDirectoryToolStripMenuItem
            // 
            this.minecraftDirectoryToolStripMenuItem.Name = "minecraftDirectoryToolStripMenuItem";
            this.minecraftDirectoryToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.minecraftDirectoryToolStripMenuItem.Text = "Minecraft Directory";
            this.minecraftDirectoryToolStripMenuItem.Click += new System.EventHandler(this.minecraftDirectoryToolStripMenuItem_Click_1);
            // 
            // pTRDownloadDirectoryToolStripMenuItem
            // 
            this.pTRDownloadDirectoryToolStripMenuItem.Name = "pTRDownloadDirectoryToolStripMenuItem";
            this.pTRDownloadDirectoryToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pTRDownloadDirectoryToolStripMenuItem.Text = "PTR Download Directory";
            this.pTRDownloadDirectoryToolStripMenuItem.Click += new System.EventHandler(this.pTRDownloadDirectoryToolStripMenuItem_Click);
            // 
            // downloadOlderVersionsToolStripMenuItem
            // 
            this.downloadOlderVersionsToolStripMenuItem.Name = "downloadOlderVersionsToolStripMenuItem";
            this.downloadOlderVersionsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.downloadOlderVersionsToolStripMenuItem.Text = "Download Older Versions";
            this.downloadOlderVersionsToolStripMenuItem.Click += new System.EventHandler(this.downloadOlderVersionsToolStripMenuItem_Click);
            // 
            // openAppFolderToolStripMenuItem
            // 
            this.openAppFolderToolStripMenuItem.Name = "openAppFolderToolStripMenuItem";
            this.openAppFolderToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openAppFolderToolStripMenuItem.Text = "Open App Folder";
            this.openAppFolderToolStripMenuItem.Click += new System.EventHandler(this.openAppFolderToolStripMenuItem_Click);
            // 
            // closeProgramToolStripMenuItem
            // 
            this.closeProgramToolStripMenuItem.Name = "closeProgramToolStripMenuItem";
            this.closeProgramToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.closeProgramToolStripMenuItem.Text = "Close Program";
            this.closeProgramToolStripMenuItem.Click += new System.EventHandler(this.closeProgramToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submitAnIssueToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutBuddyPalsUpdaterToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // submitAnIssueToolStripMenuItem
            // 
            this.submitAnIssueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modModpackIssueToolStripMenuItem,
            this.updaterIssueToolStripMenuItem,
            this.generalInquiryToolStripMenuItem});
            this.submitAnIssueToolStripMenuItem.Name = "submitAnIssueToolStripMenuItem";
            this.submitAnIssueToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.submitAnIssueToolStripMenuItem.Text = "Submit an Issue";
            // 
            // modModpackIssueToolStripMenuItem
            // 
            this.modModpackIssueToolStripMenuItem.Name = "modModpackIssueToolStripMenuItem";
            this.modModpackIssueToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.modModpackIssueToolStripMenuItem.Text = "Mod / Modpack Issue";
            this.modModpackIssueToolStripMenuItem.Click += new System.EventHandler(this.modModpackIssueToolStripMenuItem_Click);
            // 
            // updaterIssueToolStripMenuItem
            // 
            this.updaterIssueToolStripMenuItem.Name = "updaterIssueToolStripMenuItem";
            this.updaterIssueToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.updaterIssueToolStripMenuItem.Text = "Updater Issue";
            this.updaterIssueToolStripMenuItem.Click += new System.EventHandler(this.updaterIssueToolStripMenuItem_Click);
            // 
            // generalInquiryToolStripMenuItem
            // 
            this.generalInquiryToolStripMenuItem.Name = "generalInquiryToolStripMenuItem";
            this.generalInquiryToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.generalInquiryToolStripMenuItem.Text = "General Inquiry";
            this.generalInquiryToolStripMenuItem.Click += new System.EventHandler(this.generalInquiryToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // aboutBuddyPalsUpdaterToolStripMenuItem
            // 
            this.aboutBuddyPalsUpdaterToolStripMenuItem.Name = "aboutBuddyPalsUpdaterToolStripMenuItem";
            this.aboutBuddyPalsUpdaterToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.aboutBuddyPalsUpdaterToolStripMenuItem.Text = "About BuddyPals Updater";
            this.aboutBuddyPalsUpdaterToolStripMenuItem.Click += new System.EventHandler(this.aboutBuddyPalsUpdaterToolStripMenuItem_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(8, 192);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(116, 13);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Downloading Update...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblModpackVersion
            // 
            this.lblModpackVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblModpackVersion.Location = new System.Drawing.Point(0, 24);
            this.lblModpackVersion.Name = "lblModpackVersion";
            this.lblModpackVersion.Size = new System.Drawing.Size(192, 13);
            this.lblModpackVersion.TabIndex = 1;
            this.lblModpackVersion.Text = "Currently Running v.X.X.X";
            this.lblModpackVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblModpackStatusText
            // 
            this.lblModpackStatusText.AutoSize = true;
            this.lblModpackStatusText.Location = new System.Drawing.Point(117, 169);
            this.lblModpackStatusText.Name = "lblModpackStatusText";
            this.lblModpackStatusText.Size = new System.Drawing.Size(0, 13);
            this.lblModpackStatusText.TabIndex = 18;
            // 
            // lblUpdateAvailable
            // 
            this.lblUpdateAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblUpdateAvailable.Location = new System.Drawing.Point(0, 45);
            this.lblUpdateAvailable.Name = "lblUpdateAvailable";
            this.lblUpdateAvailable.Size = new System.Drawing.Size(192, 16);
            this.lblUpdateAvailable.TabIndex = 20;
            this.lblUpdateAvailable.Text = "Update Available!";
            this.lblUpdateAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUpdateToVersion
            // 
            this.lblUpdateToVersion.AutoSize = true;
            this.lblUpdateToVersion.Location = new System.Drawing.Point(32, 61);
            this.lblUpdateToVersion.Name = "lblUpdateToVersion";
            this.lblUpdateToVersion.Size = new System.Drawing.Size(128, 13);
            this.lblUpdateToVersion.TabIndex = 21;
            this.lblUpdateToVersion.Text = "Update to Version X.Y.Z?";
            this.lblUpdateToVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnApplyUpdate
            // 
            this.btnApplyUpdate.Location = new System.Drawing.Point(55, 77);
            this.btnApplyUpdate.Name = "btnApplyUpdate";
            this.btnApplyUpdate.Size = new System.Drawing.Size(82, 23);
            this.btnApplyUpdate.TabIndex = 22;
            this.btnApplyUpdate.Text = "Apply Update";
            this.btnApplyUpdate.UseVisualStyleBackColor = true;
            this.btnApplyUpdate.Click += new System.EventHandler(this.btnApplyUpdate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 110);
            this.Controls.Add(this.btnApplyUpdate);
            this.Controls.Add(this.lblUpdateToVersion);
            this.Controls.Add(this.lblUpdateAvailable);
            this.Controls.Add(this.lblModpackStatusText);
            this.Controls.Add(this.lblModpackVersion);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "BuddyPals Updater";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minecraftDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pTRDownloadDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAppFolderToolStripMenuItem;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblModpackVersion;
        private System.Windows.Forms.Label lblModpackStatusText;
        private System.Windows.Forms.Label lblUpdateAvailable;
        private System.Windows.Forms.Label lblUpdateToVersion;
        private System.Windows.Forms.Button btnApplyUpdate;
        private System.Windows.Forms.ToolStripMenuItem downloadOlderVersionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem submitAnIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modModpackIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updaterIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalInquiryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutBuddyPalsUpdaterToolStripMenuItem;
    }
}

