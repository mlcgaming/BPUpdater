using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuddyPals;
using BuddyPals.Versioning;
using WinSCP;

namespace MLCModpackLauncher.Updating
{
    public class UpdaterCompleteEventArgs : EventArgs
    {
        public bool UpdateSuccessful { get; set; }
    }

    public class Updater : IDisposable
    {
        public Manifest WorkingManifest { get; private set; }
        public string WorkingDirectory { get; private set; }

        private int TotalFileProgress, CurrentFileProgress, TotalOverallProgress;
        private string ModDownloadRootUrl, ConfigDownloadRootUrl, ScriptDownloadRootUrl;
        private DirectoryInfo ModDirectory, ConfigDirectory, ScriptDirectory;
        private List<FileInfo> CurrentMods, CurrentConfigFiles, CurrentScripts;
        private List<DirectoryInfo> CurrentConfigDirectories;
        private UpdaterForm MyForm;

        public event EventHandler<UpdaterCompleteEventArgs> UpdateComplete;

        public Updater (Manifest workingManifest, string workingDirectory, bool isPTRUpdate, UpdaterForm form)
        {
            Initialize(workingManifest, workingDirectory, isPTRUpdate, form);
        }

        public void Initialize(Manifest workingManifest, string workingDirectory, bool isPTRUpdate, UpdaterForm form)
        {
            WorkingManifest = workingManifest;
            WorkingDirectory = workingDirectory;
            MyForm = form;
            TotalFileProgress = 0;
            CurrentFileProgress = 0;
            TotalOverallProgress = 0;

            if(isPTRUpdate == true)
            {
                ModDownloadRootUrl = Library.ModPTRDownloadRootUrl;
                ConfigDownloadRootUrl = "/modpack/bin/ptr/configs/";
                ScriptDownloadRootUrl = Library.ScriptPTRDownloadRootUrl;
            }
            else
            {
                ModDownloadRootUrl = Library.ModDownloadRootUrl;
                ConfigDownloadRootUrl = "/modpack/bin/stable/configs/";
                ScriptDownloadRootUrl = Library.ScriptDownloadRootUrl;
            }

            ModDirectory = new DirectoryInfo(Path.Combine(WorkingDirectory, "mods\\"));
            ConfigDirectory = new DirectoryInfo(Path.Combine(WorkingDirectory, "config\\"));
            ScriptDirectory = new DirectoryInfo(Path.Combine(WorkingDirectory, "scripts\\"));

            CurrentMods = new List<FileInfo>();
            CurrentConfigFiles = new List<FileInfo>();
            CurrentConfigDirectories = new List<DirectoryInfo>();
            CurrentScripts = new List<FileInfo>();

            if(ModDirectory.GetFiles().Count() > 0)
            {
                foreach (FileInfo file in ModDirectory.GetFiles())
                {
                    CurrentMods.Add(file);
                }
            }
            if(ConfigDirectory.GetFiles().Count() > 0)
            {
                foreach (FileInfo file in ConfigDirectory.GetFiles())
                {
                    CurrentConfigFiles.Add(file);
                }
            }
            if(ConfigDirectory.GetDirectories().Count() > 0)
            {
                foreach (DirectoryInfo directory in ConfigDirectory.GetDirectories())
                {
                    CurrentConfigDirectories.Add(directory);
                }
            }
            if(ScriptDirectory.GetFiles().Count() > 0)
            {
                foreach (FileInfo file in ScriptDirectory.GetFiles())
                {
                    CurrentScripts.Add(file);
                }
            }

            TotalFileProgress = workingManifest.Mods.Count + workingManifest.Scripts.Count;
            MyForm.SetTotalItems(TotalFileProgress);
        }
        public void PerformUpdate()
        {
            MyForm.Show();

            if(WorkingManifest.Mods.Count > 0)
            {
                foreach(ModPackage mod in WorkingManifest.Mods)
                {
                    UpdateMod(mod);
                    CurrentFileProgress += 1;
                    SetOverallProgress();
                }
            }
            if(WorkingManifest.Scripts.Count > 0)
            {
                foreach(ManifestPackage script in WorkingManifest.Scripts)
                {
                    UpdateScript(script);
                    CurrentFileProgress += 1;
                    SetOverallProgress();
                }
            }

            OnUpdateComplete();
        }

