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
            this.grpModpack = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpCommunity = new System.Windows.Forms.GroupBox();
            this.linkWiki = new System.Windows.Forms.LinkLabel();
            this.picBuddyPalLogo = new System.Windows.Forms.PictureBox();
            this.linkMainSite = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMainSite = new System.Windows.Forms.Label();
            this.btnForceUpdate = new System.Windows.Forms.Button();
            this.menuMain.SuspendLayout();
            this.grpModpack.SuspendLayout();
            this.grpCommunity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBuddyPalLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(417, 24);
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
            this.lblModpackVersion.Location = new System.Drawing.Point(110, 37);
            this.lblModpackVersion.Name = "lblModpackVersion";
            this.lblModpackVersion.Size = new System.Drawing.Size(52, 14);
            this.lblModpackVersion.TabIndex = 1;
            this.lblModpackVersion.Text = "X.X.X";
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
            this.lblUpdateAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblUpdateAvailable.Location = new System.Drawing.Point(59, 16);
            this.lblUpdateAvailable.Name = "lblUpdateAvailable";
            this.lblUpdateAvailable.Size = new System.Drawing.Size(135, 19);
            this.lblUpdateAvailable.TabIndex = 20;
            this.lblUpdateAvailable.Text = "Update Available!";
            this.lblUpdateAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUpdateToVersion
            // 
            this.lblUpdateToVersion.AutoSize = true;
            this.lblUpdateToVersion.Location = new System.Drawing.Point(34, 78);
            this.lblUpdateToVersion.Name = "lblUpdateToVersion";
            this.lblUpdateToVersion.Size = new System.Drawing.Size(128, 13);
            this.lblUpdateToVersion.TabIndex = 21;
            this.lblUpdateToVersion.Text = "Update to Version X.Y.Z?";
            this.lblUpdateToVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnApplyUpdate
            // 
            this.btnApplyUpdate.Location = new System.Drawing.Point(112, 94);
            this.btnApplyUpdate.Name = "btnApplyUpdate";
            this.btnApplyUpdate.Size = new System.Drawing.Size(82, 23);
            this.btnApplyUpdate.TabIndex = 22;
            this.btnApplyUpdate.Text = "Apply Update";
            this.btnApplyUpdate.UseVisualStyleBackColor = true;
            this.btnApplyUpdate.Click += new System.EventHandler(this.btnApplyUpdate_Click);
            // 
            // grpModpack
            // 
            this.grpModpack.Controls.Add(this.btnForceUpdate);
            this.grpModpack.Controls.Add(this.label2);
            this.grpModpack.Controls.Add(this.label1);
            this.grpModpack.Controls.Add(this.lblUpdateAvailable);
            this.grpModpack.Controls.Add(this.btnApplyUpdate);
            this.grpModpack.Controls.Add(this.lblModpackVersion);
            this.grpModpack.Controls.Add(this.lblUpdateToVersion);
            this.grpModpack.Location = new System.Drawing.Point(3, 29);
            this.grpModpack.Name = "grpModpack";
            this.grpModpack.Size = new System.Drawing.Size(200, 126);
            this.grpModpack.TabIndex = 23;
            this.grpModpack.TabStop = false;
            this.grpModpack.Text = "Modpack Information";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Current Version:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Status:";
            // 
            // grpCommunity
            // 
            this.grpCommunity.Controls.Add(this.linkWiki);
            this.grpCommunity.Controls.Add(this.picBuddyPalLogo);
            this.grpCommunity.Controls.Add(this.linkMainSite);
            this.grpCommunity.Controls.Add(this.label3);
            this.grpCommunity.Controls.Add(this.lblMainSite);
            this.grpCommunity.Location = new System.Drawing.Point(209, 29);
            this.grpCommunity.Name = "grpCommunity";
            this.grpCommunity.Size = new System.Drawing.Size(200, 126);
            this.grpCommunity.TabIndex = 24;
            this.grpCommunity.TabStop = false;
            this.grpCommunity.Text = "Community Information";
            // 
            // linkWiki
            // 
            this.linkWiki.AutoSize = true;
            this.linkWiki.Location = new System.Drawing.Point(112, 37);
            this.linkWiki.Name = "linkWiki";
            this.linkWiki.Size = new System.Drawing.Size(56, 13);
            this.linkWiki.TabIndex = 4;
            this.linkWiki.TabStop = true;
            this.linkWiki.Text = "Click Here";
            // 
            // picBuddyPalLogo
            // 
            this.picBuddyPalLogo.Image = ((System.Drawing.Image)(resources.GetObject("picBuddyPalLogo.Image")));
            this.picBuddyPalLogo.Location = new System.Drawing.Point(65, 56);
            this.picBuddyPalLogo.Name = "picBuddyPalLogo";
            this.picBuddyPalLogo.Size = new System.Drawing.Size(66, 64);
            this.picBuddyPalLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBuddyPalLogo.TabIndex = 3;
            this.picBuddyPalLogo.TabStop = false;
            // 
            // linkMainSite
            // 
            this.linkMainSite.AutoSize = true;
            this.linkMainSite.Location = new System.Drawing.Point(93, 19);
            this.linkMainSite.Name = "linkMainSite";
            this.linkMainSite.Size = new System.Drawing.Size(56, 13);
            this.linkMainSite.TabIndex = 2;
            this.linkMainSite.TabStop = true;
            this.linkMainSite.Text = "Click Here";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Community Wiki:";
            // 
            // lblMainSite
            // 
            this.lblMainSite.AutoSize = true;
            this.lblMainSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainSite.Location = new System.Drawing.Point(6, 19);
            this.lblMainSite.Name = "lblMainSite";
            this.lblMainSite.Size = new System.Drawing.Size(81, 13);
            this.lblMainSite.TabIndex = 0;
            this.lblMainSite.Text = "Latest News:";
            // 
            // btnForceUpdate
            // 
            this.btnForceUpdate.Location = new System.Drawing.Point(6, 94);
            this.btnForceUpdate.Name = "btnForceUpdate";
            this.btnForceUpdate.Size = new System.Drawing.Size(82, 23);
            this.btnForceUpdate.TabIndex = 25;
            this.btnForceUpdate.Text = "Force Update";
            this.btnForceUpdate.UseVisualStyleBackColor = true;
            this.btnForceUpdate.Click += new System.EventHandler(this.btnForceUpdate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 162);
            this.Controls.Add(this.grpCommunity);
            this.Controls.Add(this.grpModpack);
            this.Controls.Add(this.lblModpackStatusText);
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
            this.grpModpack.ResumeLayout(false);
            this.grpModpack.PerformLayout();
            this.grpCommunity.ResumeLayout(false);
            this.grpCommunity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBuddyPalLogo)).EndInit();
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
        private System.Windows.Forms.GroupBox grpModpack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpCommunity;
        private System.Windows.Forms.LinkLabel linkWiki;
        private System.Windows.Forms.PictureBox picBuddyPalLogo;
        private System.Windows.Forms.LinkLabel linkMainSite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMainSite;
        private System.Windows.Forms.Button btnForceUpdate;
    }
}

