using System.Collections.Generic;
using Valheim.EnhancedProgressTracker.GlobalKey.Shared;

namespace Valheim.EnhancedProgressTracker.GlobalKey
{
    internal static class EnhancedGlobalKey
    {
        internal static bool HasPlayerKey(Player player, string key, HashSet<string> globalKeys)
        {
            string playerKey = KeyHelper.GetPlayerKey(player, key);

            if (playerKey == null)
            {
                return false;
            }

            return globalKeys.Contains(playerKey);
        }

        internal static bool HasPlayerKey(string playerName, string key, HashSet<string> globalKeys)
        {
            string playerKey = KeyHelper.GetPlayerKey(playerName, key);

            if (playerKey == null)
            {
                return false;
            }

            return globalKeys.Contains(playerKey);
        }

        internal static bool HasTribeKey(Player player, string key, HashSet<string> globalKeys)
        {
            var tribeKey = KeyHelper.GetTribeKey(player, key);

            if (tribeKey == null)
            {
                //No tribe for player. Use global defaults.
                return globalKeys.Contains(key);
            }

            return globalKeys.Contains(tribeKey);
        }

        internal static bool HasTribeKey(string player, string key, HashSet<string> globalKeys)
        {
            var tribeKey = KeyHelper.GetTribeKey(player, key);

            if (tribeKey == null)
            {
                //No tribe for player. Use global defaults.
                return globalKeys.Contains(key);
            }

            return globalKeys.Contains(tribeKey);
        }
    }
}
