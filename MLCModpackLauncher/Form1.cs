using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MLCModpackLauncher.MojangLauncherProfile;
using MLCModpackLauncher.DirectoryFiles;
using Newtonsoft.Json;

namespace MLCModpackLauncher
{
    public partial class MainForm : Form
    {
        OptionsConfiguration Options;
        ModpackVerFile CurrentVersion, LatestVersion;
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
            DownloadFileFTP(Library.BuddyPalsAppDataDirectory);
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
            Library.Initialize();
            HideStatus();
            cboxLiveOrPTR.SelectedIndex = 0;

            if (File.Exists(Library.LogFilePath) == false)
            {
                var myFile = File.Create(Library.LogFilePath);
                myFile.Close();
                AppendLog("Created new updater.log file.", false);
            }

            AppendLog("\nInitializing MainForm");

            btnUpdateModpack.Enabled = false;
            CurrentVersion = null;
            LatestVersion = null;

            AppendLog("ConfigFilePath: " + Library.UpdaterConfigFilePath);
            AppendLog("VersionFilePath: " + Library.ModpackVersionFilePath);
            AppendLog("LogFilePath: " + Library.LogFilePath);

            if (File.Exists(Library.ModpackVersionFilePath) == false)
            {
                AppendLog("No Current Modpack Version File, Creating a Dummy File");

                ModpackVerFile newFile = new ModpackVerFile(0, "N/A", "N/A");
                string newFileJson = JsonConvert.SerializeObject(newFile, Formatting.Indented);
                File.WriteAllText(Library.ModpackVersionFilePath, newFileJson);

                AppendLog("Dummy File Instantiated and Serialized");
            }

