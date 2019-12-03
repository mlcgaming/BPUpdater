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
            MessageBox.Show("Select PTR Installation Folder. This should be a folder separate from your main Minecraft Installation.");
            Options.SetPTRDirectory(SelectFolder(Environment.SpecialFolder.Desktop));
            MessageBox.Show("PTR Directory Now Set to " + Options.PTRDirectory);
            UpdateConfigJSON();
        }

        private void InitializeMainForm()
        {
            HideStatus();
            cboxLiveOrPTR.SelectedIndex = 0;

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
                    if (File.Exists(Path.Combine(optionsDirectory, "updater.conf")) == false)
                    {
                        // No files at all, Make a new updater.conf file
                        AppendLog("No Configuration File Detected. Creating Default Configuration File.");
                        Options = new OptionsConfiguration();
                        UpdateConfigJSON();
                    }
                    else
                    {
                        // No config.json but we have an updater.conf; load that into memory
                        Options = JsonConvert.DeserializeObject<OptionsConfiguration>(File.ReadAllText(Path.Combine(optionsDirectory, "updater.conf")));
                    }
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
                AppendLog("Download completed! Proceeding to modpack update.");
                ChangeStatus("Unpacking Modpack Files");
                statusMainProgressBar.Value = 0;
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
            switch (cboxLiveOrPTR.SelectedIndex)
            {
                case 1:
                    {
                        // Check for PTR
                        AppendLog("Downloading modpack.ver from Master Server to check for update.");
                        // DOWNLOAD MC.MLCGAMING.COM/DOWNLOADS/MPVERSION.JSON
                        // PARSE FILE INTO VERSIONINFO OBJECT (LATESTVERSION)
                        LatestVersion = DownloadMPVersionJson("ftp://mc.mlcgaming.com/modpack/PTR/modpack.ver");
                        AppendLog("Success!");
                        // PTR Always uses a Dummy File
                        AppendLog("No current modpack.ver found. Creating a dummy file.");
                        CurrentVersion = new ModpackVerFile(0, "N/A", "NULL");
                        break;
                    }
                default:
                    {
                        // Check for Regular Modpack Update
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
                        break;
                    }
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
            // Set Root Directory Based On Live vs PTR choice
            string rootDestinationDirectory = "";

            if (cboxLiveOrPTR.SelectedIndex == 0)
            {
                // LIVE
                rootDestinationDirectory = Options.MinecraftDirectory;
            }
            else
            {
                // PTR
                rootDestinationDirectory = Options.PTRDirectory;
            }

            // Unzip the Latest Modpack
            string zipFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\" + LatestVersion.FileName;
            string extractionPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\bin";

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

            // Create Destination Directories
            string destinationModsDirectory = rootDestinationDirectory + "mods";
            string destinationConfigDirectory = rootDestinationDirectory + "config";
            string destinationPackDirectory = rootDestinationDirectory + "resourcepacks";
            string destinationShaderDirectory = rootDestinationDirectory + "shaderpacks";
            string destinationScriptsDirectory = rootDestinationDirectory + "scripts";

            // Create Forge Destination Directories (These are ALWAYS at the AppData Minecraft Root folder)
            string destinationForgeJsonDirectory = Path.Combine(Options.MinecraftDirectory, "versions");
            string destinationForgeJarDirectory = Path.Combine(Options.MinecraftDirectory, "libraries\\net\\minecraftforge\\forge\\");

            AppendLog("Logging Minecraft Mods Directory as " + destinationModsDirectory);
            AppendLog("Logging Minecraft Config Directory as " + destinationConfigDirectory);
            AppendLog("Logging Minecraft Resource Pack Directory as " + destinationPackDirectory);
            AppendLog("Logging Minecraft Shaders Directory as " + destinationShaderDirectory);
            AppendLog("Logging Minecraft Scripts Directory as " + destinationScriptsDirectory);
            AppendLog("Logging Minecraft Forge JSON Directory as " + destinationForgeJsonDirectory);
            AppendLog("Logging Minecraft Forge JAR Directory as " + destinationForgeJarDirectory);

            AppendLog("Checking if modpack includes mods");
            if (LatestVersion.ToBeUpdated["mods"] == true)
            {
                string sourceFolder = CreateSourceFolderString(LatestVersion.ModpackFolders["mods"]);

                ChangeStatus("Updating Mods");
                AppendLog("Mods Included.  Logging new mods directory as " + sourceFolder);
                AppendLog("Checking for existing mod folder at " + destinationModsDirectory);
                if (Directory.Exists(destinationModsDirectory) == true)
                {
                    AppendLog("Folder found. Removing folder.");
                    Directory.Delete(destinationModsDirectory, true);
                }

                AppendLog("Attempting to move folder from " + sourceFolder + " to " + destinationModsDirectory);
                Directory.Move(sourceFolder, destinationModsDirectory);

                ChangeStatus("Mods Updated");
            }
            AppendLog("Checking if modpack includes config");
            if (LatestVersion.ToBeUpdated["config"] == true)
            {
                string sourceFolder = CreateSourceFolderString(LatestVersion.ModpackFolders["config"]);

                ChangeStatus("Updating Configs");
                AppendLog("Config Included.  Logging new config directory as " + sourceFolder);
                AppendLog("Checking for existing folder at " + destinationConfigDirectory);
                if (Directory.Exists(destinationConfigDirectory) == true)
                {
                    AppendLog("Folder found. Removing folder.");
                    Directory.Delete(destinationConfigDirectory, true);
                }

                AppendLog("Attempting to move folder from " + sourceFolder + " to " + destinationConfigDirectory);
                Directory.Move(sourceFolder, destinationConfigDirectory);
                AppendLog("Move Successful!");

                ChangeStatus("Configs Updated");
            }
            AppendLog("Checking if modpack includes Resource Packs");
            if (LatestVersion.ToBeUpdated["resourcePacks"] == true)
            {
                string sourceFolder = CreateSourceFolderString(LatestVersion.ModpackFolders["resourcePacks"]);

                ChangeStatus("Updating Resource Packs");
                AppendLog("Resource Pack Included.  Logging new resourcepacks directory as " + sourceFolder);

                AppendLog("Checking for existing folder at " + destinationPackDirectory);
                if (Directory.Exists(destinationPackDirectory) == false)
                {
                    AppendLog("No such path found. Creating new directory.");
                    Directory.CreateDirectory(destinationPackDirectory);
                }

                AppendLog("Moving Files from " + sourceFolder + " to " + destinationPackDirectory);
                foreach (var file in Directory.GetFiles(sourceFolder))
                {
                    if(File.Exists(file) == false)
                    {
                        AppendLog("Moving " + Path.GetFileName(file) + " to " + destinationPackDirectory);
                        File.Copy(file, Path.Combine(destinationPackDirectory, Path.GetFileName(file)));
                    }
                }

                AppendLog("Move Successful!");

                ChangeStatus("Resource Packs Installed");
            }
            AppendLog("Checking if modpack includes Shaders");
            if (LatestVersion.ToBeUpdated["shaderPacks"] == true)
            {
                string sourceFolder = CreateSourceFolderString(LatestVersion.ModpackFolders["shaderPacks"]);

                ChangeStatus("Updaing Shaders");
                AppendLog("Shaders Included.  Logging new shaders directory as " + sourceFolder);

                AppendLog("Checking for existing folder at " + destinationShaderDirectory);
                if (Directory.Exists(destinationShaderDirectory) == false)
                {
                    AppendLog("No such path found. Creating new directory.");
                    Directory.CreateDirectory(destinationShaderDirectory);
                }

                AppendLog("Moving Files from " + sourceFolder + " to " + destinationShaderDirectory);
                foreach (var file in Directory.GetFiles(sourceFolder))
                {
                    if(File.Exists(file) == false)
                    {
                        AppendLog("Moving " + Path.GetFileName(file) + " to " + destinationShaderDirectory);
                        File.Copy(file, Path.Combine(destinationShaderDirectory, Path.GetFileName(file)));
                    }
                }

                AppendLog("Move Successful!");

                ChangeStatus("Shaders Installed");
            }
            AppendLog("Checking if modpack includes Scripts");
            if (LatestVersion.ToBeUpdated["scripts"] == true)
            {
                string sourceFolder = CreateSourceFolderString(LatestVersion.ModpackFolders["scripts"]);

                ChangeStatus("Installing Scripts");
                AppendLog("Scripts Included.  Logging new scripts directory as " + sourceFolder);

                AppendLog("Checking for existing directory " + destinationScriptsDirectory);
                if (Directory.Exists(destinationScriptsDirectory) == true)
                {
                    AppendLog("Removing " + destinationScriptsDirectory);
                    Directory.Delete(destinationScriptsDirectory, true);
                }

                AppendLog("Copying " + sourceFolder + " to " + destinationScriptsDirectory);
                Directory.Move(sourceFolder, destinationScriptsDirectory);
                AppendLog("Move Successful!");

                ChangeStatus("Scripts Installed");
            }
            AppendLog("Checking if modpack includes Forge files");
            if (LatestVersion.ToBeUpdated["forgeFiles"] == true)
            {
                string sourceJarFolder = CreateSourceFolderString(LatestVersion.ModpackFolders["forgeJarRoot"]);
                string sourceJsonFolder = CreateSourceFolderString(LatestVersion.ModpackFolders["forgeJson"]);

                ChangeStatus("Installing Forge Files");
                AppendLog("Forge Included.  Logging new config directories as " + sourceJarFolder + " for JAR files, and " + sourceJsonFolder + " for JSON files");

                AppendLog("Checking for " + destinationForgeJarDirectory);
                if(Directory.Exists(destinationForgeJarDirectory) == false)
                {
                    AppendLog("Directory Not Found. Creating now.");
                    Directory.CreateDirectory(destinationForgeJarDirectory);
                }
                AppendLog("Checking for " + destinationForgeJsonDirectory);
                if (Directory.Exists(destinationForgeJsonDirectory) == false)
                {
                    AppendLog("Directory Not Found. Creating now.");
                    Directory.CreateDirectory(destinationForgeJsonDirectory);
                }

                // SET BASE LOCATIONS
                AppendLog("Logging Minecraft Jar Directory as " + destinationForgeJarDirectory);
                AppendLog("Logging Minecraft Json Directory as " + destinationForgeJsonDirectory);

                // GET FOLDER NAMES HERE
                AppendLog("Attempting to move folder structure from " + sourceJarFolder + " to " + destinationForgeJarDirectory);
                CopyAll(new DirectoryInfo(LatestVersion.ModpackFolders["forgeJarRoot"]), new DirectoryInfo(destinationForgeJarDirectory));
                AppendLog("Move Successful!");
                AppendLog("Attempting to move folder structure from " + sourceJsonFolder + " to " + destinationForgeJsonDirectory);
                CopyAll(new DirectoryInfo(LatestVersion.ModpackFolders["forgeJsonRoot"]), new DirectoryInfo(destinationForgeJsonDirectory));
                AppendLog("Move Successful!");

                AddNewForgeLauncherProfile(LatestVersion.Forge.ForgeVersionID, LatestVersion.Forge.InstallationName);

                ChangeStatus("Forge Files Installed");
            }

            AppendLog("Modpack Update Process Complete. Moving onto Cleanup.");
            ChangeStatus("Modpack Updated!");
            ChangeStatus("Cleaning Up...");

            if(cboxLiveOrPTR.SelectedIndex == 0)
            {
                AppendLog("Writing latest version file into " + VersionFilePath);
                string MPVersionJson = JsonConvert.SerializeObject(LatestVersion, Formatting.Indented);
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\modpack.ver", MPVersionJson);
            }
            
            AppendLog("Deleting Extraction Path.");
            Directory.Delete(extractionPath, true);
            AppendLog("Deleting Modpack Zip File.");
            File.Delete(zipFilePath);

            AppendLog("All processes completed. Closing Application.");

            ChangeStatus("Update Complete!");
            
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
            this.Size = new System.Drawing.Size(210, 270);
            lblStatus.Show();
            statusMain.Show();
            btnExit.Enabled = false;
            btnUpdateModpack.Enabled = false;
            btnCheckUpdate.Enabled = false;
        }
        private void HideStatus()
        {
            Size = new System.Drawing.Size(210, 235);
            lblStatus.Hide();
            statusMain.Hide();
            btnExit.Enabled = true;
            btnUpdateModpack.Enabled = true;
            btnCheckUpdate.Enabled = true;
        }
        private void ChangeStatus(string message)
        {
            lblStatus.Text = message;
            lblStatus.Refresh();
            Thread.Sleep(500);
        }
        private string CreateSourceFolderString(string modpackFolder)
        {
            if(cboxLiveOrPTR.SelectedIndex == 0)
            {
                return Path.Combine(Options.MinecraftDirectory, modpackFolder);
            }
            else
            {
                return Path.Combine(Options.PTRDirectory, modpackFolder);
            }
        }
    }
}
