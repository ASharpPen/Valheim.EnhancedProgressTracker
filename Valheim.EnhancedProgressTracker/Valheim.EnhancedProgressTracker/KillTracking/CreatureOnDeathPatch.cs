using HarmonyLib;
using System;
using Valheim.EnhancedProgressTracker.ConfigurationCore;

namespace Valheim.EnhancedProgressTracker.KillTracking
{
    [HarmonyPatch(typeof(Character))]
    public static class CreatureOnDeathPatch
    {
        [HarmonyPatch("OnDeath")]
        [HarmonyPrefix]
        private static void SetKeyOnDeath(Character __instance)
        {
            var keys = ZoneSystem.instance.GetGlobalKeys();

            string name = __instance.gameObject?.name;

            try
            {
                string cleanedName = name.Split('(')[0].Trim().ToUpperInvariant();
                var newKey = $"KILLED_{cleanedName}";

                ZoneSystem.instance.SetGlobalKey(newKey);

#if DEBUG
                Log.LogDebug($"Set global key {newKey}.");
#endif
            }
            catch (Exception e) { }

        }
    }
}
