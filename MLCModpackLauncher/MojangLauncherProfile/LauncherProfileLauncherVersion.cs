using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MLCModpackLauncher.MojangLauncherProfile
{
    public class LauncherProfileLauncherVersion
    {
        public int Format { get; private set; }
        public string Name { get; private set; }
        public int ProfilesFormat { get; private set; }

        public LauncherProfileLauncherVersion()
        {
            Format = 21;
            Name = "2.1.3674";
            ProfilesFormat = 2;
        }

        [JsonConstructor]
        public LauncherProfileLauncherVersion(int format, string name, int profilesFormat)
        {
            Format = format;
            Name = name;
            ProfilesFormat = profilesFormat;
        }
    }
}
