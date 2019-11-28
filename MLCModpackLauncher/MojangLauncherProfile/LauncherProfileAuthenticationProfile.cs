using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MLCModpackLauncher.MojangLauncherProfile
{
    public class LauncherProfileAuthenticationProfile
    {
        public string DisplayName { get; private set; }

        public LauncherProfileAuthenticationProfile()
        {
            DisplayName = "TEST";
        }

        [JsonConstructor]
        public LauncherProfileAuthenticationProfile(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
