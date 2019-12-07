using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MLCModpackLauncher.MojangLauncherProfile
{
    public class LauncherProfile
    {
        public string Created { get; private set; }
        public string GameDir { get; private set; }
        public string Icon { get; private set; }
        public string JavaArgs { get; private set; }
        public string LastUsed { get; private set; }
        public string LastVersionID { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }

        public LauncherProfile()
        {
            Created = "2019-11-01T00:00:20.257Z";
            GameDir = null;
            Icon = "Furnace";
            JavaArgs = null;
            LastUsed = "2019-01-01T00:00:00.000Z";
            LastVersionID = "1.12.2-forge1.12.2-14.23.5.2779";
            Name = "BuddyPals Test";
            Type = "custom";
        }

        [JsonConstructor]
        public LauncherProfile(string icon, string lastUsed, string lastVersionId, string name, string type, 
            string created = "2019-11-01T00:00:20.257Z", string gameDir = null, string javaArgs = null)
        {
            Created = created;
            GameDir = gameDir;
            Icon = icon;
            JavaArgs = javaArgs;
            LastUsed = lastUsed;
            LastVersionID = lastVersionId;
            Name = name;
            Type = type;
        }

        public void UpdateForge(string forgeVersion, string installationName)
        {
            LastVersionID = forgeVersion;
            Name = installationName;
        }
    }
}
