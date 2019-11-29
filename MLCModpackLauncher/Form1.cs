using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;
using MLCModpackLauncher.MojangLauncherProfile;
using Newtonsoft.Json;

namespace MLCModpackLauncher
{
    public partial class MainForm : Form
    {
        OptionsConfiguration Options;
        VersionFile CurrentVersion, LatestVersion;
        bool IsDownloadingPTR;
        string ConfigFilePath, VersionFilePath;
        ToolTip CurrentVersionTooltip, LatestVersionTooltip, CheckForUpdateButtonTooltip, UpdateModpackTooltip, UpdateForgeTooltip;

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
            LatestVersion = DownloadMPVersionJson("ftp://mc.mlcgaming.com/modpack/PTR/BPVersion.json");

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
            btnUpdateModpack.Enabled = false;
            CurrentVersion = null;
            LatestVersion = null;
            IsDownloadingPTR = false;
            ConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\") + "updater.conf";
            VersionFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\") + "modpack.ver";

            InitializeOptions();
            CleanupAppDataDirectory();
            InitializeTooltips();
        }
        private void InitializeOptions()
        {
            string optionsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\");

            if (Directory.Exists(optionsDirectory) == true)
            {
                if (File.Exists(Path.Combine(optionsDirectory, "config.json")) == true)
                {
                    if (File.Exists(Path.Combine(optionsDirectory, "updater.conf")) == false)
                    {
                        // Use old config file to make new updater.settings file
                        Options = JsonConvert.DeserializeObject<OptionsConfiguration>(File.ReadAllText(Path.Combine(optionsDirectory, "config.json")));
                        string newFile = JsonConvert.SerializeObject(Options, Formatting.Indented);
                        File.WriteAllText(Path.Combine(optionsDirectory, "updater.conf"), newFile);

                        // Then Remove the Old File
                        File.Delete(Path.Combine(optionsDirectory, "config.json"));
                    }
                    else
                    {
                        // Just remove the old Config File
                        File.Delete(Path.Combine(optionsDirectory, "config.json"));
                    }
                }
                else
                {
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
            if(File.Exists(Path.Combine(Options.AppDirectory, "BPVersion.json")) == true)
            {
                if (File.Exists(Path.Combine(Options.AppDirectory, "modpack.ver")) == false)
                {
                    // Use old BPVersion file to make new 'modpack.ver' file
                    LatestVersion = JsonConvert.DeserializeObject<VersionFile>(File.ReadAllText(Path.Combine(Options.AppDirectory, "BPVersion.json")));
                    string newFile = JsonConvert.SerializeObject(LatestVersion, Formatting.Indented);
                    File.WriteAllText(Path.Combine(Options.AppDirectory, "modpack.ver"), newFile);

                    // Then Remove the Old File
                    File.Delete(Path.Combine(Options.AppDirectory, "BPVersion.json"));
                }
                else
                {
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
            UpdateForgeTooltip = new ToolTip();

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
            string launcherFilePath = Path.Combine(Options.MinecraftDirectory, "launcher_profiles.json");
            MojangLauncherProfileFile launcherFile = JsonConvert.DeserializeObject<MojangLauncherProfileFile>(File.ReadAllText(launcherFilePath));
            launcherFile.AddNewProfile(forgeVersion, installationName);
            string savePath = Path.Combine(Options.MinecraftDirectory, "launcher_profiles.json");
            string json = JsonConvert.SerializeObject(launcherFile, Formatting.Indented);
            File.WriteAllText(savePath, json);
        }

        private void DownloadFileFTP(string downloadPath)
        {
            string fileName = LatestVersion.FileName;

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
            prgbarMain.Value = e.ProgressPercentage;
        }
        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (IsDownloadingPTR == false)
            {
                MessageBox.Show("The download is completed!");
                MessageBox.Show("Modpack will now update!");

                UpdateModpack();
            }
            else
            {
                MessageBox.Show("PTR Zip Downloaded!");
                string optionsFile = JsonConvert.SerializeObject(Options, Formatting.Indented);
                File.WriteAllText(Options.AppDirectory + "/config.json", optionsFile);
                IsDownloadingPTR = false;
                prgbarMain.Value = 0;
                menuMain.Enabled = true;
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            ExitProgram();
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
        private void UpdateConfigJSON()
        {
            string optionsFile = JsonConvert.SerializeObject(Options, Formatting.Indented);
            File.WriteAllText(ConfigFilePath, optionsFile);
        }
        private void CheckForUpdate()
        {
            // DOWNLOAD MC.MLCGAMING.COM/DOWNLOADS/MPVERSION.JSON
            // PARSE FILE INTO VERSIONINFO OBJECT (LATESTVERSION)
            LatestVersion = DownloadMPVersionJson("ftp://mc.mlcgaming.com/modpack/modpack.ver");

            // LOOK FOR MPVERSION.JSON IN APPDATA\BUDDYPALS\ FOLDER

            if (File.Exists(VersionFilePath) == true)
            {
                CurrentVersion = JsonConvert.DeserializeObject<VersionFile>(File.ReadAllText(VersionFilePath));
            }
            else
            {
                CurrentVersion = new VersionFile(0, "N/A", "NULL");
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

            if (Directory.Exists(extractionPath) == true)
            {
                Directory.Delete(extractionPath, true);
            }

            ZipFile.ExtractToDirectory(zipFilePath, extractionPath);

            // DELETE MODS AND CONFIG FOLDERS
            string modsDirectory = Options.MinecraftDirectory + "mods";
            string configDirectory = Options.MinecraftDirectory + "config";
            string packDirectory = Options.MinecraftDirectory + "resourcepacks";
            string shaderDirectory = Options.MinecraftDirectory + "shaderpacks";
            string forgeVersionDirectory = Path.Combine(Options.MinecraftDirectory, "versions");
            string forgeJarDirectory = Path.Combine(Options.MinecraftDirectory, "libraries\\net\\minecraftforge\\forge\\");

            if (LatestVersion.IncludesMods == true)
            {
                MessageBox.Show("Updating Mods!");

                if (Directory.Exists(modsDirectory) == true)
                {
                    Directory.Delete(modsDirectory, true);
                }

                string latestModsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\bin\\mods\\";

                Directory.Move(latestModsDirectory, modsDirectory);
            }
            if (LatestVersion.IncludesConfig == true)
            {
                MessageBox.Show("Updating Configs!");

                if (Directory.Exists(configDirectory) == true)
                {
                    Directory.Delete(configDirectory, true);
                }

                string latestConfigDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\bin\\config\\";

                Directory.Move(latestConfigDirectory, configDirectory);
            }
            if (LatestVersion.IncludesResourcePack == true)
            {
                MessageBox.Show("Installing Resource Packs!");

                if (Directory.Exists(packDirectory) == false)
                {
                    Directory.CreateDirectory(packDirectory);
                }

                string latestPackDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\bin\\resourcepacks\\";

                foreach (var file in Directory.GetFiles(latestPackDirectory))
                {
                    if(File.Exists(file) == false)
                    {
                        File.Copy(file, Path.Combine(packDirectory, Path.GetFileName(file)));
                    }
                }

                MessageBox.Show("This Modpack Update included a ResourcePack! You can enable it in-game by going to Options > Resource Packs!");
            }
            if (LatestVersion.IncludesShaders == true)
            {
                MessageBox.Show("Installing Shaders!");

                if (Directory.Exists(shaderDirectory) == false)
                {
                    Directory.CreateDirectory(shaderDirectory);
                }

                string latestShadersDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\bin\\shaderpacks\\";

                foreach (var file in Directory.GetFiles(latestShadersDirectory))
                {
                    if(File.Exists(file) == false)
                    {
                        File.Copy(file, Path.Combine(shaderDirectory, Path.GetFileName(file)));
                    }
                }

                MessageBox.Show("This Modpack Update included a Shader Pack! You can enable it in-game by going to Options > Video Settings > Shaders!");
            }
            if (LatestVersion.IncludesForge == true)
            {
                MessageBox.Show("Installing Updated Forge Files!");

                if(Directory.Exists(forgeJarDirectory) == false)
                {
                    Directory.CreateDirectory(forgeJarDirectory);
                }
                if(Directory.Exists(forgeVersionDirectory) == false)
                {
                    Directory.CreateDirectory(forgeVersionDirectory);
                }

                string latestJarDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\bin\\forge\\JAR\\";
                string latestJsonDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\bin\\forge\\JSON\\";

                foreach (var folder in Directory.GetDirectories(latestJarDirectory))
                {
                    var lastFolderName = Path.GetFileName(folder.TrimEnd('\\'));

                    if (Directory.Exists(Path.Combine(forgeJarDirectory, Path.GetDirectoryName(lastFolderName))) == false)
                    {
                        Directory.Move(folder, Path.Combine(forgeJarDirectory, Path.GetDirectoryName(lastFolderName)));
                    }
                }
                foreach (var folder in Directory.GetDirectories(latestJsonDirectory))
                {
                    var lastFolderName = Path.GetFileName(folder.TrimEnd('\\'));

                    if (Directory.Exists(Path.Combine(forgeVersionDirectory, Path.GetDirectoryName(lastFolderName))) == false)
                    {
                        Directory.Move(folder, Path.Combine(forgeVersionDirectory, Path.GetDirectoryName(lastFolderName)));
                    }
                }

                AddNewForgeLauncherProfile(LatestVersion.Forge.ForgeVersionID, LatestVersion.Forge.InstallationName);

                MessageBox.Show("This Modpack release includes a newer version of Forge. A new launcher profile has been created for you. Make sure to select " + LatestVersion.Forge.InstallationName + " next to the Play button, if it is not selected already! This change will not take effect until the next time you open your Minecraft Launcher, so if it open during this update, you'll need to close and re-open the window!");
            }

            MessageBox.Show("Modpack Updated!");
            MessageBox.Show("Cleaning Up...Almost Finished!");

            string MPVersionJson = JsonConvert.SerializeObject(LatestVersion, Formatting.Indented);
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\modpack.ver", MPVersionJson);

            Directory.Delete(extractionPath, true);
            File.Delete(zipFilePath);

            MessageBox.Show("All Done! Enjoy!");
            Close();
        }
        private void ExitProgram()
        {
            UpdateConfigJSON();
            Close();
        }
        private VersionFile DownloadMPVersionJson(string wgetURL)
        {
            // DOWNLOAD MC.MLCGAMING.COM/DOWNLOADS/MPVERSION.JSON
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(wgetURL);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            // PARSE FILE INTO VERSIONINFO OBJECT (LATESTVERSION)
            StreamReader reader = new StreamReader(responseStream);
            return JsonConvert.DeserializeObject<VersionFile>(reader.ReadToEnd());
        }
    }
}
