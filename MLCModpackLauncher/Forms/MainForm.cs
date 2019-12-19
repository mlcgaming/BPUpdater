using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MLCModpackLauncher.Updating;
using Newtonsoft.Json;
using BuddyPals;
using BuddyPals.Versioning;

namespace MLCModpackLauncher
{
    public partial class MainForm : Form
    {
        OptionsConfiguration Options;
        VersionFile CurrentVersion, LatestVersion;
        VersionManifest MasterVersionManifest;
        MainFormState State;

        enum MainFormState
        {
            Idle,
            CheckingVersion,
            ObtainingMasterManifest
        }
        enum DownloadedItem
        {
            VersionManifest,
            VersionFile
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeMainForm();
        }

        private void closeProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void changeMinecraftDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            return;
        }
        private void minecraftDirectoryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Select Minecraft Installation Folder. This should be the folder that contains your MODS and CONFIGS folders.");
            Options.SetMCDirectory(SelectFolder(Environment.SpecialFolder.ApplicationData));
            MessageBox.Show("Minecraft Install Directory set to " + Options.MinecraftDirectory);
            UpdateConfigJSON();
        }
        private void pTRDownloadDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select PTR Installation Folder. This should be a folder separate from your main Minecraft Installation.");
            Options.SetPTRDirectory(SelectFolder(Environment.SpecialFolder.Desktop));
            MessageBox.Show("PTR Directory Now Set to " + Options.PTRDirectory);
            UpdateConfigJSON();
        }

        private void InitializeMainForm()
        {
            ResetForm();
            State = MainFormState.ObtainingMasterManifest;
            CheckMasterVersionManifest();
            CheckForUpdate();
        }
        private void InitializeOptions()
        {
            if(File.Exists(Library.UpdaterConfigFilePath) == false)
            {
                OptionsConfiguration options = new OptionsConfiguration();
                string optionJson = JsonConvert.SerializeObject(options, Formatting.Indented);
                File.WriteAllText(Library.UpdaterConfigFilePath, optionJson);
            }

            Options = JsonConvert.DeserializeObject<OptionsConfiguration>(File.ReadAllText(Library.UpdaterConfigFilePath));
        }
        private void CleanupDirectories()
        {
            List<string> trashFiles = new List<string>
            {
                Path.Combine(Library.UpdaterDirectory, "config.json"),
                Path.Combine(Library.UpdaterDirectory, "MPVersion.json"),
                Path.Combine(Library.UpdaterDirectory, "BPVersion.json"),
                Path.Combine(Library.UpdaterDirectory, "modpack.ver"),
                Path.Combine(Library.UpdaterDirectory, "settings.json"),
                Path.Combine(Library.RootDirectory, "config.json"),
                Path.Combine(Library.RootDirectory, "MPVersion.json"),
                Path.Combine(Library.RootDirectory, "BPVersion.json"),
                Path.Combine(Library.RootDirectory, "modpack.ver"),
                Path.Combine(Library.RootDirectory, "updater.log"),
                Path.Combine(Library.RootDirectory, "updater.conf")
            };

            foreach(string file in trashFiles)
            {
                if(File.Exists(file) == true)
                {
                    File.Delete(file);
                }
            }
        }
        private void CheckMasterVersionManifest()
        {
            if(File.Exists(Path.Combine(Library.UpdaterDirectory, "versions")) == true)
            {
                File.Delete(Path.Combine(Library.UpdaterDirectory, "versions"));
            }
            DownloadFileFTP("ftp://mc.mlcgaming.com/modpack/bin/repository/versions", Path.Combine(Library.UpdaterDirectory, "versions"),
                DownloadedItem.VersionManifest);
        }
        private void CheckForUpdate()
        {
            if(File.Exists(Path.Combine(Library.UpdaterDirectory, "stable.ver")) == true)
            {
                File.Delete(Path.Combine(Library.UpdaterDirectory, "stable.ver"));
            }
            if (File.Exists(Path.Combine(Library.UpdaterDirectory, "ptr.ver")) == true)
            {
                File.Delete(Path.Combine(Library.UpdaterDirectory, "ptr.ver"));
            }

            State = MainFormState.CheckingVersion;
            DownloadFileFTP("ftp://mc.mlcgaming.com/modpack/bin/stable/client.ver", Path.Combine(Library.UpdaterDirectory, "stable.ver"),
                DownloadedItem.VersionFile);
        }
        private void CompareVersions()
        {
            LatestVersion = JsonConvert.DeserializeObject<VersionFile>(File.ReadAllText(Library.UpdaterDirectory + "stable.ver"));
            if (LatestVersion.ID != CurrentVersion.ID)
            {
                lblUpdateAvailable.Visible = true;
                lblUpdateToVersion.Text = "Update to Version " + LatestVersion.DisplayID + "?";
                lblUpdateToVersion.Visible = true;
                btnApplyUpdate.Visible = true;
                btnApplyUpdate.Enabled = true;
                Height = 211;
            }
            else
            {
                lblUpdateAvailable.Text = "Modpack is Up-To-Date!";
                lblUpdateAvailable.Visible = true;
                Height = 169;
            }

            State = MainFormState.Idle;
        }
        
        private void DownloadFileFTP(string ftpURL, string fileDestinationPath, DownloadedItem item)
        {
            AppendLog("Downloading from " + ftpURL + " to " + fileDestinationPath);
            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential("ftpuser", "mlcTech19!");
                switch (item)
                {
                    case DownloadedItem.VersionFile:
                        {
                            request.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadVersionFileCompleted);
                            break;
                        }
                    case DownloadedItem.VersionManifest:
                        {
                            request.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadVersionManifestCompleted);
                            break;
                        }
                }
                request.DownloadFileAsync(new Uri(ftpURL), fileDestinationPath);
            }
        }
        private void DownloadVersionFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Thread.Sleep(1000);
            CompareVersions();
        }
        private void DownloadVersionManifestCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Thread.Sleep(1000);
            MasterVersionManifest = JsonConvert.DeserializeObject<VersionManifest>(File.ReadAllText(Path.Combine(Library.UpdaterDirectory, "versions")));
            State = MainFormState.Idle;
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            AppendLog("Exit Button invoked.");
        }
        private void openAppFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Library.RootDirectory);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        private string SelectFolder(Environment.SpecialFolder rootFolder)
        {
            string startingFolder;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog { RootFolder = rootFolder };

            if (rootFolder == Environment.SpecialFolder.Desktop)
            {
                startingFolder = Options.PTRDirectory;
                folderBrowserDialog.SelectedPath = Options.PTRDirectory;
            }
            else
            {
                startingFolder = Options.MinecraftDirectory;
                folderBrowserDialog.SelectedPath = Options.MinecraftDirectory;
            }

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
        private void UpdateConfigJSON()
        {
            string optionsFile = JsonConvert.SerializeObject(Options, Formatting.Indented);
            File.WriteAllText(Library.UpdaterConfigFilePath, optionsFile);
            AppendLog(Library.UpdaterConfigFilePath + " updated.");
        }

        private void ResetForm()
        {
            State = MainFormState.Idle;
            HideStatus();
            lblUpdateAvailable.Visible = false;
            lblUpdateToVersion.Visible = false;
            btnApplyUpdate.Enabled = false;
            btnApplyUpdate.Visible = false;

            Library.Initialize();
            CleanupDirectories();

            if (Directory.Exists(Library.UpdaterDirectory) == false)
            {
                Directory.CreateDirectory(Library.UpdaterDirectory);
            }
            if (File.Exists(Library.UpdaterLogFilePath) == false)
            {
                var myFile = File.Create(Library.UpdaterLogFilePath);
                myFile.Close();
                AppendLog("Created new updater.log file.", false);
            }
            if (File.Exists(Library.UpdaterVersionFilePath) == false)
            {
                VersionFile newVersion = new VersionFile(0, "0.0.0", true, "", "", "", new Dictionary<string, bool>(), null, null);
                string newVersionString = JsonConvert.SerializeObject(newVersion, Formatting.Indented);
                File.WriteAllText(Library.UpdaterVersionFilePath, newVersionString);
            }

            CurrentVersion = JsonConvert.DeserializeObject<VersionFile>(File.ReadAllText(Library.UpdaterVersionFilePath));
            InitializeOptions();

            lblModpackVersion.Text = "Currently Running v" + CurrentVersion.DisplayID;
        }
        private void AppendLog(string logMessage, bool isNewLine = true)
        {
            string entry = "";

            if (isNewLine == true)
            {
                entry = Environment.NewLine;
                entry += $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}";
                entry += ": " + logMessage;

                File.AppendAllText(Library.UpdaterLogFilePath, entry);
            }
            else
            {
                entry += $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}";
                entry += ": " + logMessage;

                File.AppendAllText(Library.UpdaterLogFilePath, entry);
            }
        }
        private void HideStatus()
        {
            
        }
        private void ShowStatus()
        {
            
        }

        private void btnApplyUpdate_Click(object sender, EventArgs e)
        {
            btnApplyUpdate.Enabled = false;
            btnApplyUpdate.Visible = false;
            Updater.Setup(LatestVersion.Manifest, Options.MinecraftDirectory, false, new UpdaterForm(LatestVersion.DisplayID));
            Updater.PerformUpdate();
            
            if(File.Exists(Library.UpdaterVersionFilePath) == true)
            {
                File.Delete(Library.UpdaterVersionFilePath);
            }

            string latestJson = JsonConvert.SerializeObject(LatestVersion, Formatting.Indented);
            File.WriteAllText(Library.UpdaterVersionFilePath, latestJson);

            ResetForm();
           State = MainFormState.CheckingVersion;
        }
        private void aboutBuddyPalsUpdaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open AboutForm as Dialog
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }
        private void modModpackIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Submit a Mod/Modpack Issue as an Email to max@mlcgaming.com
        }
        private void updaterIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an Issue on the BPUpdater Github page
        }
        private void generalInquiryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Email to max@mlcgaming.com
        }
        private void downloadOlderVersionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open OlderVersionsForm
            OlderVersionsForm olderVersions = new OlderVersionsForm(MasterVersionManifest);
            olderVersions.ShowDialog();
        }

        private void ChangeStatus(string message)
        {
            lblStatus.Text = message;
            lblStatus.Refresh();
            Thread.Sleep(500);
        }
    }
}
