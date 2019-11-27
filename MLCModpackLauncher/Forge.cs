using Newtonsoft.Json;

namespace MLCModpackLauncher
{
    public class Forge
    {
        public string ID { get; protected set; }
        public string Icon { get; protected set; }
        public string LastVersionID { get; protected set; }
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public string Created { get; protected set; }

        public Forge(string id, string lastVersionId, string name = "BuddyPals", string type = "custom")
        {
            ID = id;
            Icon = "Furnace";
            LastVersionID = lastVersionId;
            Created = "2019-10-24T14:41:20.713Z";
            Name = name;
            Type = type;
        }

        [JsonConstructor]
        public Forge(string id, string icon, string lastVersionId, string created, string name = "BuddyPals", string type = "custom")
        {
            ID = id;
            Icon = icon;
            LastVersionID = lastVersionId;
            Created = created;
            Name = name;
            Type = type;
        }
    }
}
