using System;
using System.IO;

namespace MLCModpackLauncher.DirectoryFiles
{
    public static class Library
    {
        public const string MOD_ID = "mods";
        public const string CONFIG_ID = "config";
        public const string RESOURCEPACK_ID = "resourcePacks";
        public const string SHADERS_ID = "shaderPacks";
        public const string SCRIPTS_ID = "scripts";
        public const string FORGEFILES_ID = "forgeFiles";

        public const string MOD_BIN_PATH = "BuddyPals\\bin\\mods\\";
        public const string CONFIG_BIN_PATH = "BuddyPals\\bin\\config\\";
        public const string RESOURCEPACK_BIN_PATH = "BuddyPals\\bin\\resourcepacks\\";
        public const string SHADERS_BIN_PATH = "BuddyPals\\bin\\shaderpacks\\";
        public const string SCRIPTS_BIN_PATH = "BuddyPals\\bin\\scripts\\";
        public const string JAR_BIN_PATH = "BuddyPals\\bin\\forge\\libraries";
        public const string JSON_BIN_PATH = "BuddyPals\\bin\\forge\\versions";

        public const string JAR_ROOT_ID = "forgeJarRoot";
        public const string JSON_ROOT_ID = "forgeJsonRoot";

        public static string MOD_SRC_FOLDER, CONFIG_SRC_FOLDER, RESOURCEPACK_SRC_FOLDER, SHADERS_SRC_FOLDER, SCRIPTS_SRC_FOLDER;

        public static string MOD_DST_FOLDER = "mods";
        public static string CONFIG_DST_FOLDER = "config";
        public static string RESOURCEPACK_DST_FOLDER = "resourcepacks";
        public static string SHADERS_DST_FOLDER = "shaderpacks";
        public static string SCRIPTS_DST_FOLDER = "scripts";
        public static string JAR_DST_FOLDER = "libraries";
        public static string JSON_DST_FOLDER = "versions";

        public static string BuddyPalsAppDataDirectory { get; private set; }
        public static string UpdaterConfigFilePath { get; private set; }
        public static string ModpackVersionFilePath { get; private set; }
        public static string LogFilePath { get; private set; }

        public static void Initialize()
        {
            InitializePaths();
        }

        private static void InitializePaths()
        {
            BuddyPalsAppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\");
            UpdaterConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\updater.conf");
            ModpackVersionFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\modpack.ver");
            LogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BuddyPals\\updater.log");

            MOD_SRC_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), MOD_BIN_PATH);
            CONFIG_SRC_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), CONFIG_BIN_PATH);
            RESOURCEPACK_SRC_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), RESOURCEPACK_BIN_PATH);
            SHADERS_SRC_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), SHADERS_BIN_PATH);
            SCRIPTS_SRC_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), SCRIPTS_BIN_PATH);
        }

        /// <summary>
        /// Sets all static directory strings for destination folders.
        /// </summary>
        /// <param name="workingDirectoryRoot">Either Options.MinecraftDirectory or Options.PTRDirectory</param>
        /// <param name="forgeDirectoryRoot">Usually Options.MineCraftDiretory (C:/Users/USER/AppData/.minecraft/)</param>
        public static void UpdateDestinationFolderPaths(string workingDirectoryRoot, string forgeDirectoryRoot)
        {
            MOD_DST_FOLDER = Path.Combine(workingDirectoryRoot, "mods");
            CONFIG_DST_FOLDER = Path.Combine(workingDirectoryRoot, "config");
            RESOURCEPACK_DST_FOLDER = Path.Combine(workingDirectoryRoot, "resourcepacks");
            SHADERS_DST_FOLDER = Path.Combine(workingDirectoryRoot, "shaderpacks");
            SCRIPTS_DST_FOLDER = Path.Combine(workingDirectoryRoot, "scripts");

            JAR_DST_FOLDER = Path.Combine(forgeDirectoryRoot, "libraries");
            JSON_DST_FOLDER = Path.Combine(forgeDirectoryRoot, "versions");
        }
    }
}
