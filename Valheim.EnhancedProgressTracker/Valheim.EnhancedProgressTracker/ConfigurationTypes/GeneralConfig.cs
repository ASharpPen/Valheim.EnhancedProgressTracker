using BepInEx.Configuration;
using System;
using Valheim.EnhancedProgressTracker.ConfigurationCore;

namespace Valheim.EnhancedProgressTracker.ConfigurationTypes
{
    [Serializable]
    internal class GeneralConfig
    {
        [NonSerialized]
        private ConfigFile Config;

        #region Debug

        public ConfigurationEntry<bool> DebugLoggingOn = new ConfigurationEntry<bool>(false, "Enable debug logging.");

        public ConfigurationEntry<bool> TraceLoggingOn = new ConfigurationEntry<bool>(false, "Enables in-depth logging. Note, this might generate a LOT of log entries.");

        #endregion

        #region General

        public ConfigurationEntry<bool> StopTouchingMyConfigs = new ConfigurationEntry<bool>(false, "Disables automatic updating and saving of configurations.\nThis means no descriptions or missing fields will be added, but.. allows you to keep things compact.");

        public ConfigurationEntry<string> KeyMode = new ConfigurationEntry<string>("Default", "Choose key tracking mode. This decides how progress is tracked. Default, Player, Tribe.");

        #endregion

        #region Kill Tracking

        //Disabled until I can figure out how to do this.
        //public ConfigurationEntry<bool> TrackKillContributors = new ConfigurationEntry<bool>(true, "Toggle if players who damaged a creature will have their (or their tribe's) progress updated on creature death.");

        public ConfigurationEntry<float> TrackPlayersWithinDistance = new ConfigurationEntry<float>(100, "If players are within the set distance on creature death, they will have their (or their tribe's) progress updated. If 0 or less, this setting is disabled.");

        #endregion

        #region Keys

        public ConfigurationEntry<bool> RecordPlayerKeys = new ConfigurationEntry<bool>(true, "Enables player key recording. Meaning each player will get a global key recorded, when a creature dies.");
        public ConfigurationEntry<bool> RecordTribeKeys = new ConfigurationEntry<bool>(true, "Enables tribe key recording. Meaning each tribe will get a global key recorded, when a creature dies.");

        #endregion

        public void Load(ConfigFile configFile)
        {
            Config = configFile;

            DebugLoggingOn.Bind(Config, "Debug", "DebugLoggingOn");
            TraceLoggingOn.Bind(Config, "Debug", "TraceLoggingOn");

            TrackPlayersWithinDistance.Bind(Config, "KillTracking", nameof(TrackPlayersWithinDistance));

            KeyMode.Bind(Config, "General", nameof(KeyMode));
            StopTouchingMyConfigs.Bind(Config, "General", nameof(StopTouchingMyConfigs));

            RecordPlayerKeys.Bind(Config, "Keys", nameof(RecordPlayerKeys));
            RecordTribeKeys.Bind(Config, "Keys", nameof(RecordTribeKeys));
        }
    }
}
