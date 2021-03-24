using HarmonyLib;
using Valheim.EnhancedProgressTracker.ConfigurationCore;
using Valheim.EnhancedProgressTracker.ConfigurationTypes;
using Valheim.EnhancedProgressTracker.Tribe;

namespace Valheim.EnhancedProgressTracker
{
    [HarmonyPatch(typeof(FejdStartup), "OnWorldStart")]
    public static class ResetConfigurations
    {
        private static void Postfix()
        {
            //Check for singleplayer.
            if (ZNet.instance == null)
            {
                Log.LogDebug("Resetting configurations");

                TribeHelper.Reset();

                ConfigurationManager.LoadAllConfigurations();
            }
        }
    }
}