        private void UpdateMod(ModPackage modPackage)
        {
            SetCurrentItem(modPackage.Name);
            // Set Up Our URL's for Downloading
            string modDownloadUrl = ModDownloadRootUrl + modPackage.Latest;
            UpdateStatus("Checking " + modPackage.Name);
            FileInfo modLatestFileInfo = new FileInfo(Path.Combine(ModDirectory.FullName, modPackage.Latest));

            // Iterate through all current mod files
            if(CurrentMods.Contains(modLatestFileInfo) == false)
            {
                // Mods folder is missing the mod, download it!
                UpdateStatus("Downloading " + modPackage.Latest + " ...");
                DownloadFile(modDownloadUrl, Path.Combine(ModDirectory.FullName, modPackage.Latest));
            }
            else
            {
                // Mods Folder already has the mod
                // Check for Forced Update and Process accordingly
                if(modPackage.Forced == true)
                {
                    UpdateStatus("Downloading " + modPackage.Latest + " ...");
                    File.Delete(Path.Combine(ModDirectory.FullName, modPackage.Latest));
                    DownloadFile(modDownloadUrl, Path.Combine(ModDirectory.FullName, modPackage.Latest));
                }
            }

            // Iterate through Current Mods and Delete historical versions
            UpdateStatus("Removing Old Versions of " + modPackage.Name);
            foreach (FileInfo file in CurrentMods)
            {
                // Search for and Delete matches of historical files
                foreach (string oldfile in modPackage.History)
                {
                    FileInfo modOldFileInfo = new FileInfo(Path.Combine(ModDirectory.FullName, oldfile));
                    if (file.Name == oldfile)
                    {
                        File.Delete(Path.Combine(ModDirectory.FullName, oldfile));
                    }
                }
            }

            // CONFIG(S)
            // Config Files are always updated
            if(modPackage.Config.Type != ConfigPackage.ConfigType.Null)
            {
                UpdateStatus("Updating " + modPackage.Name + " Config File(s)");
                switch (modPackage.Config.Type)
                {
                    case ConfigPackage.ConfigType.File:
                        {
                            foreach(string configFile in modPackage.Config.PathName)
                            {
                                foreach (FileInfo currentConfig in CurrentConfigFiles)
                                {
                                    // Search for Match and Delete if Found
                                    if(currentConfig.Name == configFile)
                                    {
                                        File.Delete(Path.Combine(ConfigDirectory.FullName, currentConfig.Name));
                                    }
                                }

                                // Download A Copy of the Config
                                string configDownloadUrl = ConfigDownloadRootUrl + configFile;
                                UpdateStatus("Downloading Config File: " + configFile);
                                DownloadFile("ftp://mc.mlcgaming.com" + configDownloadUrl, Path.Combine(ConfigDirectory.FullName, configFile));
                            }
                            break;
                        }
                    case ConfigPackage.ConfigType.Directory:
                        {
                            foreach (string configDirectory in modPackage.Config.PathName)
                            {
                                foreach (DirectoryInfo currentConfig in CurrentConfigDirectories)
                                {
                                    // Search for Match and Delete if Found
                                    if (currentConfig.FullName.Contains(configDirectory) == true)
                                    {
                                        Directory.Delete(currentConfig.FullName, true);
                                    }
                                }

                                // Download A Copy of the Config
                                string configDownloadUrl = ConfigDownloadRootUrl + configDirectory + "/*";
                                UpdateStatus("Downloading Config File: " + configDirectory);
                                DownloadDirectory("mc.mlcgaming.com", configDownloadUrl, Path.Combine(ConfigDirectory.FullName, configDirectory));
                            }
                            break;
                        }
                }
            }
        }
        private void UpdateScript(ManifestPackage scriptPackage)
        {
            // Set up our URL's for Downloading
            foreach(string file in scriptPackage.Files)
            {
                foreach(FileInfo existingfile in CurrentScripts)
                {
                    if(existingfile.Name == file)
                    {
                        File.Delete(existingfile.FullName);
                    }
                }

                string downloadURL = ScriptDownloadRootUrl + file;
                UpdateStatus("Downloading Script File: " + file);
                DownloadFile(downloadURL, Path.Combine(ScriptDirectory.FullName, file));
            }
        }
        /// <summary>
        /// Downloads File from URL specified to the Path specified
        /// </summary>
        /// <param name="url">Full URL of the File to be Downloaded</param>
        /// <param name="filePath">Full Path with File Name of the Destination File</param>
        /// <param name="username">Username to Use for FTP Connection (Default is "anonymous"</param>
        /// <param name="password">Password to Use for FTP Connection (Default is "")</param>
        private void DownloadFile(string url, string filePath, string username = "anonymous", string password = "")
        {
            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential("ftpuser", "mlcTech19!");
                request.DownloadProgressChanged += ProgressChanged;
                request.DownloadFileAsync(new Uri(url), filePath);
            }
        }
        /// <summary>
        /// Downloads Directory from URL specified to the Path specified
        /// </summary>
        /// <param name="host">FQDN or IP address of the FTP Host (Default is "mc.mlcgaming.com"</param>
        /// <param name="directory">Directory to be downloaded, as an extension of the Host (e.g. for mc.mlcgaming.com/downloads/directory/ this would be /downloads/directory/</param>
        /// <param name="downloadPath">The Root Directory to Copy the Downloaded Directories (e.g. To download /downloads/directory/ and get C:/Temp/downloads/directory/ this would be "C:/Temp/downloads/directory/*"</param>
        /// <param name="username">Username used to access the FTP service</param>
        /// <param name="password">Password used to access the FTP service</param>
        private void DownloadDirectory(string host, string directory, string downloadPath, string username = "anonymous", string password = "")
        {
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = host,
                UserName = username,
                Password = password
            };

