using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLCModpackLauncher.DirectoryFiles
{
    public class ConfigPackage
    {
        public ConfigType Type { get; private set; }
        public string PathName { get; private set; }

        public enum ConfigType
        {
            File,
            Directory,
            Null
        }

        public ConfigPackage(ConfigType type, string pathName)
        {
            Type = type;
            PathName = pathName;
        }
    }
}
