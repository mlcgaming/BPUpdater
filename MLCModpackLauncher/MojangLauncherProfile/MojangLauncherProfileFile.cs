using System;
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


    }
}
