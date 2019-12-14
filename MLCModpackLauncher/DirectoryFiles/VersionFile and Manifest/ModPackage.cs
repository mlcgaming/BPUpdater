using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MLCModpackLauncher.DirectoryFiles
{
    public class ModPackage
    {
        public string Name { get; private set; }
        public bool Enabled { get; private set; }
        public bool Forced { get; private set; }
        public string Latest { get; private set; }
        public List<string> History { get; private set; }
        public ConfigPackage Config { get; private set; }

        public ModPackage(string name, bool isEnabled, bool isForced, string latestFileName, ConfigPackage config, List<string> history)
        {
            Name = name;
            Enabled = isEnabled;
            Forced = isForced;
            Latest = latestFileName;
            Config = config;

            if(history.Count > 0)
            {
                History = new List<string>(history);
            }
            else
            {
                History = new List<string>();
            }
        }
    }
}
