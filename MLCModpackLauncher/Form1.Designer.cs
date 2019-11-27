﻿namespace MLCModpackLauncher
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
            this.btnUpdateModpack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.lblLatestVersion = new System.Windows.Forms.Label();
            this.lblInstalledVersionText = new System.Windows.Forms.Label();
            this.lblOnlineVersionText = new System.Windows.Forms.Label();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.prgbarMain = new System.Windows.Forms.ProgressBar();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minecraftDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pTRDownloadDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadPTRPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdateForge = new System.Windows.Forms.Button();
            this.grpUpdate = new System.Windows.Forms.GroupBox();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.menuMain.SuspendLayout();
            this.grpUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpdateModpack
            // 
            this.btnUpdateModpack.Location = new System.Drawing.Point(6, 16);
            this.btnUpdateModpack.Name = "btnUpdateModpack";
            this.btnUpdateModpack.Size = new System.Drawing.Size(80, 23);
            this.btnUpdateModpack.TabIndex = 0;
            this.btnUpdateModpack.Text = "Modpack";
            this.btnUpdateModpack.UseVisualStyleBackColor = true;
            this.btnUpdateModpack.Click += new System.EventHandler(this.BtnApplyUpdate_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(10, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(171, 13);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "BuddyPals Modpack Updater";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentVersion.Location = new System.Drawing.Point(10, 87);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(98, 13);
            this.lblCurrentVersion.TabIndex = 4;
            this.lblCurrentVersion.Text = "Current Version:";
            // 
            // lblLatestVersion
            // 
            this.lblLatestVersion.AutoSize = true;
            this.lblLatestVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatestVersion.Location = new System.Drawing.Point(16, 110);
            this.lblLatestVersion.Name = "lblLatestVersion";
            this.lblLatestVersion.Size = new System.Drawing.Size(92, 13);
            this.lblLatestVersion.TabIndex = 5;
            this.lblLatestVersion.Text = "Latest Version:";
            // 
            // lblInstalledVersionText
            // 
            this.lblInstalledVersionText.AutoSize = true;
            this.lblInstalledVersionText.Location = new System.Drawing.Point(106, 88);
            this.lblInstalledVersionText.Name = "lblInstalledVersionText";
            this.lblInstalledVersionText.Size = new System.Drawing.Size(0, 13);
            this.lblInstalledVersionText.TabIndex = 6;
            // 
            // lblOnlineVersionText
            // 
            this.lblOnlineVersionText.AutoSize = true;
            this.lblOnlineVersionText.Location = new System.Drawing.Point(106, 111);
            this.lblOnlineVersionText.Name = "lblOnlineVersionText";
            this.lblOnlineVersionText.Size = new System.Drawing.Size(0, 13);
            this.lblOnlineVersionText.TabIndex = 7;
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Location = new System.Drawing.Point(40, 129);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(117, 23);
            this.btnCheckUpdate.TabIndex = 8;
            this.btnCheckUpdate.Text = "Check for Updates";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.BtnCheckUpdate_Click);
            // 
            // prgbarMain
            // 
            this.prgbarMain.Location = new System.Drawing.Point(5, 43);
            this.prgbarMain.Name = "prgbarMain";
            this.prgbarMain.Size = new System.Drawing.Size(160, 10);
            this.prgbarMain.TabIndex = 9;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(194, 24);
            this.menuMain.TabIndex = 10;
            this.menuMain.Text = "menuMain";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDirectoryToolStripMenuItem,
            this.downloadPTRPackageToolStripMenuItem,
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
            this.changeDirectoryToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
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
            // downloadPTRPackageToolStripMenuItem
            // 
            this.downloadPTRPackageToolStripMenuItem.Name = "downloadPTRPackageToolStripMenuItem";
            this.downloadPTRPackageToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.downloadPTRPackageToolStripMenuItem.Text = "Download PTR Package";
            this.downloadPTRPackageToolStripMenuItem.Click += new System.EventHandler(this.downloadPTRPackageToolStripMenuItem_Click);
            // 
            // closeProgramToolStripMenuItem
            // 
            this.closeProgramToolStripMenuItem.Name = "closeProgramToolStripMenuItem";
            this.closeProgramToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.closeProgramToolStripMenuItem.Text = "Close Program";
            this.closeProgramToolStripMenuItem.Click += new System.EventHandler(this.closeProgramToolStripMenuItem_Click);
            // 
            // btnUpdateForge
            // 
            this.btnUpdateForge.Location = new System.Drawing.Point(88, 16);
            this.btnUpdateForge.Name = "btnUpdateForge";
            this.btnUpdateForge.Size = new System.Drawing.Size(77, 23);
            this.btnUpdateForge.TabIndex = 11;
            this.btnUpdateForge.Text = "Forge";
            this.btnUpdateForge.UseVisualStyleBackColor = true;
            // 
            // grpUpdate
            // 
            this.grpUpdate.Controls.Add(this.btnUpdateModpack);
            this.grpUpdate.Controls.Add(this.prgbarMain);
            this.grpUpdate.Controls.Add(this.btnUpdateForge);
            this.grpUpdate.Location = new System.Drawing.Point(12, 154);
            this.grpUpdate.Name = "grpUpdate";
            this.grpUpdate.Size = new System.Drawing.Size(170, 58);
            this.grpUpdate.TabIndex = 12;
            this.grpUpdate.TabStop = false;
            this.grpUpdate.Text = "Update..";
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(10, 38);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(175, 39);
            this.lblInstructions.TabIndex = 3;
            this.lblInstructions.Text = "Click \"Check for Updates\" below to\r\nsee if updates are available for your\r\nBuddyP" +
    "als modpack or Forge!";
            this.lblInstructions.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 216);
            this.Controls.Add(this.grpUpdate);
            this.Controls.Add(this.btnCheckUpdate);
            this.Controls.Add(this.lblOnlineVersionText);
            this.Controls.Add(this.lblInstalledVersionText);
            this.Controls.Add(this.lblLatestVersion);
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Updater";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.grpUpdate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateModpack;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label lblLatestVersion;
        private System.Windows.Forms.Label lblInstalledVersionText;
        private System.Windows.Forms.Label lblOnlineVersionText;
        private System.Windows.Forms.Button btnCheckUpdate;
        private System.Windows.Forms.ProgressBar prgbarMain;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadPTRPackageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minecraftDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pTRDownloadDirectoryToolStripMenuItem;
        private System.Windows.Forms.Button btnUpdateForge;
        private System.Windows.Forms.GroupBox grpUpdate;
        private System.Windows.Forms.Label lblInstructions;
    }
}

