using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MLCModpackLauncher.MojangLauncherProfile
{
    public class LauncherProfileSelectedUser
    {
        public string Account { get; private set; }
        public string Profile { get; private set; }

        public LauncherProfileSelectedUser()
        {
            Account = "6d044e877b0e4d51964a43d66032fc5c";
            Profile = "c130580798b24b28a91c888952f84864";
        }

        [JsonConstructor]
        public LauncherProfileSelectedUser(string account, string profile)
        {
            Account = account;
            Profile = profile;
        }
    }
}
