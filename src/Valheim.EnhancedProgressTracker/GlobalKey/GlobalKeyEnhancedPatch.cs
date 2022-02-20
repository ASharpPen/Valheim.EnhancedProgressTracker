using HarmonyLib;
using System;
using System.Collections.Generic;
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

            if(player is null)
            {
                return;
            }

            if (Enum.TryParse(ConfigurationManager.GeneralConfig.KeyMode.Value, out KeyMode keyMode))
            {
                switch (keyMode)
                {
                    case KeyMode.Player:
                        __result = EnhancedGlobalKey.HasPlayerKey(player, name, ___m_globalKeys);
                        break;
                    case KeyMode.Tribe:
                        __result = EnhancedGlobalKey.HasTribeKey(player, name, ___m_globalKeys);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
