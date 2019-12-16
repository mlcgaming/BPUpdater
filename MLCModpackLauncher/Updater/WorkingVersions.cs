using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyPals;
using BuddyPals.Versioning;

namespace MLCModpackLauncher.Updater
{
    public class WorkingVersions
    {
        public VersionFile Current { get; private set; }
        public VersionFile Latest { get; private set; }

        public WorkingVersions(VersionFile current, VersionFile latest)
        {
            Current = current;
            Latest = latest;
        }
    }
}
