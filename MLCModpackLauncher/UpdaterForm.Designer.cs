namespace MLCModpackLauncher
{
    partial class UpdaterForm
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
            this.progressCurrentItem = new System.Windows.Forms.ProgressBar();
            this.progressTotal = new System.Windows.Forms.ProgressBar();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCurrentItem = new System.Windows.Forms.Label();
            this.lblOverallProgress = new System.Windows.Forms.Label();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressCurrentItem
            // 
            this.progressCurrentItem.Location = new System.Drawing.Point(12, 57);
            this.progressCurrentItem.Name = "progressCurrentItem";
            this.progressCurrentItem.Size = new System.Drawing.Size(250, 23);
            this.progressCurrentItem.TabIndex = 0;
            // 
            // progressTotal
            // 
            this.progressTotal.Location = new System.Drawing.Point(12, 102);
            this.progressTotal.Name = "progressTotal";
            this.progressTotal.Size = new System.Drawing.Size(250, 23);
            this.progressTotal.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(274, 25);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Updating to X.Y.Z";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentItem
            // 
            this.lblCurrentItem.AutoSize = true;
            this.lblCurrentItem.Location = new System.Drawing.Point(12, 41);
            this.lblCurrentItem.Name = "lblCurrentItem";
            this.lblCurrentItem.Size = new System.Drawing.Size(61, 13);
            this.lblCurrentItem.TabIndex = 3;
            this.lblCurrentItem.Text = "CurrentItem";
            // 
            // lblOverallProgress
            // 
            this.lblOverallProgress.AutoSize = true;
            this.lblOverallProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverallProgress.Location = new System.Drawing.Point(12, 86);
            this.lblOverallProgress.Name = "lblOverallProgress";
            this.lblOverallProgress.Size = new System.Drawing.Size(104, 13);
            this.lblOverallProgress.TabIndex = 4;
            this.lblOverallProgress.Text = "Overall Progress:";
            // 
            // lblStatusText
            // 
            this.lblStatusText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusText.Location = new System.Drawing.Point(0, 130);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(274, 20);
            this.lblStatusText.TabIndex = 5;
            this.lblStatusText.Text = "StatusText";
            this.lblStatusText.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // UpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 150);
            this.Controls.Add(this.lblStatusText);
            this.Controls.Add(this.lblOverallProgress);
            this.Controls.Add(this.lblCurrentItem);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.progressTotal);
            this.Controls.Add(this.progressCurrentItem);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdaterForm";
            this.Text = "Updating Modpack";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressCurrentItem;
        private System.Windows.Forms.ProgressBar progressTotal;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCurrentItem;
        private System.Windows.Forms.Label lblOverallProgress;
        private System.Windows.Forms.Label lblStatusText;
    }
}