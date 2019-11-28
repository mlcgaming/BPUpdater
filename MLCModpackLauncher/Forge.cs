using Newtonsoft.Json;

namespace MLCModpackLauncher
{
    public class Forge
    {
        public string InstallationName { get; private set; }
        public string ForgeVersionID { get; private set; }

        [JsonConstructor]
        public Forge(string installationName, string forgeVersionId)
        {
            InstallationName = installationName;
            ForgeVersionID = forgeVersionId;
        }
    }
}
