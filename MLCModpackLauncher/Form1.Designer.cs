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
            this.openAppFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.statusMainProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.grpLoginInfo = new System.Windows.Forms.GroupBox();
            this.lblLoginStatus = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.tboxPassword = new System.Windows.Forms.TextBox();
            this.tboxUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblVersionName = new System.Windows.Forms.Label();
            this.lblModpackVersion = new System.Windows.Forms.Label();
            this.lblModpackStatus = new System.Windows.Forms.Label();
            this.lblModpackStatusText = new System.Windows.Forms.Label();
            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.grpLoginInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(238, 24);
            this.menuMain.TabIndex = 10;
            this.menuMain.Text = "menuMain";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDirectoryToolStripMenuItem,
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
            this.changeDirectoryToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
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
            // openAppFolderToolStripMenuItem
            // 
            this.openAppFolderToolStripMenuItem.Name = "openAppFolderToolStripMenuItem";
            this.openAppFolderToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.openAppFolderToolStripMenuItem.Text = "Open App Folder";
            this.openAppFolderToolStripMenuItem.Click += new System.EventHandler(this.openAppFolderToolStripMenuItem_Click);
            // 
            // closeProgramToolStripMenuItem
            // 
            this.closeProgramToolStripMenuItem.Name = "closeProgramToolStripMenuItem";
            this.closeProgramToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.closeProgramToolStripMenuItem.Text = "Close Program";
            this.closeProgramToolStripMenuItem.Click += new System.EventHandler(this.closeProgramToolStripMenuItem_Click);
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMainProgressBar});
            this.statusMain.Location = new System.Drawing.Point(0, 169);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(238, 22);
            this.statusMain.TabIndex = 12;
            this.statusMain.Text = "TEST";
            // 
            // statusMainProgressBar
            // 
            this.statusMainProgressBar.Name = "statusMainProgressBar";
            this.statusMainProgressBar.Size = new System.Drawing.Size(175, 16);
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
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(6, 25);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(67, 13);
            this.lblUsername.TabIndex = 15;
            this.lblUsername.Text = "Username:";
            // 
            // grpLoginInfo
            // 
            this.grpLoginInfo.Controls.Add(this.lblLoginStatus);
            this.grpLoginInfo.Controls.Add(this.btnPlay);
            this.grpLoginInfo.Controls.Add(this.tboxPassword);
            this.grpLoginInfo.Controls.Add(this.tboxUsername);
            this.grpLoginInfo.Controls.Add(this.lblPassword);
            this.grpLoginInfo.Controls.Add(this.lblUsername);
            this.grpLoginInfo.Location = new System.Drawing.Point(11, 63);
            this.grpLoginInfo.Name = "grpLoginInfo";
            this.grpLoginInfo.Size = new System.Drawing.Size(214, 98);
            this.grpLoginInfo.TabIndex = 16;
            this.grpLoginInfo.TabStop = false;
            this.grpLoginInfo.Text = "Login Info";
            // 
            // lblLoginStatus
            // 
            this.lblLoginStatus.AutoSize = true;
            this.lblLoginStatus.Location = new System.Drawing.Point(7, 74);
            this.lblLoginStatus.Name = "lblLoginStatus";
            this.lblLoginStatus.Size = new System.Drawing.Size(63, 13);
            this.lblLoginStatus.TabIndex = 20;
            this.lblLoginStatus.Text = "LoginStatus";
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.Location = new System.Drawing.Point(133, 69);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 19;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // tboxPassword
            // 
            this.tboxPassword.Location = new System.Drawing.Point(79, 43);
            this.tboxPassword.Name = "tboxPassword";
            this.tboxPassword.Size = new System.Drawing.Size(129, 20);
            this.tboxPassword.TabIndex = 18;
            // 
            // tboxUsername
            // 
            this.tboxUsername.Location = new System.Drawing.Point(79, 22);
            this.tboxUsername.Name = "tboxUsername";
            this.tboxUsername.Size = new System.Drawing.Size(129, 20);
            this.tboxUsername.TabIndex = 17;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(6, 46);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(65, 13);
            this.lblPassword.TabIndex = 16;
            this.lblPassword.Text = "Password:";
            // 
            // lblVersionName
            // 
            this.lblVersionName.AutoSize = true;
            this.lblVersionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionName.Location = new System.Drawing.Point(6, 31);
            this.lblVersionName.Name = "lblVersionName";
            this.lblVersionName.Size = new System.Drawing.Size(229, 16);
            this.lblVersionName.TabIndex = 0;
            this.lblVersionName.Text = "BuddyPals Community Launcher";
            this.lblVersionName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblModpackVersion
            // 
            this.lblModpackVersion.AutoSize = true;
            this.lblModpackVersion.Location = new System.Drawing.Point(53, 47);
            this.lblModpackVersion.Name = "lblModpackVersion";
            this.lblModpackVersion.Size = new System.Drawing.Size(144, 13);
            this.lblModpackVersion.TabIndex = 1;
            this.lblModpackVersion.Text = "BuddyPals Modpack v.X.X.X";
            this.lblModpackVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblModpackStatus
            // 
            this.lblModpackStatus.AutoSize = true;
            this.lblModpackStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModpackStatus.Location = new System.Drawing.Point(8, 169);
            this.lblModpackStatus.Name = "lblModpackStatus";
            this.lblModpackStatus.Size = new System.Drawing.Size(103, 13);
            this.lblModpackStatus.TabIndex = 17;
            this.lblModpackStatus.Text = "Modpack Status:";
            // 
            // lblModpackStatusText
            // 
            this.lblModpackStatusText.AutoSize = true;
            this.lblModpackStatusText.Location = new System.Drawing.Point(117, 169);
            this.lblModpackStatusText.Name = "lblModpackStatusText";
            this.lblModpackStatusText.Size = new System.Drawing.Size(0, 13);
            this.lblModpackStatusText.TabIndex = 18;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 191);
            this.Controls.Add(this.lblModpackStatusText);
            this.Controls.Add(this.lblModpackStatus);
            this.Controls.Add(this.lblModpackVersion);
            this.Controls.Add(this.grpLoginInfo);
            this.Controls.Add(this.lblVersionName);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Updater";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.grpLoginInfo.ResumeLayout(false);
            this.grpLoginInfo.PerformLayout();
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
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripProgressBar statusMainProgressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.GroupBox grpLoginInfo;
        private System.Windows.Forms.TextBox tboxPassword;
        private System.Windows.Forms.TextBox tboxUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblLoginStatus;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label lblVersionName;
        private System.Windows.Forms.Label lblModpackVersion;
        private System.Windows.Forms.Label lblModpackStatus;
        private System.Windows.Forms.Label lblModpackStatusText;
    }
}

