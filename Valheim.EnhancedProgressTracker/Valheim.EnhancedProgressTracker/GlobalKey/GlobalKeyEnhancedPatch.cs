using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valheim.EnhancedProgressTracker.ConfigurationTypes;

namespace Valheim.EnhancedProgressTracker.GlobalKey
{
    [HarmonyPatch(typeof(ZoneSystem))]
    public static class GlobalKeyEnhancedPatch
    {
        [HarmonyPatch("GetGlobalKey")]
        [HarmonyPostfix]
        private static void GetPlayerOrTribeKey(string name, HashSet<string> ___m_globalKeys, ref bool __result)
        {
            var player = Player.m_localPlayer;

            if (Enum.TryParse(ConfigurationManager.GeneralConfig.KeyMode.Value, out KeyMode keyMode))
            {
                switch (keyMode)
                {
                    case KeyMode.Player:
                        __result = HasPlayerkey(player, name, ___m_globalKeys);
                        break;
                    case KeyMode.Tribe:
                        __result = HasTribeKey(player, name, ___m_globalKeys);
                        break;
                    default:
                        break;
                }
            }
        }

        private static bool HasPlayerkey(Player player, string key, HashSet<string> globalKeys)
        {
            string playerKey = KeyHelper.GetPlayerKey(player, key);

            if(playerKey == null)
            {
                return false;
            }

            return globalKeys.Contains(playerKey);
        }

        private static bool HasTribeKey(Player player, string key, HashSet<string> globalKeys)
        {
            var tribeKey = KeyHelper.GetTribeKey(player, key);

            if(tribeKey == null)
            {
                //No tribe for player. Use global defaults.
                return globalKeys.Contains(key);
            }

            return globalKeys.Contains(tribeKey);
        }
    }
}
