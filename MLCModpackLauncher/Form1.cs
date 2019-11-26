using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MLCModpackLauncher
{
    public partial class MainForm : Form
    {
        OptionsConfiguration Options;
        VersionFile CurrentVersion, LatestVersion;
        bool IsDownloadingPTR;
        string UpdaterConfigFilePath, BuddyPalVersionFilePath;

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
            btnExit.Enabled = false;
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
            btnExit.Enabled = false;
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
                btnExit.Enabled = true;
                IsDownloadingPTR = false;
                return;
            }
        }

        private void InitializeMainForm()
        {
            btnApplyUpdate.Enabled = false;
            CurrentVersion = null;
            LatestVersion = null;
            IsDownloadingPTR = false;
            UpdaterConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\") + "config.json";
            BuddyPalVersionFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\") + "BPVersion.json";

            string optionsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\");

            if (Directory.Exists(optionsDirectory) == true)
            {
                if (File.Exists(UpdaterConfigFilePath) == true)
                {
                    string filePath = optionsDirectory + "\\config.json";
                    Options = JsonConvert.DeserializeObject<OptionsConfiguration>(File.ReadAllText(filePath));
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
                btnExit.Enabled = true;
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

        private void UpdateConfigJSON()
        {
            string optionsFile = JsonConvert.SerializeObject(Options, Formatting.Indented);
            File.WriteAllText(UpdaterConfigFilePath, optionsFile);
        }
        private void CheckForUpdate()
        {
            // DOWNLOAD MC.MLCGAMING.COM/DOWNLOADS/MPVERSION.JSON
            // PARSE FILE INTO VERSIONINFO OBJECT (LATESTVERSION)
            LatestVersion = DownloadMPVersionJson("ftp://mc.mlcgaming.com/modpack/BPVersion.json");

            // LOOK FOR MPVERSION.JSON IN APPDATA\BUDDYPALS\ FOLDER

            if (File.Exists(BuddyPalVersionFilePath) == true)
            {
                CurrentVersion = JsonConvert.DeserializeObject<VersionFile>(File.ReadAllText(BuddyPalVersionFilePath));
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
                btnApplyUpdate.Enabled = true;
            }
            else
            {
                btnApplyUpdate.Enabled = false;
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
                    File.Copy(file, Path.Combine(packDirectory, Path.GetFileName(file)));
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
                    File.Copy(file, Path.Combine(shaderDirectory, Path.GetFileName(file)));
                }

                MessageBox.Show("This Modpack Update included a Shader Pack! You can enable it in-game by going to Options > Video Settings > Shaders!");
            }

            MessageBox.Show("Modpack Updated!");
            MessageBox.Show("Cleaning Up...Almost Finished!");

            string MPVersionJson = JsonConvert.SerializeObject(LatestVersion, Formatting.Indented);
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BuddyPals\\BPVersion.json", MPVersionJson);

            Directory.Delete(extractionPath, true);
            File.Delete(zipFilePath);

            MessageBox.Show("All Done! Enjoy!");
            Close();
        }
    }
}
