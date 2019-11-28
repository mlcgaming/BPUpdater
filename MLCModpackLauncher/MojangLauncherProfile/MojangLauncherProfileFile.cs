using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MLCModpackLauncher.MojangLauncherProfile
{
    public class MojangLauncherProfileFile
    {
        public int AnalyticsFailCount { get; private set; }
        public string AnalyticsToken { get; private set; }
        public Dictionary<string, LauncherProfileAuthenticationPackage> AuthenticationDatabase { get; private set; }
        public string ClientToken { get; private set; }
        public LauncherProfileLauncherVersion LauncherVersion { get; private set; }
        public Dictionary<string, LauncherProfile> Profiles { get; private set; }
        public LauncherProfileSelectedUser SelectedUser { get; private set; }
        public LauncherProfileSettings Settings { get; private set; }

        public MojangLauncherProfileFile()
        {
            AnalyticsFailCount = 0;
            AnalyticsToken = "3c3caaf33160cbfcb3c281cebf62a32f";
            AuthenticationDatabase = new Dictionary<string, LauncherProfileAuthenticationPackage>();
            ClientToken = "f1f216107a33f9014333ca06cbf25785";
            LauncherVersion = new LauncherProfileLauncherVersion();
            Profiles = new Dictionary<string, LauncherProfile>();
            SelectedUser = new LauncherProfileSelectedUser();
            Settings = new LauncherProfileSettings();
            CreateTestData();
        }

        [JsonConstructor]
        public MojangLauncherProfileFile(int analyticsFailCount, string analyticsToken, Dictionary<string,LauncherProfileAuthenticationPackage> authenticationDatabase,
            string clientToken, LauncherProfileLauncherVersion launcherVersion, Dictionary<string, LauncherProfile> profiles,
            LauncherProfileSelectedUser selectedUser, LauncherProfileSettings settings)
        {
            AnalyticsFailCount = analyticsFailCount;
            AnalyticsToken = analyticsToken;
            AuthenticationDatabase = new Dictionary<string, LauncherProfileAuthenticationPackage>(authenticationDatabase);
            ClientToken = clientToken;
            LauncherVersion = launcherVersion;
            Profiles = new Dictionary<string, LauncherProfile>(profiles);
            SelectedUser = selectedUser;
            Settings = settings;
        }

        private void CreateTestData()
        {
            LauncherProfile newProfile = new LauncherProfile("Furnace", "2019-11-27T01:40:56.781Z", "1.12.2-forge1.12.2-14.23.5.2779", "BuddyPals TEST", "custom", javaArgs: "-Xmx10G -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32M");
            Profiles.Add("BuddyPals", newProfile);
        }

        public void AddNewProfile(string forgeVersion, string installationName)
        {
            LauncherProfile newProfile = new LauncherProfile("Furnace", "2019-11-27T01:40:56.781Z", forgeVersion, installationName, "custom", javaArgs: "-Xmx10G -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32M");
            Profiles.Add("BuddyPals", newProfile);
        }
    }
}
