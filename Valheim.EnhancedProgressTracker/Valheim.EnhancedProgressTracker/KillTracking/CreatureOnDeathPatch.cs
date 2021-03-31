using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Valheim.EnhancedProgressTracker.ConfigurationCore;
using Valheim.EnhancedProgressTracker.ConfigurationTypes;
using Valheim.EnhancedProgressTracker.GlobalKey.Shared;
using Valheim.EnhancedProgressTracker.Tribe;

namespace Valheim.EnhancedProgressTracker.KillTracking
{
    [HarmonyPatch(typeof(Character))]
    public static class CreatureOnDeathPatch
    {
        private static FieldInfo _globalVariables = AccessTools.Field(typeof(ZoneSystem), "m_globalKeys");

        [HarmonyPatch("OnDeath")]
        [HarmonyPrefix]
        private static void SetKeysOnDeath(Character __instance, ZNetView ___m_nview)
        {
            if(___m_nview is not null && !___m_nview.IsOwner())
            {
                return;
            }

            string name = __instance.gameObject?.name;

            if(string.IsNullOrWhiteSpace(__instance.gameObject?.name))
            {
                return;
            }

            try
            {
                string cleanedName = name.Split('(')[0].Trim().ToUpperInvariant();
                var newKey = $"KILLED_{cleanedName}";

                var existingKeys = _globalVariables.GetValue(ZoneSystem.instance) as HashSet<string> ?? new HashSet<string>(0);

#if DEBUG
                Log.LogDebug($"Found {existingKeys.Count} existing keys.");
#endif

                SetKey(newKey, existingKeys);

                //Check early escape. No reason to start doing more costly operations, if we aren't going to add record any more advanced keys.
                if(ConfigurationManager.GeneralConfig?.RecordPlayerKeys?.Value == false && ConfigurationManager.GeneralConfig?.RecordTribeKeys?.Value == false)
                {
                    return;
                }

                if(ConfigurationManager.GeneralConfig.TrackPlayersWithinDistance.Value > 0)
                {
                    List<ZNet.PlayerInfo> players = ZNet.instance.GetPlayerList();

                    var position = __instance.transform.position;

                    List<ZNet.PlayerInfo> playersInDistance = new List<ZNet.PlayerInfo>(players.Count);
                    
                    foreach (var player in players)
                    {
                        var playerZDO = ZDOMan.instance.GetZDO(player.m_characterID);


                        if(playerZDO is null)
                        {
                            Log.LogWarning("Unable to retrieve ZDO for player: " + player.m_name);
                            continue;
                        }

                        var playerPosition = playerZDO.GetPosition();
                        
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

                        if (ConfigurationManager.GeneralConfig?.RecordPlayerKeys?.Value == true)
                        {

                            //Set default key with player
                            if (hasDefaultKey)
                            {
                                string defaultPlayerKey = KeyHelper.GetPlayerKey(playerName, defaultKey);

                                SetKey(defaultPlayerKey, existingKeys);
                            }

                            string playerKey = KeyHelper.GetPlayerKey(playerName, newKey);
                            SetKey(playerKey, existingKeys);
                        }

                        if (ConfigurationManager.GeneralConfig.RecordTribeKeys.Value)
                        {
                            if (TribeHelper.TryGetPlayerTribe(playerName, out string tribe))
                            {
                                //Set default key with tribe
                                if (hasDefaultKey)
                                {
                                    string defaultTribeKey = KeyHelper.GetTribeKey(playerName, defaultKey);
                                    SetKey(defaultTribeKey, existingKeys);
                                }

                                string tribeKey = KeyHelper.GetTribeKey(playerName, newKey);
                                SetKey(tribeKey, existingKeys);
                            }
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                Log.LogError("Error while trying to add enhanced keys.", e);
            }
        }

        private static void SetKey(string newKey, HashSet<string> existingKeys)
        {
            if (!existingKeys.Contains(newKey))
            {
                Log.LogDebug("Setting global key: " + newKey);

                ZoneSystem.instance.SetGlobalKey(newKey);
            }
        }
    }
}
