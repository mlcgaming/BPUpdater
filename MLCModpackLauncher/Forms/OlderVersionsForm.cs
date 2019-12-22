using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuddyPals.Versioning;

namespace MLCModpackLauncher
{
    public partial class OlderVersionsForm : Form
    {
        VersionManifest Versions;
        VersionPackage SelectedVersion;

        public OlderVersionsForm(VersionManifest versions)
        {
            InitializeComponent();
            Versions = versions;
            SelectedVersion = null;
            SetupVersionDropdown(Versions);
        }

        private void SetupVersionDropdown(VersionManifest versions)
        {
            foreach(VersionPackage version in versions.Versions)
            {
                cmbVersions.Items.Add(version.DisplayID);
            }

            cmbVersions.SelectedIndex = 0;
        }
        private string SelectFolder(Environment.SpecialFolder rootFolder)
        {
            string startingFolder;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog { RootFolder = rootFolder };

            startingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            folderBrowserDialog.SelectedPath = startingFolder;

            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                return folderBrowserDialog.SelectedPath;
            }
            else
            {
                return startingFolder;
            }
        }
        private void DownloadFileFTP(string ftpURL, string fileDestinationPath)
        {
            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential("ftpuser", "mlcTech19!");
                request.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);
                request.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                request.DownloadFileAsync(new Uri(ftpURL), fileDestinationPath);
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            statusProgressbar.Value = e.ProgressPercentage;
            statusLabel.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            statusProgressbar.Value = 100;
            statusLabel.Text = "100%";
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            statusProgressbar.Value = 0;
            statusLabel.Text = "0%";

            string fileName = "BuddyPals_" + SelectedVersion.DisplayID + ".zip";
            string destinationDirectory = SelectFolder(Environment.SpecialFolder.Desktop);
            DownloadFileFTP(SelectedVersion.DownloadURL, Path.Combine(destinationDirectory, fileName));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(VersionPackage version in Versions.Versions)
            {
                if(version.DisplayID == cmbVersions.Text)
                {
                    SelectedVersion = version;
                }
            }

            lblVersionNameValue.Text = SelectedVersion.VersionName;
            lblForgeRequirementValue.Text = SelectedVersion.ForgeRequirement.Version;
        }
    }
}