            try
            {
                // Check for older VersionFile formats and convert to a new ModpackVerFile object
                AppendLog("Reading " + Library.ModpackVersionFilePath);
                VersionFile oldVersionFile = JsonConvert.DeserializeObject<VersionFile>(File.ReadAllText(Library.ModpackVersionFilePath));
                if(oldVersionFile != null)
                {
                    AppendLog("modpack.ver is old format. Updating to new format.");
                    File.Delete(Library.ModpackVersionFilePath);
                    ModpackVerFile newVersionFile = ModpackVerFile.ConvertFromVersionFile(oldVersionFile);
                    string newVersionFileJson = JsonConvert.SerializeObject(newVersionFile, Formatting.Indented);
                    File.WriteAllText(Library.ModpackVersionFilePath, newVersionFileJson);
                    AppendLog(Library.ModpackVersionFilePath + " updated to new format.");
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
                    LatestVersion = JsonConvert.DeserializeObject<ModpackVerFile>(File.ReadAllText(Path.Combine(Library.BuddyPalsAppDataDirectory, "BPVersion.json")));
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
            AppendLog("Download completed! Proceeding to modpack update.");
            ChangeStatus("Unpacking Modpack Files");
            statusMainProgressBar.Value = 0;
            UpdateModpack();
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
                        AppendLog("Checking for current modpack.ver file at " + Library.ModpackVersionFilePath);
                        if (File.Exists(Library.ModpackVersionFilePath) == true)
                        {
                            AppendLog(Library.ModpackVersionFilePath + " found! Loading current version file.");
                            CurrentVersion = JsonConvert.DeserializeObject<ModpackVerFile>(File.ReadAllText(Library.ModpackVersionFilePath));
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
            string rootDestinationDirectory;

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

            Library.UpdateDestinationFolderPaths(rootDestinationDirectory, Options.MinecraftDirectory);

            // Unzip the Latest Modpack
            string zipFilePath = Path.Combine(Library.BuddyPalsAppDataDirectory, LatestVersion.FileName);
            string extractionPath = Path.Combine(Library.BuddyPalsAppDataDirectory, "bin");

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

            AppendLog("Logging Minecraft Mods Directory as " + Library.MOD_DST_FOLDER);
            AppendLog("Logging Minecraft Config Directory as " + Library.CONFIG_DST_FOLDER);
            AppendLog("Logging Minecraft Resource Pack Directory as " + Library.RESOURCEPACK_DST_FOLDER);
            AppendLog("Logging Minecraft Shaders Directory as " + Library.SHADERS_DST_FOLDER);
            AppendLog("Logging Minecraft Scripts Directory as " + Library.SCRIPTS_DST_FOLDER);
            AppendLog("Logging Minecraft Forge JSON Directory as " + Library.JSON_DST_FOLDER);
            AppendLog("Logging Minecraft Forge JAR Directory as " + Library.JAR_DST_FOLDER);

            AppendLog("Checking if modpack includes mods");
            if (LatestVersion.ToBeUpdated[Library.MOD_ID] == true)
            {
                ChangeStatus("Updating Mods");
                AppendLog("Mods Included.  Logging new mods directory as " + Library.MOD_SRC_FOLDER);
                AppendLog("Checking for existing mod folder at " + Library.MOD_DST_FOLDER);
                if (Directory.Exists(Library.MOD_DST_FOLDER) == true)
                {
                    AppendLog("Folder found. Removing folder.");
                    Directory.Delete(Library.MOD_DST_FOLDER, true);
                }

                AppendLog("Attempting to move folder from " + Library.MOD_SRC_FOLDER + " to " + Library.MOD_DST_FOLDER);
                Directory.Move(Library.MOD_SRC_FOLDER, Library.MOD_DST_FOLDER);

                ChangeStatus("Mods Updated");
            }
            AppendLog("Checking if modpack includes config");
            if (LatestVersion.ToBeUpdated[Library.CONFIG_ID] == true)
            {
                ChangeStatus("Updating Configs");
                AppendLog("Config Included.  Logging new config directory as " + Library.CONFIG_SRC_FOLDER);
                AppendLog("Checking for existing folder at " + Library.CONFIG_DST_FOLDER);
                if (Directory.Exists(Library.CONFIG_DST_FOLDER) == true)
                {
                    AppendLog("Folder found. Removing folder.");
                    Directory.Delete(Library.CONFIG_DST_FOLDER, true);
                }

                AppendLog("Attempting to move folder from " + Library.CONFIG_SRC_FOLDER + " to " + Library.CONFIG_DST_FOLDER);
                Directory.Move(Library.CONFIG_SRC_FOLDER, Library.CONFIG_DST_FOLDER);
                AppendLog("Move Successful!");

                ChangeStatus("Configs Updated");
            }
            AppendLog("Checking if modpack includes Resource Packs");
            if (LatestVersion.ToBeUpdated[Library.RESOURCEPACK_ID] == true)
            {
                ChangeStatus("Updating Resource Packs");
                AppendLog("Resource Pack Included.  Logging new resourcepacks directory as " + Library.RESOURCEPACK_SRC_FOLDER);

                AppendLog("Checking for existing folder at " + Library.RESOURCEPACK_DST_FOLDER);
                if (Directory.Exists(Library.RESOURCEPACK_DST_FOLDER) == false)
                {
                    AppendLog("No such path found. Creating new directory.");
                    Directory.CreateDirectory(Library.RESOURCEPACK_DST_FOLDER);
                }

                AppendLog("Moving Files from " + Library.RESOURCEPACK_SRC_FOLDER + " to " + Library.RESOURCEPACK_DST_FOLDER);
                foreach (var file in Directory.GetFiles(Library.RESOURCEPACK_SRC_FOLDER))
                {
                    if(File.Exists(file) == false)
                    {
                        AppendLog("Moving " + Path.GetFileName(file) + " to " + Library.RESOURCEPACK_DST_FOLDER);
                        File.Copy(file, Path.Combine(Library.RESOURCEPACK_DST_FOLDER, Path.GetFileName(file)));
                    }
                }

                AppendLog("Move Successful!");

                ChangeStatus("Resource Packs Installed");
            }
            AppendLog("Checking if modpack includes Shaders");
            if (LatestVersion.ToBeUpdated[Library.SHADERS_ID] == true)
            {
                ChangeStatus("Updaing Shaders");
                AppendLog("Shaders Included.  Logging new shaders directory as " + Library.SHADERS_SRC_FOLDER);

                AppendLog("Checking for existing folder at " + Library.SHADERS_DST_FOLDER);
                if (Directory.Exists(Library.SHADERS_DST_FOLDER) == false)
                {
                    AppendLog("No such path found. Creating new directory.");
                    Directory.CreateDirectory(Library.SHADERS_DST_FOLDER);
                }

                AppendLog("Moving Files from " + Library.SHADERS_SRC_FOLDER + " to " + Library.SHADERS_DST_FOLDER);
                foreach (var file in Directory.GetFiles(Library.SHADERS_SRC_FOLDER))
                {
                    if(File.Exists(file) == false)
                    {
                        AppendLog("Moving " + Path.GetFileName(file) + " to " + Library.SHADERS_DST_FOLDER);
                        File.Copy(file, Path.Combine(Library.SHADERS_DST_FOLDER, Path.GetFileName(file)));
                    }
                }

                AppendLog("Move Successful!");

                ChangeStatus("Shaders Installed");
            }
            AppendLog("Checking if modpack includes Scripts");
            if (LatestVersion.ToBeUpdated[Library.SCRIPTS_ID] == true)
            {
                ChangeStatus("Installing Scripts");
                AppendLog("Scripts Included.  Logging new scripts directory as " + Library.SCRIPTS_SRC_FOLDER);

                AppendLog("Checking for existing directory " + Library.SCRIPTS_DST_FOLDER);
                if (Directory.Exists(Library.SCRIPTS_DST_FOLDER) == true)
                {
                    AppendLog("Removing " + Library.SCRIPTS_DST_FOLDER);
                    Directory.Delete(Library.SCRIPTS_DST_FOLDER, true);
                }

                AppendLog("Copying " + Library.SCRIPTS_SRC_FOLDER + " to " + Library.SCRIPTS_DST_FOLDER);
                Directory.Move(Library.SCRIPTS_SRC_FOLDER, Library.SCRIPTS_DST_FOLDER);
                AppendLog("Move Successful!");

                ChangeStatus("Scripts Installed");
            }
            AppendLog("Checking if modpack includes Forge files");
            if (LatestVersion.ToBeUpdated[Library.FORGEFILES_ID] == true)
            {
                ChangeStatus("Installing Forge Files");
                AppendLog("Forge Included.  Logging new config directories as " + Library.JAR_DST_FOLDER + " for JAR files, and " + Library.JSON_DST_FOLDER + " for JSON files");

                AppendLog("Checking for " + Library.JAR_DST_FOLDER);
                if(Directory.Exists(Library.JAR_DST_FOLDER) == false)
                {
                    AppendLog("Directory Not Found. Creating now.");
                    Directory.CreateDirectory(Library.JAR_DST_FOLDER);
                }
                AppendLog("Checking for " + Library.JSON_DST_FOLDER);
                if (Directory.Exists(Library.JSON_DST_FOLDER) == false)
                {
                    AppendLog("Directory Not Found. Creating now.");
                    Directory.CreateDirectory(Library.JSON_DST_FOLDER);
                }

                // SET BASE LOCATIONS
                AppendLog("Logging Minecraft Jar Directory as " + Library.JAR_DST_FOLDER);
                AppendLog("Logging Minecraft Json Directory as " + Library.JSON_DST_FOLDER);

                // GET FOLDER NAMES HERE
                AppendLog("Attempting to move folder structure from " + LatestVersion.ModpackFolders[Library.JAR_ROOT_ID] + " to " + Library.JAR_DST_FOLDER);
                CopyAll(new DirectoryInfo(LatestVersion.ModpackFolders[Library.JAR_ROOT_ID]), new DirectoryInfo(Library.JAR_DST_FOLDER));
                AppendLog("Move Successful!");
                AppendLog("Attempting to move folder structure from " + LatestVersion.ModpackFolders[Library.JSON_ROOT_ID] + " to " + Library.JSON_DST_FOLDER);
                CopyAll(new DirectoryInfo(LatestVersion.ModpackFolders[Library.JSON_ROOT_ID]), new DirectoryInfo(Library.JSON_DST_FOLDER));
                AppendLog("Move Successful!");

                AddNewForgeLauncherProfile(LatestVersion.Forge.ForgeVersionID, LatestVersion.Forge.InstallationName);

                ChangeStatus("Forge Files Installed");
            }

            AppendLog("Modpack Update Process Complete. Moving onto Cleanup.");
            ChangeStatus("Modpack Updated!");
            ChangeStatus("Cleaning Up...");

            if(cboxLiveOrPTR.SelectedIndex == 0)
            {
                AppendLog("Writing latest version file into " + Library.ModpackVersionFilePath);
                string MPVersionJson = JsonConvert.SerializeObject(LatestVersion, Formatting.Indented);
                File.WriteAllText(Library.ModpackVersionFilePath, MPVersionJson);
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

                File.AppendAllText(Library.LogFilePath, entry);
            }
            else
            {
                entry += $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}";
                entry += ": " + logMessage;

                File.AppendAllText(Library.LogFilePath, entry);
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
    }
}
