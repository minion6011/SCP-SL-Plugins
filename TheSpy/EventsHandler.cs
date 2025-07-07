using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
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
            List<Player> ListSoldier = ev.Players.Where(x => x != null && x.Role == PlayerRoles.RoleTypeId.NtfPrivate).ToList();
            int r = rnd.Next(ListSoldier.Count);
            SpyManager.Spawn(ListSoldier[r]);
        }
    }
    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        if (ev.Player != null && SpyManager.SpyPlayers.Contains(ev.Player))
        {
            SpyManager.Kill(ev.Player);
        }
        if (SpyManager.EndRoundCheck()) {
            Round.End();
        }
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
                            ev.Player.Damage(amount: standardDamageHandler.Damage, reason: "Ucciso da una spia");
                            ev.Attacker.SendHitMarker();
                            if (!SpyManager.HitmarkOnPlayers.Contains(ev.Player)) {
                                SpyManager.HitmarkOnPlayers.Add(ev.Attacker); 
                            }

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
                            ev.Player.Damage(amount: standardDamageHandler.Damage, reason: "Ucciso da una spia");
                            ev.Attacker.SendHitMarker();
                            if (!SpyManager.HitmarkOnPlayers.Contains(ev.Player)) {
                                SpyManager.HitmarkOnPlayers.Add(ev.Attacker);
                            }
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
                            if (SpyManager.HitmarkOnPlayers.Contains(ev.Player))
                            {
                                ev.Player.Damage(amount: standardDamageHandler.Damage, reason: $"Ucciso da {ev.Attacker.DisplayName}");
                                ev.Attacker.SendHitMarker();
                                SpyManager.HitmarkOnPlayers.Add(ev.Attacker);
                            }
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
                            if (SpyManager.HitmarkOnPlayers.Contains(ev.Player))
                            {
                                ev.Player.Damage(amount: standardDamageHandler.Damage, reason: $"Ucciso da {ev.Attacker.DisplayName}");
                                ev.Attacker.SendHitMarker();
                                SpyManager.HitmarkOnPlayers.Add(ev.Attacker);
                            }
                        }
                    }
                    else if (ev.Player.IsNTF) { ev.IsAllowed = false; }
                }
            }

        }
    }
    public override void OnServerRoundEndingConditionsCheck(RoundEndingConditionsCheckEventArgs ev)
    {
        if (SpyManager.EndRoundCheck())
        {
            ev.CanEnd = true;
            Round.End(true);
        }
        else
        {
            ev.CanEnd = false;
        }
    }
}
