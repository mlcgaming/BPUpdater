using Newtonsoft.Json;

namespace MLCModpackLauncher.DirectoryFiles
{
    public class ClientCodex
    {
        public static string ID { get; private set; }
        public string Password { get; private set; }

        [JsonConstructor]
        public ClientCodex(string id, string password)
        {
            ID = id;
            Password = password;
        }
    }
}
