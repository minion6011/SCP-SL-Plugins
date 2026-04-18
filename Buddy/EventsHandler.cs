using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace Buddy;


public class EventsHandler : CustomEventsHandler
{
    static RoleTypeId[] scpRoles = { RoleTypeId.Scp049, RoleTypeId.Scp079, RoleTypeId.Scp096, RoleTypeId.Scp106, RoleTypeId.Scp173, RoleTypeId.Scp939, RoleTypeId.Scp3114};
    static System.Random rnd = new System.Random();
    public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
    {
        if (!Plugin.Buddies.ContainsKey(ev.Player.UserId))
        {
            ev.Player.SendConsoleMessage(Plugin.Singleton.Config.ServerJoinMsg, Plugin.Singleton.Config.MsgColor);
        }
        else
        {
            if (!Plugin.Buddies.TryGetValue(ev.Player.UserId, out string buddy1) || buddy1 == null)
            {
                Plugin.Buddies.Remove(ev.Player.UserId);
                Plugin.Singleton.RemovePerson(ev.Player.UserId);
            }
            else
            {
                Player player = Player.Get(buddy1);
                if (player == null) return;
                if (Plugin.Singleton.Config.JoinMsg)
                    ev.Player.SendHint(Plugin.Singleton.Config.JoinedMsg.Replace("$name", player.Nickname), Plugin.Singleton.Config.HintDuration);
            }
        }
    }
    public override void OnServerRoundRestarted()
    {
        if (Plugin.Singleton.Config.ResetBuddiesEveryRound)
            Plugin.Buddies= new Dictionary<string, string>();
    }
    public override void OnServerRoundStarted()
    {
        Timing.RunCoroutine(SetRoles());
    }


    private IEnumerator<float> SetRoles()
    {
        yield return Timing.WaitForSeconds(1f);

        List<string> doneIDs = new List<string>();
        IEnumerable<string> onlinePlayers = Player.List.Select(x => x.UserId);

        IEnumerable<string> hubs = Plugin.Buddies.Values;
        for (int i = 0; i < hubs.Count(); i++)
        {
            string id = hubs.ElementAt(i);
            Player player = Player.Get(id);
            if (player == null) continue;
            //check if player has a buddy
            if (Plugin.Buddies.ContainsKey(player.UserId))
            {
                try
                {
                    if (!Plugin.Buddies.TryGetValue(player.UserId, out string buddy1) || buddy1 == null || !onlinePlayers.Contains(id) || !onlinePlayers.Contains(buddy1))
                    {
                        Plugin.Buddies.Remove(id);
                        if (buddy1 != null) Plugin.Buddies.Remove(buddy1);
                        else Plugin.Singleton.RemovePerson(id);
                        doneIDs.Add(buddy1);
                        doneIDs.Add(id);
                        continue;
                    }
                    if ((doneIDs.Contains(id) || doneIDs.Contains(buddy1))) continue;
                    Player buddy = Player.Get(buddy1);
                    if (buddy == null) continue;
                    //take action if they have different roles
                    if (player.Role != buddy.Role &&
                        /* massive check for scientist/guard combo */
                        !(!Plugin.Singleton.Config.DisallowGuardScientistCombo && ((player.Role == RoleTypeId.FacilityGuard && buddy.Role == RoleTypeId.Scientist) || (player.Role == RoleTypeId.Scientist && buddy.Role == RoleTypeId.FacilityGuard)))
                        )
                    {
                        //SCPs take priority
                        if (buddy.Team == Team.SCPs) continue;

                        //if force exact role is on we can just set the buddy to the other player's role
                        if (Plugin.Singleton.Config.ForceExactRole)
                        {
                            buddy.SetRole(player.Role);
                            doneIDs.Add(buddy1);
                            doneIDs.Add(id);
                            continue;
                        }
                        //if they are an scp, we need to remove another scp first
                        if (player.Team == Team.SCPs)
                        {
                            //loop through every scp and swap the buddy with one of them
                            Boolean setRole = false;
                            foreach (Player hub in Player.List)
                            {
                                Player player1 = hub;
                                //check if the player is an scp
                                if (player1.UserId != id && player1.UserId != buddy1 && !Plugin.Buddies.ContainsKey(player1.UserId) && player1.Team == Team.SCPs)
                                {
                                    //set the buddy to that player's role and set the player to classd
                                    buddy.SetRole(player1.Role);
                                    player1.SetRole(RoleTypeId.ClassD);
                                    setRole = true;
                                    doneIDs.Add(buddy1);
                                    doneIDs.Add(id);
                                    break;
                                }
                            }
                            //if their role is not set (their buddy is the sole scp), set them to a random scp
                            if (!setRole)
                            {
                                List<RoleTypeId> roles = new List<RoleTypeId>(scpRoles);
                                roles.Remove(player.Role);
                                buddy.SetRole(roles[rnd.Next(roles.Count)]);
                                doneIDs.Add(buddy1);
                                doneIDs.Add(id);
                            }
                            continue;
                        }
                        //if they are not an scp, we can just set them to the same role as their buddy
                        buddy.SetRole(player.Role);
                        doneIDs.Add(buddy1);
                        doneIDs.Add(id);
                    }
                }
                catch
                {
                    try
                    {
                        Plugin.Buddies.Remove(id);
                    }
                    catch (ArgumentException) { }
                    Plugin.Singleton.RemovePerson(id);
                    doneIDs.Add(id);
                    continue;
                }
            }

        }
    }

}