            using (Session session = new Session())
            {
                session.Open(sessionOptions);
                session.GetFiles(directory, downloadPath + "\\*").Check();
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if(MyForm != null)
            {
                SetCurrentItemProgress(e.ProgressPercentage);
            }
        }
        private void CalculateOverallProgress()
        {
            TotalOverallProgress = (int)(100*((double)CurrentFileProgress / (double)TotalFileProgress));
        }
        private void SetCurrentItemProgress(int value)
        {
            MyForm.SetCurrentItemProgress(value);
            MyForm.Update();
        }
        private void SetOverallProgress()
        {
            CalculateOverallProgress();
            MyForm.SetOverallProgress(TotalOverallProgress, CurrentFileProgress);
            MyForm.Update();
        }
        private void SetCurrentItem(string currentItem)
        {
            MyForm.SetCurrentItem(currentItem);
            MyForm.Update();
        }
        private void UpdateStatus(string status)
        {
            MyForm.UpdateStatus(status);
            MyForm.Update();
        }

        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            MyForm.Close();
            MyForm = null;
            WorkingManifest = null;
            WorkingDirectory = null;

            TotalFileProgress = 0;
            CurrentFileProgress = 0;
            TotalOverallProgress = 0;
            ModDownloadRootUrl = null;
            ConfigDownloadRootUrl = null;
            ScriptDownloadRootUrl = null;
            ModDirectory = null;
            ConfigDirectory = null;
            ScriptDirectory = null;
            CurrentMods = null;
            CurrentConfigFiles = null;
            CurrentScripts = null;
            CurrentConfigDirectories = null;

            GC.SuppressFinalize(this);
        }

        public void OnUpdateComplete()
        {
            UpdateComplete?.Invoke(null, new UpdaterCompleteEventArgs() { UpdateSuccessful = true });
            MyForm.Close();
        }

        // Finalizer
        ~Updater()
        {
            Dispose(false);
        }
    }
}
