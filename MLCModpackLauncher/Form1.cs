using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MLCModpackLauncher.MojangLauncherProfile;
using Newtonsoft.Json;

namespace MLCModpackLauncher
{
    public partial class MainForm : Form
    {
        OptionsConfiguration Options;
        ModpackVerFile CurrentVersion, LatestVersion;
        bool IsDownloadingPTR;
        string ConfigFilePath, VersionFilePath, LogFilePath;
        ToolTip CurrentVersionTooltip, LatestVersionTooltip, CheckForUpdateButtonTooltip, UpdateModpackTooltip;
        int ProgressCounter, ProgressCompleted;

        public MainForm()
        {
            InitializeComponent();
            InitializeMainForm();
        }

        private void BtnCheckUpdate_Click(object sender, EventArgs e)
        {
            CheckForUpdate();
        }
        private void BtnApplyUpdate_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Downloading Update...";
            ShowStatus();
            LatestVersion.SetModpackFolders();
            menuMain.Enabled = false;
            DownloadFileFTP(Options.AppDirectory);
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }
        private void closeProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }
        private void changeMinecraftDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            return;
        }
        private void downloadPTRPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsDownloadingPTR = true;
            menuMain.Enabled = false;
            LatestVersion = DownloadMPVersionJson("ftp://mc.mlcgaming.com/modpack/PTR/modpack.ver");

            if (LatestVersion.IsActive == true)
            {
                MessageBox.Show("PTR Package will download to " + Options.PTRDirectory);
                DownloadFileFTP(Options.PTRDirectory);
            }
            else
            {
                MessageBox.Show("PTR is not Currently Active. Check back later!");
                LatestVersion = null;
                menuMain.Enabled = true;
                IsDownloadingPTR = false;
                return;
            }
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
            MessageBox.Show("Select a Download Path for PTR files. PTR files download as a ZIP that needs to be extracted and moved into your PTR build on your own.");
            Options.SetPTRDirectory(SelectFolder(Environment.SpecialFolder.Desktop));
            MessageBox.Show("PTR Download Path Now Set to " + Options.PTRDirectory);
            UpdateConfigJSON();
        }

        private void InitializeMainForm()
        {
            HideStatus();

            ConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\") + "updater.conf";
            VersionFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\") + "modpack.ver";
            LogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\") + "updater.log";

            if (File.Exists(LogFilePath) == false)
            {
                var myFile = File.Create(LogFilePath);
                myFile.Close();
                AppendLog("Created new updater.log file.", false);
            }

            AppendLog("Initializing MainForm");

            btnUpdateModpack.Enabled = false;
            CurrentVersion = null;
            LatestVersion = null;
            IsDownloadingPTR = false;

            AppendLog("ConfigFilePath: " + ConfigFilePath);
            AppendLog("VersionFilePath: " + VersionFilePath);
            AppendLog("LogFilePath: " + LogFilePath);

            if (File.Exists(VersionFilePath) == false)
            {
                AppendLog("No Current Modpack Version File, Creating a Dummy File");

                ModpackVerFile newFile = new ModpackVerFile(0, "N/A", "N/A");
                string newFileJson = JsonConvert.SerializeObject(newFile, Formatting.Indented);
                File.WriteAllText(VersionFilePath, newFileJson);

                AppendLog("Dummy File Instantiated and Serialized");
            }

            try
            {
                // Check for older VersionFile formats and convert to a new ModpackVerFile object
                AppendLog("Reading " + VersionFilePath);
                VersionFile oldVersionFile = JsonConvert.DeserializeObject<VersionFile>(File.ReadAllText(VersionFilePath));
                if(oldVersionFile != null)
                {
                    AppendLog("modpack.ver is old format. Updating to new format.");
                    File.Delete(VersionFilePath);
                    ModpackVerFile newVersionFile = ModpackVerFile.ConvertFromVersionFile(oldVersionFile);
                    string newVersionFileJson = JsonConvert.SerializeObject(newVersionFile, Formatting.Indented);
                    File.WriteAllText(VersionFilePath, newVersionFileJson);
                    AppendLog(VersionFilePath + " updated to new format.");
                }
            }
            finally
            {
                InitializeOptions();
                CleanupAppDataDirectory();
                InitializeTooltips();
            }
        }
        private void InitializeOptions()
        {
            string optionsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\");

            AppendLog("Checking for BuddyPals AppData at " + optionsDirectory);

            if (Directory.Exists(optionsDirectory) == true)
            {
                AppendLog("Folder Found, Looking for updater.conf");

                if (File.Exists(Path.Combine(optionsDirectory, "config.json")) == true)
                {
                    AppendLog("Older 'config.json' found.");

                    if (File.Exists(Path.Combine(optionsDirectory, "updater.conf")) == false)
                    {
                        AppendLog("config.json present, but no updater.conf; Converting config.json to updater.conf standard...");
                        // Use old config file to make new updater.settings file
                        Options = JsonConvert.DeserializeObject<OptionsConfiguration>(File.ReadAllText(Path.Combine(optionsDirectory, "config.json")));
                        string newFile = JsonConvert.SerializeObject(Options, Formatting.Indented);
                        File.WriteAllText(Path.Combine(optionsDirectory, "updater.conf"), newFile);

                        AppendLog("Success! New updater.conf located at " + Path.Combine(optionsDirectory, "updater.conf"));

                        // Then Remove the Old File
                        File.Delete(Path.Combine(optionsDirectory, "config.json"));

                        AppendLog("Older config.json removed.");
                    }
                    else
                    {
                        // Just remove the old Config File
                        AppendLog("Both config.json and updater.conf present. Deleting config.json.");
                        File.Delete(Path.Combine(optionsDirectory, "config.json"));
                    }
                }
                else
                {
                    AppendLog("No Configuration File Detected. Creating Default Configuration File.");
                    Options = new OptionsConfiguration();
                    UpdateConfigJSON();
                }
            }
            else
            {
                Directory.CreateDirectory(optionsDirectory);
                Options = new OptionsConfiguration();
                UpdateConfigJSON();
            }
        }
        private void CleanupAppDataDirectory()
        {
            // Check for and Remove old BPVersion file formats
            AppendLog("Looking for old modpack version file at " + Path.Combine(Options.AppDirectory, "BPVersion.json"));
            if(File.Exists(Path.Combine(Options.AppDirectory, "BPVersion.json")) == true)
            {
                AppendLog("File Found. Checking for current modpack.ver file");
                if (File.Exists(Path.Combine(Options.AppDirectory, "modpack.ver")) == false)
                {
                    AppendLog("No modpack.ver file found. Converting BPVersion.json to modpack.ver standard.");
                    // Use old BPVersion file to make new 'modpack.ver' file
                    LatestVersion = JsonConvert.DeserializeObject<ModpackVerFile>(File.ReadAllText(Path.Combine(Options.AppDirectory, "BPVersion.json")));
                    string newFile = JsonConvert.SerializeObject(LatestVersion, Formatting.Indented);
                    File.WriteAllText(Path.Combine(Options.AppDirectory, "modpack.ver"), newFile);

                    AppendLog("Success! Removing older BPVersion.json file");

                    // Then Remove the Old File
                    File.Delete(Path.Combine(Options.AppDirectory, "BPVersion.json"));
                }
                else
                {
                    AppendLog("Newer modpack.ver found. Removing older files.");

                    // Just remove the old BPVersion File
                    File.Delete(Path.Combine(Options.AppDirectory, "BPVersion.json"));
                }
            }

            // Check for and Delete Old Config file formats
            if(File.Exists(Path.Combine(Options.AppDirectory, "config.json")) == true)
            {
                File.Delete(Path.Combine(Options.AppDirectory, "config.json"));
            }

            LatestVersion = null;
        }
        private void InitializeTooltips()
        {
            CurrentVersionTooltip = new ToolTip();
            LatestVersionTooltip = new ToolTip();
            CheckForUpdateButtonTooltip = new ToolTip();
            UpdateModpackTooltip = new ToolTip();

            CurrentVersionTooltip.IsBalloon = true;
            CurrentVersionTooltip.ShowAlways = true;
            CurrentVersionTooltip.SetToolTip(lblCurrentVersion, "This is the version of the BuddyPals Modpack detected on your machine, using the Minecraft Directory defined in the Options above.");

            LatestVersionTooltip.IsBalloon = true;
            LatestVersionTooltip.ShowAlways = true;
            LatestVersionTooltip.SetToolTip(lblLatestVersion, "This is the version of the BuddyPals Modpack currently being deployed by the master server. If it does not match your installed version, you'll have the option to update below.");

            CheckForUpdateButtonTooltip.IsBalloon = true;
            CheckForUpdateButtonTooltip.ShowAlways = true;
            CheckForUpdateButtonTooltip.SetToolTip(btnCheckUpdate, "Check the master server for the latest updates to the BuddyPals Community Modpack");

            UpdateModpackTooltip.IsBalloon = true;
            UpdateModpackTooltip.ShowAlways = true;
            UpdateModpackTooltip.SetToolTip(btnUpdateModpack, "Install the most recent modpack files!");
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
            if (IsDownloadingPTR == false)
            {
                ProgressCounter = 1;
                ProgressCompleted = 0;

                foreach(var key in LatestVersion.ToBeUpdated.Keys)
                {
                    if(LatestVersion.ToBeUpdated[key] == true)
                    {
                        ProgressCounter += 1;
                    }
                }

                AppendLog("Download completed! Proceeding to modpack update.");
                lblStatus.Text = "Unpacking Modpack Files";
                statusMainProgressBar.Value = 0;
                Thread.Sleep(2000);
                UpdateModpack();
            }
            else
            {
                AppendLog("PTR Files downloaded successfully. Returning to main thread.");
                MessageBox.Show("PTR Zip Downloaded!");
                string optionsFile = JsonConvert.SerializeObject(Options, Formatting.Indented);
                File.WriteAllText(Options.AppDirectory + "/config.json", optionsFile);
                IsDownloadingPTR = false;
                statusMainProgressBar.Value = 0;
                menuMain.Enabled = true;
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            AppendLog("Exit Button invoked.");
            ExitProgram();
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
            string startingFolder = "";

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = rootFolder;

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
            Process.Start("explorer.exe", Options.AppDirectory);
        }

        private void UpdateConfigJSON()
        {
            string optionsFile = JsonConvert.SerializeObject(Options, Formatting.Indented);
            File.WriteAllText(ConfigFilePath, optionsFile);
            AppendLog(ConfigFilePath + " updated.");
        }
        private void CheckForUpdate()
        {
            AppendLog("Downloading modpack.ver from Master Server to check for update.");
            // DOWNLOAD MC.MLCGAMING.COM/DOWNLOADS/MPVERSION.JSON
            // PARSE FILE INTO VERSIONINFO OBJECT (LATESTVERSION)
            LatestVersion = DownloadMPVersionJson("ftp://mc.mlcgaming.com/modpack/modpack.ver");
            AppendLog("Success!");
            // LOOK FOR MPVERSION.JSON IN APPDATA\BUDDYPALS\ FOLDER
            AppendLog("Checking for current modpack.ver file at " + VersionFilePath);
            if (File.Exists(VersionFilePath) == true)
            {
                AppendLog(VersionFilePath + " found! Loading current version file.");
                CurrentVersion = JsonConvert.DeserializeObject<ModpackVerFile>(File.ReadAllText(VersionFilePath));
            }
            else
            {
                AppendLog("No current modpack.ver found. Creating a dummy file.");
                CurrentVersion = new ModpackVerFile(0, "N/A", "NULL");
            }

            // IF NONE, ASSUME NO MODPACK 
            // IF EXISTS, PARSE IT INTO VERSIONINFO OBJECT (CURRENTVERSION)
            // IF CURRENTVERSION IS NULL OR DIFFERS FROM LATESTVERSION,
            //  ENABLE APPLYUPDATE BUTTON AND DISPLAY VERSIONS

            if (CurrentVersion.ID != LatestVersion.ID)
            {
                btnUpdateModpack.Enabled = true;
            }
            else
            {
                btnUpdateModpack.Enabled = false;
            }

            if (CurrentVersion == null)
            {
                lblInstalledVersionText.Text = "N/A";
            }
            else
            {
                lblInstalledVersionText.Text = CurrentVersion.Text;
            }

            lblOnlineVersionText.Text = LatestVersion.Text;
        }
        private void UpdateModpack()
        {
            // Unzip the Latest Modpack
            string zipFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\" + LatestVersion.FileName;
            string extractionPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\" + "bin";

            AppendLog("Logging modpack file as " + zipFilePath);
            AppendLog("Logging extraction path as " + extractionPath);

            AppendLog("Checking for Extraction Path.");
            if (Directory.Exists(extractionPath) == true)
            {
                AppendLog("Extraction Path Already Exists. Clearing out old data.");
                Directory.Delete(extractionPath, true);
            }

            AppendLog("Unzipping Modpack into Extraction Path");
            ZipFile.ExtractToDirectory(zipFilePath, extractionPath);

            // DELETE MODS AND CONFIG FOLDERS
            string minecraftModsDirectory = Options.MinecraftDirectory + "mods";
            string minecraftConfigDirectory = Options.MinecraftDirectory + "config";
            string minecraftPackDirectory = Options.MinecraftDirectory + "resourcepacks";
            string minecraftShaderDirectory = Options.MinecraftDirectory + "shaderpacks";
            string minecraftScriptsDirectory = Options.MinecraftDirectory + "scripts";
            string minecraftForgeJsonDirectory = Path.Combine(Options.MinecraftDirectory, "versions");
            string minecraftForgeJarDirectory = Path.Combine(Options.MinecraftDirectory, "libraries\\net\\minecraftforge\\forge\\");

            AppendLog("Logging Minecraft Mods Directory as " + minecraftModsDirectory);
            AppendLog("Logging Minecraft Config Directory as " + minecraftConfigDirectory);
            AppendLog("Logging Minecraft Resource Pack Directory as " + minecraftPackDirectory);
            AppendLog("Logging Minecraft Shaders Directory as " + minecraftShaderDirectory);
            AppendLog("Logging Minecraft Scripts Directory as " + minecraftScriptsDirectory);
            AppendLog("Logging Minecraft Forge JSON Directory as " + minecraftForgeJsonDirectory);
            AppendLog("Logging Minecraft Forge JAR Directory as " + minecraftForgeJarDirectory);

            AppendLog("Checking if modpack includes mods");
            if (LatestVersion.ToBeUpdated["mods"] == true)
            {
                lblStatus.Text = "Updating Mods";
                AppendLog("Mods Included.  Logging new mods directory as " + LatestVersion.ModpackFolders["mods"]);
                AppendLog("Checking for existing mod folder at " + minecraftModsDirectory);
                if (Directory.Exists(minecraftModsDirectory) == true)
                {
                    AppendLog("Folder found. Removing folder.");
                    Directory.Delete(minecraftModsDirectory, true);
                }

                AppendLog("Attempting to move folder from " + LatestVersion.ModpackFolders["mods"] + " to " + minecraftModsDirectory);
                Directory.Move(LatestVersion.ModpackFolders["mods"], minecraftModsDirectory);

                lblStatus.Text = "Mods Updated";
                ProgressCompleted += 1;
                statusMainProgressBar.Value = GetProgressComplete();
                Thread.Sleep(2000);
            }
            AppendLog("Checking if modpack includes config");
            if (LatestVersion.ToBeUpdated["config"] == true)
            {
                lblStatus.Text = "Updating Configs";
                AppendLog("Config Included.  Logging new config directory as " + LatestVersion.ModpackFolders["config"]);
                AppendLog("Checking for existing folder at " + minecraftConfigDirectory);
                if (Directory.Exists(minecraftConfigDirectory) == true)
                {
                    AppendLog("Folder found. Removing folder.");
                    Directory.Delete(minecraftConfigDirectory, true);
                }

                AppendLog("Attempting to move folder from " + LatestVersion.ModpackFolders["config"] + " to " + minecraftConfigDirectory);
                Directory.Move(LatestVersion.ModpackFolders["config"], minecraftConfigDirectory);
                AppendLog("Move Successful!");

                lblStatus.Text = "Configs Updated";
                ProgressCompleted += 1;
                statusMainProgressBar.Value = GetProgressComplete();
                Thread.Sleep(2000);
            }
            AppendLog("Checking if modpack includes Resource Packs");
            if (LatestVersion.ToBeUpdated["resourcePacks"] == true)
            {
                lblStatus.Text = "Installing Resource Packs";
                AppendLog("Resource Pack Included.  Logging new resourcepacks directory as " + LatestVersion.ModpackFolders["resourcePacks"]);

                AppendLog("Checking for existing folder at " + minecraftPackDirectory);
                if (Directory.Exists(minecraftPackDirectory) == false)
                {
                    AppendLog("No such path found. Creating new directory.");
                    Directory.CreateDirectory(minecraftPackDirectory);
                }

                AppendLog("Moving Files from " + LatestVersion.ModpackFolders["resourcePacks"] + " to " + minecraftPackDirectory);
                foreach (var file in Directory.GetFiles(LatestVersion.ModpackFolders["resourcePacks"]))
                {
                    if(File.Exists(file) == false)
                    {
                        AppendLog("Moving " + Path.GetFileName(file) + " to " + minecraftPackDirectory);
                        File.Copy(file, Path.Combine(minecraftPackDirectory, Path.GetFileName(file)));
                    }
                }

                AppendLog("Move Successful!");

                lblStatus.Text = "Resource Pack Installed";
                ProgressCompleted += 1;
                statusMainProgressBar.Value = GetProgressComplete();
                Thread.Sleep(2000);
            }
            AppendLog("Checking if modpack includes Shaders");
            if (LatestVersion.ToBeUpdated["shaderPacks"] == true)
            {
                lblStatus.Text = "Installing Shaders";
                AppendLog("Shaders Included.  Logging new shaders directory as " + LatestVersion.ModpackFolders["shaderPacks"]);

                AppendLog("Checking for existing folder at " + minecraftShaderDirectory);
                if (Directory.Exists(minecraftShaderDirectory) == false)
                {
                    AppendLog("No such path found. Creating new directory.");
                    Directory.CreateDirectory(minecraftShaderDirectory);
                }

                AppendLog("Moving Files from " + LatestVersion.ModpackFolders["shaderPacks"] + " to " + minecraftShaderDirectory);
                foreach (var file in Directory.GetFiles(LatestVersion.ModpackFolders["shaderPacks"]))
                {
                    if(File.Exists(file) == false)
                    {
                        AppendLog("Moving " + Path.GetFileName(file) + " to " + minecraftShaderDirectory);
                        File.Copy(file, Path.Combine(minecraftShaderDirectory, Path.GetFileName(file)));
                    }
                }

                AppendLog("Move Successful!");

                lblStatus.Text = "Shaders Installed";
                ProgressCompleted += 1;
                statusMainProgressBar.Value = GetProgressComplete();
                Thread.Sleep(2000);
            }
            AppendLog("Checking if modpack includes Scripts");
            if (LatestVersion.ToBeUpdated["scripts"] == true)
            {
                lblStatus.Text = "Installing Scripts";
                AppendLog("Scripts Included.  Logging new scripts directory as " + LatestVersion.ModpackFolders["scripts"]);

                AppendLog("Checking for existing directory " + minecraftScriptsDirectory);
                if (Directory.Exists(minecraftScriptsDirectory) == true)
                {
                    AppendLog("Removing " + minecraftScriptsDirectory);
                    Directory.Delete(minecraftScriptsDirectory, true);
                }

                AppendLog("Copying " + LatestVersion.ModpackFolders["scripts"] + " to " + minecraftScriptsDirectory);
                Directory.Move(LatestVersion.ModpackFolders["scripts"], minecraftScriptsDirectory);
                AppendLog("Move Successful!");

                lblStatus.Text = "Scripts Installed";
                ProgressCompleted += 1;
                statusMainProgressBar.Value = GetProgressComplete();
                Thread.Sleep(2000);
            }
            AppendLog("Checking if modpack includes Forge files");
            if (LatestVersion.ToBeUpdated["forgeFiles"] == true)
            {
                lblStatus.Text = "Installing Forge Files";
                AppendLog("Forge Included.  Logging new config directory as " + LatestVersion.ModpackFolders["config"]);

                AppendLog("Checking for " + minecraftForgeJarDirectory);
                if(Directory.Exists(minecraftForgeJarDirectory) == false)
                {
                    AppendLog("Directory Not Found. Creating now.");
                    Directory.CreateDirectory(minecraftForgeJarDirectory);
                }
                AppendLog("Checking for " + minecraftForgeJsonDirectory);
                if (Directory.Exists(minecraftForgeJsonDirectory) == false)
                {
                    AppendLog("Directory Not Found. Creating now.");
                    Directory.CreateDirectory(minecraftForgeJsonDirectory);
                }

                // SET BASE LOCATIONS
                string minecraftJarDirectory = Path.Combine(Options.MinecraftDirectory, "libraries\\net\\minecraftforge\\forge\\");
                string minecraftJsonDirectory = Path.Combine(Options.MinecraftDirectory, "versions\\");

                AppendLog("Logging Minecraft Jar Directory as " + minecraftJarDirectory);
                AppendLog("Logging Minecraft Json Directory as " + minecraftJsonDirectory);

                // GET FOLDER NAMES HERE
                AppendLog("Attempting to move folder structure from " + LatestVersion.ModpackFolders["forgeJarRoot"] + " to " + minecraftJsonDirectory);
                CopyAll(new DirectoryInfo(LatestVersion.ModpackFolders["forgeJarRoot"]), new DirectoryInfo(minecraftJarDirectory));
                AppendLog("Move Successful!");
                AppendLog("Attempting to move folder structure from " + LatestVersion.ModpackFolders["forgeJsonRoot"] + " to " + minecraftJsonDirectory);
                CopyAll(new DirectoryInfo(LatestVersion.ModpackFolders["forgeJsonRoot"]), new DirectoryInfo(minecraftJsonDirectory));
                AppendLog("Move Successful!");

                AddNewForgeLauncherProfile(LatestVersion.Forge.ForgeVersionID, LatestVersion.Forge.InstallationName);

                lblStatus.Text = "Forge Files Installed";
                ProgressCompleted += 1;
                statusMainProgressBar.Value = GetProgressComplete();
                Thread.Sleep(2000);
            }

            AppendLog("Modpack Update Process Complete. Moving onto Cleanup.");
            lblStatus.Text = "Modpack Updated!";
            Thread.Sleep(2000);
            lblStatus.Text = "Cleaning Up";

            AppendLog("Writing latest version file into " + VersionFilePath);
            string MPVersionJson = JsonConvert.SerializeObject(LatestVersion, Formatting.Indented);
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\modpack.ver", MPVersionJson);

            AppendLog("Deleting Extraction Path.");
            Directory.Delete(extractionPath, true);
            AppendLog("Deleting Modpack Zip File.");
            File.Delete(zipFilePath);

            AppendLog("All processes completed. Closing Application.");

            lblStatus.Text = "Updated Finished";
            ProgressCompleted += 1;
            statusMainProgressBar.Value = GetProgressComplete();
            Thread.Sleep(2000);

            HideStatus();
            menuMain.Enabled = true;
            btnCheckUpdate.Enabled = true;
            btnUpdateModpack.Enabled = false;
            btnExit.Enabled = true;
            lblInstalledVersionText.Text = LatestVersion.Text;
        }
        private void ExitProgram()
        {
            UpdateConfigJSON();
            Close();
        }
        private ModpackVerFile DownloadMPVersionJson(string wgetURL)
        {
            // DOWNLOAD MC.MLCGAMING.COM/DOWNLOADS/MPVERSION.JSON
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(wgetURL);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            // PARSE FILE INTO VERSIONINFO OBJECT (LATESTVERSION)
            StreamReader reader = new StreamReader(responseStream);
            return JsonConvert.DeserializeObject<ModpackVerFile>(reader.ReadToEnd());
        }

        private void AppendLog(string logMessage, bool isNewLine = true)
        {
            string entry = "";

            if(isNewLine == true)
            {
                entry = Environment.NewLine;
                entry += $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}";
                entry += ": " + logMessage;

                File.AppendAllText(LogFilePath, entry);
            }
            else
            {
                entry += $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}";
                entry += ": " + logMessage;

                File.AppendAllText(LogFilePath, entry);
            }
        }
        private void ShowStatus()
        {
            this.Size = new System.Drawing.Size(210, 265);
            lblStatus.Show();
            statusMain.Show();
            btnExit.Enabled = false;
            btnUpdateModpack.Enabled = false;
            btnCheckUpdate.Enabled = false;
        }
        private void HideStatus()
        {
            Size = new System.Drawing.Size(210, 230);
            lblStatus.Hide();
            statusMain.Hide();
            btnExit.Enabled = true;
            btnUpdateModpack.Enabled = true;
            btnCheckUpdate.Enabled = true;
        }
        private int GetProgressComplete()
        {
            return Convert.ToInt32(100 *(ProgressCompleted / ProgressCounter));
        }
    }
}
