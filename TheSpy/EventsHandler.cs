using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;

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
    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev) {
        if (SpyManager.SpyPlayers.Contains(ev.Player))
        {
            SpyManager.Kill(ev.Player);
        }
    }
    public override void OnPlayerHurting(PlayerHurtingEventArgs ev)
    {
        if (ev.Attacker != null)
        {
            // Taking
            if (SpyManager.SpyPlayers.Contains(ev.Attacker))
            {
                if (ev.Player.IsNTF || ev.Player.Role == PlayerRoles.RoleTypeId.Scientist || ev.Attacker.Role == PlayerRoles.RoleTypeId.FacilityGuard)
                {
                    if (ev.Attacker.IsNTF)
                    {
                        ev.IsAllowed = true;
                        if (ev.DamageHandler is PlayerStatsSystem.StandardDamageHandler standardDamageHandler)
                        {
                            ev.Player.Damage(amount: standardDamageHandler.Damage, reason: Plugin.Singleton.Config.DamageReason);
                        }
                    }
                    else if (ev.Attacker.IsChaos) { ev.IsAllowed = false; }
                }
                else if (ev.Player.IsChaos || ev.Player.Role == PlayerRoles.RoleTypeId.ClassD)
                {
                    if (ev.Attacker.IsChaos)
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
                if (ev.Attacker.IsNTF || ev.Attacker.Role == PlayerRoles.RoleTypeId.Scientist || ev.Attacker.Role == PlayerRoles.RoleTypeId.FacilityGuard)
                {
                    if (ev.Player.IsNTF)
                    {
                        if (ev.DamageHandler is PlayerStatsSystem.StandardDamageHandler standardDamageHandler)
                        {
                            ev.Player.Damage(amount: standardDamageHandler.Damage, reason: Plugin.Singleton.Config.DamageReason);
                        }
                    }
                    else if (ev.Player.IsChaos) { ev.IsAllowed = false; }
                }
                else if (ev.Attacker.IsChaos || ev.Attacker.Role == PlayerRoles.RoleTypeId.ClassD)
                {
                    if (ev.Player.IsChaos)
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
}