using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valheim.EnhancedProgressTracker.Tribe;

namespace Valheim.EnhancedProgressTracker.KillTracking
{
    /*
    [HarmonyPatch(typeof(Character))]
    public static class CreatureKillContributorPatch
    {
        [HarmonyPatch("OnDamaged")]
        [HarmonyPostfix]
        public static void TrackDamageContributor(Character __instance, HitData hit)
        {
            // Check if player.
            Character attacker = hit.GetAttacker();
            Player player = attacker.GetComponent<Player>();

            if (player)
            {
                var name = player.GetHoverName();

                if(TribeHelper.TryGetPlayerTribe(name, out string tribe))
                {

                }
            }


            // Get player name

            // Get tribe name

            // Add contributor name to table.

        }
    }*/
}
