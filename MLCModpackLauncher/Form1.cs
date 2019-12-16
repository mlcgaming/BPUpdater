using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using MLCModpackLauncher.MojangLauncherProfile;
using MLCModpackLauncher.DirectoryFiles;
using Newtonsoft.Json;
using BuddyPals;
using BuddyPals.Versioning;

namespace MLCModpackLauncher
{
    public partial class MainForm : Form
    {
        OptionsConfiguration Options;
        VersionFile CurrentVersion, LatestVersion;

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
            Library.Initialize();

            if (File.Exists(Library.LogFilePath) == false)
            {
                var myFile = File.Create(Library.LogFilePath);
                myFile.Close();
                AppendLog("Created new updater.log file.", false);
            }

            AppendLog("\nInitializing MainForm");

            CurrentVersion = null;
            LatestVersion = null;

            AppendLog("ConfigFilePath: " + Library.UpdaterConfigFilePath);
            AppendLog("VersionFilePath: " + Library.ModpackVersionFilePath);
            AppendLog("LogFilePath: " + Library.LogFilePath);

            if (File.Exists(Library.ModpackVersionFilePath) == false)
            {
                AppendLog("No Current Modpack Version File, Creating a Dummy File");

                VersionFile newFile = new VersionFile(0, true, "N/A", "DummyFile", "", "", null, null);
                string newFileJson = JsonConvert.SerializeObject(newFile, Formatting.Indented);
                File.WriteAllText(Library.ModpackVersionFilePath, newFileJson);

                AppendLog("Dummy File Instantiated and Serialized");
            }

            trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            trayIcon.BalloonTipText = "Initializing BuddyPals Modpack Updater";
            trayIcon.BalloonTipTitle = "Initializing";
            trayIcon.ShowBalloonTip(2000);
        }
        private void InitializeOptions()
        {
            AppendLog("Checking for BuddyPals AppData at " + Library.BuddyPalsAppDataDirectory);
            if (Directory.Exists(Library.BuddyPalsAppDataDirectory) == true)
            {
                AppendLog("Folder Found, Looking for updater.conf");

                if (File.Exists(Path.Combine(Library.BuddyPalsAppDataDirectory, "config.json")) == true)
                {
                    AppendLog("Older 'config.json' found.");
                    if (File.Exists(Library.UpdaterConfigFilePath) == false)
                    {
                        AppendLog("config.json present, but no updater.conf; Converting config.json to updater.conf standard...");
                        Options = JsonConvert.DeserializeObject<OptionsConfiguration>(File.ReadAllText(Path.Combine(Library.BuddyPalsAppDataDirectory, "config.json")));
                        string newFile = JsonConvert.SerializeObject(Options, Formatting.Indented);
                        File.WriteAllText(Library.UpdaterConfigFilePath, newFile);
                        AppendLog("Success! New updater.conf located at " + Library.UpdaterConfigFilePath);
                        File.Delete(Path.Combine(Library.BuddyPalsAppDataDirectory, "config.json"));
                        AppendLog("Older config.json removed.");
                    }
                    else
                    {
                        AppendLog("Both config.json and updater.conf present. Deleting config.json.");
                        File.Delete(Path.Combine(Library.BuddyPalsAppDataDirectory, "config.json"));
                    }
                }
                else
                {
                    if (File.Exists(Library.UpdaterConfigFilePath) == false)
                    {
                        // No files at all, Make a new updater.conf file
                        AppendLog("No Configuration File Detected. Creating Default Configuration File.");
                        Options = new OptionsConfiguration();
                        UpdateConfigJSON();
                    }
                    else
                    {
                        // No config.json but we have an updater.conf; load that into memory
                        Options = JsonConvert.DeserializeObject<OptionsConfiguration>(File.ReadAllText(Library.UpdaterConfigFilePath));
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(Library.BuddyPalsAppDataDirectory);
                Options = new OptionsConfiguration();
                UpdateConfigJSON();
            }
        }
        private void CleanupAppDataDirectory()
        {
            // Check for and Remove old BPVersion file formats
            if(File.Exists(Path.Combine(Library.BuddyPalsAppDataDirectory, "MPVersion.json")) == true)
            {
                File.Delete(Path.Combine(Library.BuddyPalsAppDataDirectory, "MPVersion.json"));
            }
            AppendLog("Looking for old modpack version file at " + Path.Combine(Library.BuddyPalsAppDataDirectory, "BPVersion.json"));
            if(File.Exists(Path.Combine(Library.BuddyPalsAppDataDirectory, "BPVersion.json")) == true)
            {
                AppendLog("File Found. Checking for current modpack.ver file");
                if (File.Exists(Path.Combine(Library.BuddyPalsAppDataDirectory, "modpack.ver")) == false)
                {
                    AppendLog("No modpack.ver file found. Converting BPVersion.json to modpack.ver standard.");
                    // Use old BPVersion file to make new 'modpack.ver' file
                    LatestVersion = JsonConvert.DeserializeObject<VersionFile>(File.ReadAllText(Path.Combine(Library.BuddyPalsAppDataDirectory, "BPVersion.json")));
                    string newFile = JsonConvert.SerializeObject(LatestVersion, Formatting.Indented);
                    File.WriteAllText(Path.Combine(Library.BuddyPalsAppDataDirectory, "modpack.ver"), newFile);

                    AppendLog("Success! Removing older BPVersion.json file");

                    // Then Remove the Old File
                    File.Delete(Path.Combine(Library.BuddyPalsAppDataDirectory, "BPVersion.json"));
                }
                else
                {
                    AppendLog("Newer modpack.ver found. Removing older files.");

                    // Just remove the old BPVersion File
                    File.Delete(Path.Combine(Library.BuddyPalsAppDataDirectory, "BPVersion.json"));
                }
            }

            // Check for and Delete Old Config file formats
            if(File.Exists(Path.Combine(Library.BuddyPalsAppDataDirectory, "config.json")) == true)
            {
                File.Delete(Path.Combine(Library.BuddyPalsAppDataDirectory, "config.json"));
            }

            LatestVersion = null;
        }
        private void InitializeTooltips()
        {
            
        }
        private void AddNewForgeLauncherProfile(string forgeVersion, string installationName)
        {
            AppendLog("Creating Forge package for Launcher Profile.");
            string launcherFilePath = Path.Combine(Options.MinecraftDirectory, "launcher_profiles.json");
            MojangLauncherProfileFile launcherFile = JsonConvert.DeserializeObject<MojangLauncherProfileFile>(File.ReadAllText(launcherFilePath));
            launcherFile.AddNewProfile(forgeVersion, installationName);
            string savePath = Path.Combine(Options.MinecraftDirectory, "launcher_profiles.json");
            string json = JsonConvert.SerializeObject(launcherFile, Formatting.Indented);
            File.WriteAllText(savePath, json);
            AppendLog("Success! Updated launcher profiles located at " + savePath);
        }

        private void DownloadFileFTP(string downloadPath)
        {
            string fileName = LatestVersion.FileName;
            AppendLog("Downloading latest modpack from " + downloadPath);
            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential("ftpuser", "mlcTech19!");
                request.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);
                request.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                request.DownloadFileAsync(new Uri(LatestVersion.URL), downloadPath + fileName);
            }
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            statusMainProgressBar.Value = e.ProgressPercentage;
            lblStatus.Text = "Downloading Update.." + "(" + statusMainProgressBar.Value.ToString() + "%)";
        }
        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            AppendLog("Download completed! Proceeding to modpack update.");
            ChangeStatus("Unpacking Modpack Files");
            statusMainProgressBar.Value = 0;
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            AppendLog("Exit Button invoked.");
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

        private void openAppFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Library.BuddyPalsAppDataDirectory);
        }

        private void UpdateConfigJSON()
        {
            string optionsFile = JsonConvert.SerializeObject(Options, Formatting.Indented);
            File.WriteAllText(Library.UpdaterConfigFilePath, optionsFile);
            AppendLog(Library.UpdaterConfigFilePath + " updated.");
        }

        private void AppendLog(string logMessage, bool isNewLine = true)
        {
            string entry = "";

            if(isNewLine == true)
            {
                entry = Environment.NewLine;
                entry += $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}";
                entry += ": " + logMessage;

                File.AppendAllText(Library.LogFilePath, entry);
            }
            else
            {
                entry += $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}";
                entry += ": " + logMessage;

                File.AppendAllText(Library.LogFilePath, entry);
            }
        }

        private void ChangeStatus(string message)
        {
            lblStatus.Text = message;
            lblStatus.Refresh();
            Thread.Sleep(500);
        }
    }
}
