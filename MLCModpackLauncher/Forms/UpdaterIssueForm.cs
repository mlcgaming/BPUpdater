using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLCModpackLauncher
{
    public partial class UpdaterIssueForm : Form
    {
        public EventHandler IssueSubmitted;
        public EventHandler IssueCancelled;

        public UpdaterIssueForm()
        {
            InitializeComponent();
        }

        private void Initialize()
        {

        }

        private void ResetForm()
        {
            txtEmail.Text = "letme@know_when.its_fixed";
            txtSubject.Text = "";
            rtxtDetails.Text = "";
            cmbUrgency.SelectedIndex = 0;
            radioBug.Checked = true;
            radioFeatureRequest.Checked = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            /* Do Submission Things */
            string urgency = cmbUrgency.Text;
            string type = "BUG";
            if(radioFeatureRequest.Checked == true)
            {
                type = "REQUEST";
            }

            string title = "[" + type + "] " + txtSubject.Text;
            string body = rtxtDetails.Text;

            GithubIssueItem newIssue = new GithubIssueItem(title, body, type);
            string issueJson = JsonConvert.SerializeObject(newIssue);
            GithubIssueItem.SendTicket(issueJson);

            /* Then Close Form */
            OnIssueSubmitted(null, new EventArgs());
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnIssueSubmitted(object sender, EventArgs e)
        {
            IssueSubmitted?.Invoke(null, new EventArgs());
            MessageBox.Show("Issue Submitted to MLCGaming!");
            Close();
        }

        private void radioBug_CheckedChanged(object sender, EventArgs e)
        {
            if(radioBug.Checked == true)
            {
                radioFeatureRequest.Checked = false;
            }
        }
        private void radioFeatureRequest_CheckedChanged(object sender, EventArgs e)
        {
            if(radioFeatureRequest.Checked == true)
            {
                radioBug.Checked = false;
            }
        }
    }
}
