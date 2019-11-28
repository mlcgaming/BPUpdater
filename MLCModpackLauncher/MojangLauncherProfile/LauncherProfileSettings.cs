using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MLCModpackLauncher.MojangLauncherProfile
{
    public class LauncherProfileSettings
    {
        public string Channel { get; private set; }
        public bool CrashAssistance { get; private set; }
        public bool EnableAdvanced { get; private set; }
        public bool EnableAnalytics { get; private set; }
        public bool EnableHistorical { get; private set; }
        public bool EnableReleases { get; private set; }
        public bool EnableSnapshots { get; private set; }
        public bool KeepLauncherOpen { get; private set; }
        public string Locale { get; private set; }
        public string ProfileSorting { get; private set; }
        public bool ShowGameLog { get; private set; }
        public bool ShowMenu { get; private set; }
        public bool SoundOn { get; private set; }

        public LauncherProfileSettings()
        {
            Channel = "release";
            CrashAssistance = true;
            EnableAdvanced = true;
            EnableAnalytics = true;
            EnableHistorical = false;
            EnableReleases = true;
            EnableSnapshots = false;
            KeepLauncherOpen = false;
            Locale = "en-us";
            ProfileSorting = "ByLastPlayed";
            ShowGameLog = false;
            ShowMenu = false;
            SoundOn = false;
        }

        [JsonConstructor]
        public LauncherProfileSettings(string channel, bool crashAssistance, bool enableAdvanced, bool enableAnalytics, bool enableHistorical, bool enableReleases, bool enableSnapshots,
            bool keepLauncherOpen, string locale, string profileSorting, bool showGameLog, bool showMenu, bool soundOn)
        {
            Channel = channel;
            CrashAssistance = crashAssistance;
            EnableAdvanced = enableAdvanced;
            EnableAnalytics = enableAnalytics;
            EnableHistorical = enableHistorical;
            EnableReleases = enableReleases;
            EnableSnapshots = enableSnapshots;
            KeepLauncherOpen = keepLauncherOpen;
            Locale = locale;
            ProfileSorting = profileSorting;
            ShowGameLog = showGameLog;
            ShowMenu = showMenu;
            SoundOn = soundOn;
        }
    }
}
