using System;
using System.Collections.Generic;
using Valheim.EnhancedProgressTracker.ConfigurationTypes;

namespace Valheim.EnhancedProgressTracker.Multiplayer
{
    [Serializable]
    internal class ConfigurationPackage
    {
        public GeneralConfig GeneralConfig;

        public Dictionary<string, TribeConfiguration> TribeConfig;

        public ConfigurationPackage(){ }

        public ConfigurationPackage(
            GeneralConfig generalConfig,
            Dictionary<string, TribeConfiguration> tribeConfigs)
        {
            GeneralConfig = generalConfig;
            TribeConfig = tribeConfigs;
        }
    }
}
