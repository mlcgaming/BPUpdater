using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuddyPals;
using BuddyPals.Versioning;

namespace MLCModpackLauncher.Updating
{
    public static class Updater
    {
        public static Manifest WorkingManifest { get; private set; }
        public static string WorkingDirectory { get; private set; }

        private static string ModDownloadRootUrl, ConfigDownloadRootUrl, ScriptDownloadRootUrl;
        private static DirectoryInfo ModDirectory, ConfigDirectory, ScriptDirectory;
        private static List<FileInfo> CurrentMods, CurrentConfigFiles, CurrentScripts;
        private static List<DirectoryInfo> CurrentConfigDirectories;
        private static ProgressBar ProgressBar;

        public static void Setup(Manifest workingManifest, string workingDirectory, bool isPTRUpdate, ProgressBar progressBar)
        {
            WorkingManifest = workingManifest;
            WorkingDirectory = workingDirectory;
            ProgressBar = progressBar;

            if(isPTRUpdate == true)
            {
                ModDownloadRootUrl = Library.ModPTRDownloadRootUrl;
                ConfigDownloadRootUrl = Library.ConfigPTRDownloadRootUrl;
                ScriptDownloadRootUrl = Library.ScriptPTRDownloadRootUrl;
            }
            else
            {
                ModDownloadRootUrl = Library.ModDownloadRootUrl;
                ConfigDownloadRootUrl = Library.ConfigDownloadRootUrl;
                ScriptDownloadRootUrl = Library.ScriptDownloadRootUrl;
            }

            ModDirectory = new DirectoryInfo(Path.Combine(WorkingDirectory, "mods"));
            ConfigDirectory = new DirectoryInfo(Path.Combine(WorkingDirectory, "config"));
            ScriptDirectory = new DirectoryInfo(Path.Combine(WorkingDirectory, "scripts"));

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
        }

        public static void PerformUpdate()
        {
            if(WorkingManifest.Mods.Count > 0)
            {
                foreach(ModPackage mod in WorkingManifest.Mods)
                {
                    ProgressBar.Value = 0;
                    UpdateMod(mod);
                }
            }
            if(WorkingManifest.Scripts.Count > 0)
            {
                foreach(ManifestPackage script in WorkingManifest.Scripts)
                {
                    ProgressBar.Value = 0;
                    UpdateScript(script);
                }
            }
        }

        private static void UpdateMod(ModPackage modPackage)
        {
            // Set Up Our URL's for Downloading
            string modDownloadUrl = ModDownloadRootUrl + modPackage.Latest;
            List<string> configDownloadUrls = new List<string>();
            if(modPackage.Config.Type != ConfigPackage.ConfigType.Null)
            {
                foreach(string path in modPackage.Config.PathName)
                {
                    configDownloadUrls.Add(ConfigDownloadRootUrl + path);
                }
            }

            
        }
        private static void UpdateScript(ManifestPackage scriptPackage)
        {
            // Set up our URL's for Downloading
            List<string> downloadUrls = new List<string>();
            foreach(string file in scriptPackage.Files)
            {
                downloadUrls.Add(ScriptDownloadRootUrl + file);
            }


        }
        private static string CreateFullUrl(string rootUrl, string fileName)
        {
            return rootUrl + fileName;
        }
    }
}
