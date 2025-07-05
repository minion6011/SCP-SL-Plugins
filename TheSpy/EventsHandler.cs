using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System.Collections.Generic;
using System.Linq;

namespace TheSpy;


public class EventsHandler : CustomEventsHandler
{
    static System.Random rnd = new System.Random();
    public override void OnServerWaveRespawned(WaveRespawnedEventArgs ev)
    {
        if (ev.Players.Count >= Plugin.Singleton.Config.MinWaveSize)
        {
            int r = rnd.Next(ev.Players.Count);
            SpyManager.Spawn(ev.Players[r]);
        }
    }
    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        if (ev.Player != null && SpyManager.SpyPlayers.Contains(ev.Player))
        {
            SpyManager.Kill(ev.Player);
        }
        SpyManager.EndRoundCheck();
    }
    public override void OnPlayerHurting(PlayerHurtingEventArgs ev)
    {
        if (ev.Attacker != null)
        {
            // Taking
            if (SpyManager.SpyPlayers.Contains(ev.Attacker))
            {
                if ((ev.Player.IsNTF || ev.Player.Role == PlayerRoles.RoleTypeId.Scientist || ev.Attacker.Role == PlayerRoles.RoleTypeId.FacilityGuard) && ev.Attacker != ev.Player)
                {
                    if (ev.Attacker.IsNTF && !SpyManager.SpyPlayers.Contains(ev.Player) || (SpyManager.SpyPlayers.Contains(ev.Player) && ev.Player.Role != ev.Attacker.Role && SpyManager.SpyPlayers.Contains(ev.Attacker)))
                    {
                        ev.IsAllowed = true;
                        if (ev.DamageHandler is PlayerStatsSystem.StandardDamageHandler standardDamageHandler)
                        {
                            ev.Player.Damage(amount: standardDamageHandler.Damage, reason: Plugin.Singleton.Config.DamageReason);
                        }
                    }
                    else if (ev.Attacker.IsChaos) { ev.IsAllowed = false; }
                }
                else if ((ev.Player.IsChaos || ev.Player.Role == PlayerRoles.RoleTypeId.ClassD) && ev.Attacker != ev.Player)
                {
                    if (ev.Attacker.IsChaos && !SpyManager.SpyPlayers.Contains(ev.Player) || (SpyManager.SpyPlayers.Contains(ev.Player) && ev.Player.Role != ev.Attacker.Role && SpyManager.SpyPlayers.Contains(ev.Attacker)))
                    {
                        ev.IsAllowed = true;
                        if (ev.DamageHandler is PlayerStatsSystem.StandardDamageHandler standardDamageHandler)
                        {
                            ev.Player.Damage(amount: standardDamageHandler.Damage, reason: Plugin.Singleton.Config.DamageReason);
                        }
                    }
                    else if (ev.Attacker.IsNTF) { ev.IsAllowed = false; }
                }
            }
            // Hurting
            else if (SpyManager.SpyPlayers.Contains(ev.Player))
            {
                if ((ev.Attacker.IsNTF || ev.Attacker.Role == PlayerRoles.RoleTypeId.Scientist || ev.Attacker.Role == PlayerRoles.RoleTypeId.FacilityGuard) && ev.Attacker != ev.Player)
                {
                    if (ev.Player.IsNTF && !SpyManager.SpyPlayers.Contains(ev.Attacker) || (SpyManager.SpyPlayers.Contains(ev.Player) && ev.Player.Role != ev.Attacker.Role && SpyManager.SpyPlayers.Contains(ev.Attacker)))
                    {
                        if (ev.DamageHandler is PlayerStatsSystem.StandardDamageHandler standardDamageHandler)
                        {
                            ev.Player.Damage(amount: standardDamageHandler.Damage, reason: Plugin.Singleton.Config.DamageReason);
                        }
                    }
                    else if (ev.Player.IsChaos) { ev.IsAllowed = false; }
                }
                else if ((ev.Attacker.IsChaos || ev.Attacker.Role == PlayerRoles.RoleTypeId.ClassD) && ev.Attacker != ev.Player)
                {
                    if (ev.Player.IsChaos && !SpyManager.SpyPlayers.Contains(ev.Attacker) || (SpyManager.SpyPlayers.Contains(ev.Player) && ev.Player.Role != ev.Attacker.Role && SpyManager.SpyPlayers.Contains(ev.Attacker)))
                    {
                        if (ev.DamageHandler is PlayerStatsSystem.StandardDamageHandler standardDamageHandler)
                        {
                            ev.Player.Damage(amount: standardDamageHandler.Damage, reason: Plugin.Singleton.Config.DamageReason);
                        }
                    }
                    else if (ev.Player.IsNTF) { ev.IsAllowed = false; }
                }
            }

        }
    }
    public override void OnServerRoundEndingConditionsCheck(RoundEndingConditionsCheckEventArgs ev)
    {
        if (Player.ReadyList.Count() > 1 && SpyManager.SpyPlayers.Count() > 0 && !Round.IsLocked)
        {
            int totalPlayers = 0;
            int totalNTF = 0;
            int totalChaos = 0;
            foreach (Player playerList in Player.ReadyList)
            {
                if (playerList != null && playerList.IsAlive)
                {
                    totalPlayers += 1;
                    if ((playerList.IsNTF && !SpyManager.SpyPlayers.Contains(playerList)) || playerList.Role == PlayerRoles.RoleTypeId.Scientist || playerList.Role == PlayerRoles.RoleTypeId.FacilityGuard || (playerList.IsChaos && SpyManager.SpyPlayers.Contains(playerList)))
                    {
                        totalNTF += 1;
                    }
                    else if ((playerList.IsChaos && !SpyManager.SpyPlayers.Contains(playerList)) || playerList.Role == PlayerRoles.RoleTypeId.ClassD || (playerList.IsNTF && SpyManager.SpyPlayers.Contains(playerList)))
                    {
                        totalChaos += 1;
                    }
                }
            }
            // End Round Check
            if (totalPlayers == totalNTF || totalPlayers == totalChaos)
            {
                ev.CanEnd = true;
                Round.End(true);
            }
            else {
                ev.CanEnd = false;
            }
        }
        else {
            ev.CanEnd = true;
        }
    }
}
