using BepInEx;
using BepInEx.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Valheim.EnhancedProgressTracker.ConfigurationCore;

namespace Valheim.EnhancedProgressTracker.ConfigurationTypes
{
    public static class ConfigurationManager
    {
        internal const string GeneralConfigFile = "enhanced_progress_tracker.cfg";

        internal const string TribesConfigurationFile = "enhanced_progress_tracker.tribes.cfg";

        public static bool DebugOn => true;

        public static GeneralConfig GeneralConfig;

        public static Dictionary<string, TribeConfiguration> TribeConfigurations;

        public static void LoadAllConfigurations()
        {
            Log.LogInfo("Loading all configs.");

            GeneralConfig = LoadGeneral();

            TribeConfigurations = LoadTribeConfigurations();
        }

        public static GeneralConfig LoadGeneral()
        {
            Log.LogInfo("Loading general configurations");

            string configPath = Path.Combine(Paths.ConfigPath, GeneralConfigFile);
            ConfigFile configFile = new ConfigFile(configPath, true);

            GeneralConfig = new GeneralConfig();
            GeneralConfig.Load(configFile);

            Log.LogInfo("Finished loading general configurations");

            return GeneralConfig;
        }

        public static Dictionary<string, TribeConfiguration> LoadTribeConfigurations()
        {
            string configPath = Path.Combine(Paths.ConfigPath, TribesConfigurationFile);

            Log.LogInfo($"Loading tribe configurations from {configPath}.");

            ConfigurationLoader.SanitizeSectionHeaders(configPath);

            var configFile = new ConfigFile(configPath, true);
            if (GeneralConfig.StopTouchingMyConfigs.Value) configFile.SaveOnConfigSet = false;

            Dictionary<string, TribeConfiguration> configurations = ConfigurationLoader.LoadConfigurationGroup<TribeConfiguration, TribeMember>(configFile);

            Log.LogInfo($"Finished tribe configurations.");

            return configurations;
        }
    }
}
