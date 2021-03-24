using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valheim.EnhancedProgressTracker.ConfigurationCore;
using Valheim.EnhancedProgressTracker.ConfigurationTypes;
using Valheim.EnhancedProgressTracker.GlobalKey;
using Valheim.EnhancedProgressTracker.Tribe;

namespace Valheim.EnhancedProgressTracker.KillTracking
{
    [HarmonyPatch(typeof(Character))]
    public static class CreatureOnDeathPatch
    {
        [HarmonyPatch("OnDeath")]
        [HarmonyPrefix]
        private static void SetKeysOnDeath(Character __instance)
        {
            if(!__instance.IsOwner())
            {
                return;
            }

            string name = __instance.gameObject?.name;

            try
            {
                string cleanedName = name.Split('(')[0].Trim().ToUpperInvariant();
                var newKey = $"KILLED_{cleanedName}";

                ZoneSystem.instance.SetGlobalKey(newKey);

#if DEBUG
                Log.LogDebug($"Set global key {newKey}.");
#endif

                if(ConfigurationManager.GeneralConfig.TrackPlayersWithinDistance.Value > 0)
                {
                    List<ZNet.PlayerInfo> players = ZNet.instance.GetPlayerList();

                    var position = __instance.GetTransform().position;

                    List<ZNet.PlayerInfo> playersInDistance = new List<ZNet.PlayerInfo>(players.Count);
                    
                    foreach (var player in players)
                    {
                        var playerPosition = ZDOMan.instance.GetZDO(player.m_characterID).GetPosition();
#if DEBUG
                        Log.LogDebug($"Player at '{playerPosition}'. Death at '{position}'");
#endif


                        var distance = Vector3.Distance(playerPosition, position);
#if DEBUG
                        Log.LogDebug($"Player {player.m_name} is '{distance}' from killed creature. Must be within {ConfigurationManager.GeneralConfig.TrackPlayersWithinDistance.Value} to be tracked.");
#endif

                        if(distance <= ConfigurationManager.GeneralConfig.TrackPlayersWithinDistance.Value)
                        {
                            playersInDistance.Add(player);
                        }
                    }

                    string defaultKey = __instance.m_defeatSetGlobalKey;
                    bool hasDefaultKey = !string.IsNullOrWhiteSpace(defaultKey);

                    foreach (var player in playersInDistance)
                    {
                        string playerName = player.m_name;

                        //Set default key with player
                        if (hasDefaultKey)
                        {
                            string defaultPlayerKey = KeyHelper.GetPlayerKey(playerName, defaultKey);
                            ZoneSystem.instance.SetGlobalKey(defaultPlayerKey);
#if DEBUG
                            Log.LogDebug($"Set default global player key {defaultPlayerKey}.");
#endif
                        }

                        string playerKey = KeyHelper.GetPlayerKey(playerName, newKey);
                        ZoneSystem.instance.SetGlobalKey(playerKey);
#if DEBUG
                        Log.LogDebug($"Set global player key {playerKey}.");
#endif

                        if (TribeHelper.TryGetPlayerTribe(playerName, out string tribe))
                        {
                            //Set default key with tribe
                            if(hasDefaultKey)
                            {
                                string defaultTribeKey = KeyHelper.GetTribeKey(playerName, defaultKey);
                                ZoneSystem.instance.SetGlobalKey(defaultTribeKey);
#if DEBUG
                                Log.LogDebug($"Set default global tribe key {defaultTribeKey}.");
#endif
                            }

                            string tribeKey = KeyHelper.GetTribeKey(playerName, newKey);
                            ZoneSystem.instance.SetGlobalKey(tribeKey);
#if DEBUG
                            Log.LogDebug($"Set global tribe key {tribeKey}.");
#endif
                        }
                    }
                }
            }
            catch (Exception e) 
            { 
            }
        }
    }
}
