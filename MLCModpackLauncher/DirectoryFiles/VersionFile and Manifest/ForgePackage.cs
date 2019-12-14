using Newtonsoft.Json;

namespace MLCModpackLauncher
{
    public class ForgePackage
    {
        public string InstallationName { get; private set; }
        public string ForgeVersionID { get; private set; }

        [JsonConstructor]
        public ForgePackage(string installationName, string forgeVersionId)
        {
            InstallationName = installationName;
            ForgeVersionID = forgeVersionId;
        }
    }
}
