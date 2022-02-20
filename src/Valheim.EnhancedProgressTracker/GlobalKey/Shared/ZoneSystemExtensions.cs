using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using Valheim.EnhancedProgressTracker.ConfigurationCore;
using Valheim.EnhancedProgressTracker.ConfigurationTypes;

namespace Valheim.EnhancedProgressTracker.GlobalKey.Shared
{
    public static class ZoneSystemExtensions
    {
        private static FieldInfo _globalKeysField = AccessTools.Field(typeof(ZoneSystem), "m_globalKeys");

        public static bool HasGlobalKey(this ZoneSystem zoneSystem, Player player, string key)
        {
            string playerName = player.GetHoverName();

            return HasGlobalKey(zoneSystem, playerName, key);
        }

        public static bool HasGlobalKey(this ZoneSystem zoneSystem, string playerName, string key)
        {
            HashSet<string> globalKeys = _globalKeysField.GetValue(zoneSystem) as HashSet<string>;

#if DEBUG
            Log.LogDebug($"Checking for {key} in keys: " + globalKeys.Join());
#endif

            if(globalKeys is null)
            {
                Log.LogWarning("Unable to find/access global keys.");
                return false;
            }

            if (Enum.TryParse(ConfigurationManager.GeneralConfig.KeyMode.Value, true, out KeyMode keyMode))
            {
                switch (keyMode)
                {
                    case KeyMode.Player:
#if DEBUG
                        Log.LogDebug($"Checking key {key} for player {playerName}");
#endif
                        return EnhancedGlobalKey.HasPlayerKey(playerName, key, globalKeys);
                    case KeyMode.Tribe:
#if DEBUG
                        Log.LogDebug($"Checking key {key} for tribe of {playerName}");
#endif
                        return EnhancedGlobalKey.HasTribeKey(playerName, key, globalKeys);
                    default:
                        return globalKeys.Contains(key);
                }
            }

            //If no key-mode is set, use default global keys.
            Log.LogWarning("Unable to find valid key-mode configuration. Will fallback to Default mode.");
            return globalKeys.Contains(key);
        }
    }
}
