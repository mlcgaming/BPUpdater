using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLCModpackLauncher.MojangLogin
{
    public class MojangLauncherLogin
    {
        public string AccessToken { get; private set; }
        public string ClientToken { get; private set; }
        public MojangProfile SelectedProfile { get; private set; }
        public MojangProfile[] AvailableProfiles { get; private set; }

        public MojangLauncherLogin(string accessToken, MojangProfile selectedProfile, string clientToken, MojangProfile[] availableProfiles)
        {
            AccessToken = accessToken;
            SelectedProfile = selectedProfile;
            ClientToken = clientToken;
            AvailableProfiles = availableProfiles;
        }
    }
}
