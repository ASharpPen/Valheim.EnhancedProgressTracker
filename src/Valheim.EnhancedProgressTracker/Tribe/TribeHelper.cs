using System.Collections.Generic;
using Valheim.EnhancedProgressTracker.ConfigurationCore;
using Valheim.EnhancedProgressTracker.ConfigurationTypes;

namespace Valheim.EnhancedProgressTracker.Tribe
{
    public static class TribeHelper
    {
        private static Dictionary<string, string> PlayerTribeTable;

        internal static void Reset()
        {
            PlayerTribeTable = null;
        }

        public static bool TryGetPlayerTribe(string playerName, out string tribeName)
        {
            if (PlayerTribeTable == null)
            {
                InitializeTribeTable();
            }

            if (TribeHelper.PlayerTribeTable.TryGetValue(playerName.Trim().ToUpperInvariant(), out string tribe))
            {
                tribeName = tribe;
                return true;
            }
            else
            {
                tribeName = null;
                return false;
            }
        }

        private static void InitializeTribeTable()
        {
            PlayerTribeTable = new Dictionary<string, string>();

            foreach (var tribe in ConfigurationManager.TribeConfigurations)
            {
                var tribeName = tribe.Key;

                foreach (var tribeMember in tribe.Value.Sections)
                {
                    var memberName = tribeMember.Value.Name.Value.Trim().ToUpperInvariant();

                    if (PlayerTribeTable.ContainsKey(memberName))
                    {
                        Log.LogWarning($"Player '{tribeMember.Value.Name.Value}' is in multiple tribes. Overriding last seen with tribe '{tribeName}'.");
                    }

                    PlayerTribeTable[memberName] = tribeName;
                }
            }
        }
    }
}
