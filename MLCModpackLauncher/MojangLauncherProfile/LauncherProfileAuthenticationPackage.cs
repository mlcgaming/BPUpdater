using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MLCModpackLauncher.MojangLauncherProfile
{
    public class LauncherProfileAuthenticationPackage
    {
        public string AccessToken { get; private set; }
        public Dictionary<string, LauncherProfileAuthenticationProfile> Profiles { get; private set; }
        public string[] Properties { get; private set; }
        public string Username { get; private set; }

        public LauncherProfileAuthenticationPackage()
        {
            AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiI2ZDA0NGU4NzdiMGU0ZDUxOTY0YTQzZDY2MDMyZmM1YyIsIm5iZiI6MTU3NDgzNTgzNCwieWdndCI6IjE2NDZhNWM3YWViODRjYjNiNmE2MTRhMDczZjExNzdlIiwic3ByIjoiYzEzMDU4MDc5OGIyNGIyOGE5MWM4ODg5NTJmODQ4NjQiLCJyb2xlcyI6W10sImlzcyI6ImludGVybmFsLWF1dGhlbnRpY2F0aW9uIiwiZXhwIjoxNTc1MDA4NjM0LCJpYXQiOjE1NzQ4MzU4MzR9.YLCIIydI54UNxazHqmHh_1EpT6I-6DXh-K6kok6dICs";
            Profiles = new Dictionary<string, LauncherProfileAuthenticationProfile>();
            Properties = new string[0];
            Username = "HostusMostus";
        }

        [JsonConstructor]
        public LauncherProfileAuthenticationPackage(string accessToken, Dictionary<string, LauncherProfileAuthenticationProfile> profiles,
            string[] properties, string username)
        {
            AccessToken = accessToken;
            Profiles = new Dictionary<string, LauncherProfileAuthenticationProfile>(profiles);
            Properties = properties;
            Username = username;
        }
    }
}
