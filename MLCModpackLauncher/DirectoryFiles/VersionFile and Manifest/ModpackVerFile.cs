using System;
using System.Collections.Generic;
using System.IO;
using MLCModpackLauncher.DirectoryFiles;
using Newtonsoft.Json;

namespace MLCModpackLauncher
{
    public class ModpackVerFile
    {
        public int ID { get; private set; }
        public int FormatID { get; protected set; }
        public bool IsActive { get; private set; }
        public string Text { get; private set; }
        public string Name { get; private set; }
        public string FileName { get; private set; }
        public string URL { get; private set; }
        public Dictionary<string, bool> ToBeUpdated { get; private set; }
        public Dictionary<string, string> ModpackFolders { get; private set; }
        public ForgePackage Forge { get; private set; }

        public ModpackVerFile(int id, string text, string name)
        {
            ID = id;
            FormatID = 2;
            IsActive = true;
            Text = text;
            Name = name;
            FileName = "";
            URL = "";

            ToBeUpdated = new Dictionary<string, bool>
            {
                { Library.MOD_ID, true },
                { Library.CONFIG_ID, true },
                { Library.RESOURCEPACK_ID, false },
                { Library.SHADERS_ID, false },
                { Library.SCRIPTS_ID, false },
                { Library.FORGEFILES_ID, false }
            };

            SetModpackFolders();
        }

        [JsonConstructor]
        public ModpackVerFile(int id, bool isActive, string text, string name, string fileName, string url, Dictionary<string, bool> toBeUpdated = null, ForgePackage forge = null)
        {
            ID = id;
            IsActive = isActive;
            Text = text;
            Name = name;
            FileName = fileName;
            URL = url;
            Forge = forge;
            ToBeUpdated = toBeUpdated;
            FormatID = 2;

            SetModpackFolders();
        }
        public static ModpackVerFile ConvertFromVersionFile(VersionFile obj)
        {
            Dictionary<string, bool> includedFiles = new Dictionary<string, bool>
            {
                { Library.MOD_ID, obj.IncludesMods },
                { Library.CONFIG_ID, obj.IncludesConfig },
                { Library.RESOURCEPACK_ID, obj.IncludesResourcePack },
                { Library.SHADERS_ID, obj.IncludesShaders },
                { Library.SCRIPTS_ID, false },
                { Library.FORGEFILES_ID, obj.IncludesForge }
            };

            return new ModpackVerFile(obj.ID, obj.IsActive, obj.Text, obj.Name, obj.FileName, obj.URL, includedFiles, obj.Forge);
        }
        public void SetModpackFolders()
        {
            if(ModpackFolders == null)
            {
                ModpackFolders = new Dictionary<string, string>();

                if (ToBeUpdated[Library.MOD_ID] == true)
                {
                    ModpackFolders.Add(Library.MOD_ID, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.MOD_BIN_PATH));
                }
                if (ToBeUpdated[Library.CONFIG_ID] == true)
                {
                    ModpackFolders.Add(Library.CONFIG_ID, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.CONFIG_BIN_PATH));
                }
                if (ToBeUpdated[Library.RESOURCEPACK_ID] == true)
                {
                    ModpackFolders.Add(Library.RESOURCEPACK_ID, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.RESOURCEPACK_BIN_PATH));
                }
                if (ToBeUpdated[Library.SHADERS_ID] == true)
                {
                    ModpackFolders.Add(Library.SHADERS_ID, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.SHADERS_BIN_PATH));
                }
                if (ToBeUpdated[Library.SCRIPTS_ID] == true)
                {
                    ModpackFolders.Add(Library.SCRIPTS_ID, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.SCRIPTS_BIN_PATH));
                }
                if (ToBeUpdated[Library.FORGEFILES_ID] == true)
                {
                    ModpackFolders.Add(Library.JAR_ROOT_ID, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.JAR_BIN_PATH));
                    ModpackFolders.Add(Library.JSON_ROOT_ID, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.JSON_BIN_PATH));
                }
            }
            else
            {
                // Update Existing ModpackFolders with unique entries.
                if (ToBeUpdated[Library.MOD_ID] == true)
                {
                    ModpackFolders[Library.MOD_ID] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.MOD_BIN_PATH);
                }
                if (ToBeUpdated[Library.CONFIG_ID] == true)
                {
                    ModpackFolders[Library.CONFIG_ID] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.CONFIG_BIN_PATH);
                }
                if (ToBeUpdated[Library.RESOURCEPACK_ID] == true)
                {
                    ModpackFolders[Library.RESOURCEPACK_ID] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.RESOURCEPACK_BIN_PATH);
                }
                if (ToBeUpdated[Library.SHADERS_ID] == true)
                {
                    ModpackFolders[Library.SHADERS_ID] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.SHADERS_BIN_PATH);
                }
                if (ToBeUpdated[Library.SCRIPTS_ID] == true)
                {
                    ModpackFolders[Library.SCRIPTS_ID] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.SCRIPTS_BIN_PATH);
                }
                if (ToBeUpdated[Library.FORGEFILES_ID] == true)
                {
                    ModpackFolders[Library.JAR_ROOT_ID] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.JAR_BIN_PATH);
                    ModpackFolders[Library.JSON_ROOT_ID] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Library.JSON_BIN_PATH);
                }
            }
        }
    }
}
