using Valheim.EnhancedProgressTracker.Tribe;

namespace Valheim.EnhancedProgressTracker.GlobalKey.Shared
{
    public static class KeyHelper
    {
        public static string GetPlayerKey(Player player, string key)
        {
            if(player is null)
            {
                return null;
            }

            return GetPlayerKey(player.GetPlayerName(), key);
        }

        public static string GetPlayerKey(string playerName, string key)
        {
            return $"{key}:Player:{playerName}";
        }

        public static string GetTribeKey(Player player, string key)
        {
            if(player is null)
            {
                return null;
            }

            return GetTribeKey(player.GetPlayerName(), key);
        }

        public static string GetTribeKey(string playerName, string key)
        {
            if (TribeHelper.TryGetPlayerTribe(playerName, out string tribe))
            {
                return $"{key}:Tribe:{tribe}";
            }

            return null;
        }
    }
}
