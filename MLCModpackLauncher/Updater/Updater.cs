using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyPals;
using BuddyPals.Versioning;

namespace MLCModpackLauncher.Updater
{
    public class Updater
    {
        public UpdateMode Mode { get; private set; }
        public WorkingVersions WorkingVersions { get; private set; }
        public string WorkingDirectory { get; private set; }

        public enum UpdateMode
        {
            Major,
            Minor,
            Patch
        }

        public Updater(UpdateMode mode, WorkingVersions versions, string workingDirectory)
        {
            Mode = mode;
            WorkingVersions = versions;
            WorkingDirectory = workingDirectory;
        }

        private void PerformUpdate()
        {
            switch (Mode)
            {
                case UpdateMode.Major:
                    {

                        break;
                    }
                case UpdateMode.Minor:
                    {

                        break;
                    }
                case UpdateMode.Patch:
                    {

                        break;
                    }
            }
        }

        private void UpdateModsPassive()
        {

        }
        private void UpdateModsForced()
        {

        }
        private void UpdateConfig()
        {
            
        }
        private void UpdateScriptsPassive()
        {

        }
        private void UpdateScriptsForced()
        {

        }
    }
}
