namespace MLCModpackLauncher
{
    partial class UpdaterIssueForm
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
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.grpIssueTicket = new System.Windows.Forms.GroupBox();
            this.radioFeatureRequest = new System.Windows.Forms.RadioButton();
            this.radioBug = new System.Windows.Forms.RadioButton();
            this.cmbUrgency = new System.Windows.Forms.ComboBox();
            this.lblUrgency = new System.Windows.Forms.Label();
            this.rtxtDetails = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblSubmitee = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpIssueTicket.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(331, 23);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Submit An Issue";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(6, 21);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(46, 13);
            this.lblSubject.TabIndex = 1;
            this.lblSubject.Text = "Subject:";
            // 
            // grpIssueTicket
            // 
            this.grpIssueTicket.Controls.Add(this.radioFeatureRequest);
            this.grpIssueTicket.Controls.Add(this.radioBug);
            this.grpIssueTicket.Controls.Add(this.cmbUrgency);
            this.grpIssueTicket.Controls.Add(this.lblUrgency);
            this.grpIssueTicket.Controls.Add(this.rtxtDetails);
            this.grpIssueTicket.Controls.Add(this.label1);
            this.grpIssueTicket.Controls.Add(this.txtEmail);
            this.grpIssueTicket.Controls.Add(this.lblSubmitee);
            this.grpIssueTicket.Controls.Add(this.txtSubject);
            this.grpIssueTicket.Controls.Add(this.lblSubject);
            this.grpIssueTicket.Location = new System.Drawing.Point(12, 26);
            this.grpIssueTicket.Name = "grpIssueTicket";
            this.grpIssueTicket.Size = new System.Drawing.Size(307, 216);
            this.grpIssueTicket.TabIndex = 2;
            this.grpIssueTicket.TabStop = false;
            this.grpIssueTicket.Text = "Issue Ticket";
            // 
            // radioFeatureRequest
            // 
            this.radioFeatureRequest.AutoSize = true;
            this.radioFeatureRequest.Location = new System.Drawing.Point(197, 190);
            this.radioFeatureRequest.Name = "radioFeatureRequest";
            this.radioFeatureRequest.Size = new System.Drawing.Size(104, 17);
            this.radioFeatureRequest.TabIndex = 10;
            this.radioFeatureRequest.Text = "Feature Request";
            this.radioFeatureRequest.UseVisualStyleBackColor = true;
            this.radioFeatureRequest.CheckedChanged += new System.EventHandler(this.radioFeatureRequest_CheckedChanged);
            // 
            // radioBug
            // 
            this.radioBug.AutoSize = true;
            this.radioBug.Checked = true;
            this.radioBug.Location = new System.Drawing.Point(197, 167);
            this.radioBug.Name = "radioBug";
            this.radioBug.Size = new System.Drawing.Size(80, 17);
            this.radioBug.TabIndex = 9;
            this.radioBug.TabStop = true;
            this.radioBug.Text = "Bug / Issue";
            this.radioBug.UseVisualStyleBackColor = true;
            this.radioBug.CheckedChanged += new System.EventHandler(this.radioBug_CheckedChanged);
            // 
            // cmbUrgency
            // 
            this.cmbUrgency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUrgency.FormattingEnabled = true;
            this.cmbUrgency.Items.AddRange(new object[] {
            "Normal",
            "High"});
            this.cmbUrgency.Location = new System.Drawing.Point(58, 166);
            this.cmbUrgency.Name = "cmbUrgency";
            this.cmbUrgency.Size = new System.Drawing.Size(81, 21);
            this.cmbUrgency.TabIndex = 8;
            // 
            // lblUrgency
            // 
            this.lblUrgency.AutoSize = true;
            this.lblUrgency.Location = new System.Drawing.Point(6, 169);
            this.lblUrgency.Name = "lblUrgency";
            this.lblUrgency.Size = new System.Drawing.Size(50, 13);
            this.lblUrgency.TabIndex = 7;
            this.lblUrgency.Text = "Urgency:";
            // 
            // rtxtDetails
            // 
            this.rtxtDetails.Location = new System.Drawing.Point(58, 64);
            this.rtxtDetails.Name = "rtxtDetails";
            this.rtxtDetails.Size = new System.Drawing.Size(243, 96);
            this.rtxtDetails.TabIndex = 6;
            this.rtxtDetails.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Details:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(58, 41);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(243, 20);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.Text = "letme@know_when.its_fixed";
            // 
            // lblSubmitee
            // 
            this.lblSubmitee.AutoSize = true;
            this.lblSubmitee.Location = new System.Drawing.Point(6, 44);
            this.lblSubmitee.Name = "lblSubmitee";
            this.lblSubmitee.Size = new System.Drawing.Size(35, 13);
            this.lblSubmitee.TabIndex = 3;
            this.lblSubmitee.Text = "Email:";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(58, 18);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(243, 20);
            this.txtSubject.TabIndex = 2;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(12, 248);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(244, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UpdaterIssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 279);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.grpIssueTicket);
            this.Controls.Add(this.lblFormTitle);
            this.Name = "UpdaterIssueForm";
            this.Text = "Issue Form";
            this.grpIssueTicket.ResumeLayout(false);
            this.grpIssueTicket.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.GroupBox grpIssueTicket;
        private System.Windows.Forms.RadioButton radioFeatureRequest;
        private System.Windows.Forms.RadioButton radioBug;
        private System.Windows.Forms.ComboBox cmbUrgency;
        private System.Windows.Forms.Label lblUrgency;
        private System.Windows.Forms.RichTextBox rtxtDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblSubmitee;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
    }
}