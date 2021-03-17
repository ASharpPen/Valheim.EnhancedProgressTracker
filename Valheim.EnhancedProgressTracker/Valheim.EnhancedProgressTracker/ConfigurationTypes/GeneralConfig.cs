using BepInEx.Configuration;
using System;
using Valheim.EnhancedProgressTracker.ConfigurationCore;

namespace Valheim.EnhancedProgressTracker.ConfigurationTypes
{
    [Serializable]
    public class GeneralConfig
    {
        [NonSerialized]
        private ConfigFile Config;

        #region Debug

        public ConfigurationEntry<bool> DebugLoggingOn = new ConfigurationEntry<bool>(false, "Enable debug logging.");

        public ConfigurationEntry<bool> TraceLoggingOn = new ConfigurationEntry<bool>(false, "Enables in-depth logging. Note, this might generate a LOT of log entries.");

        #endregion

        #region General

        public ConfigurationEntry<bool> StopTouchingMyConfigs = new ConfigurationEntry<bool>(false, "Disables automatic updating and saving of drop table configurations.\nThis means no helpers will be added, but.. allows you to keep things compact.");

        #endregion


        public void Load(ConfigFile configFile)
        {
            Config = configFile;

            DebugLoggingOn.Bind(Config, "Debug", "DebugLoggingOn");
            TraceLoggingOn.Bind(Config, "Debug", "TraceLoggingOn");

            StopTouchingMyConfigs.Bind(Config, "General", nameof(StopTouchingMyConfigs));
        }
    }
}
