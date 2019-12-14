using Newtonsoft.Json;

namespace MLCModpackLauncher.MojangLogin
{
    public class MojangProfile
    {
        public string Name { get; private set; }
        public string ID { get; private set; }

        [JsonConstructor]
        public MojangProfile(string name, string id)
        {
            Name = name;
            ID = id;
        }
    }
}
