using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp049Events;
using LabApi.Events.Arguments.Scp079Events;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp106Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.Arguments.Scp914Events;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MapGeneration;
using PlayerRoles.Spectating;
using ProjectMER.Features.Extensions;
using System;
using System.Collections.Generic;

namespace SCP999;

public class EventsHandler : CustomEventsHandler
{
    static Random rnd = new Random();
    public override void OnServerRoundStarted()
    {
        if (Server.PlayerCount >= Plugin.Singleton.Config.MinPlayer)
        {
            List<Player> players = new List<Player>();
            foreach (var player in Player.ReadyList)
            {
                 players.Add(player);
            }

            int r = rnd.Next(players.Count);
            SCP999.Spawn(players[r]);
        }
    }

    public override void OnPlayerLeft(PlayerLeftEventArgs ev)
    {
        if (ev.Player == SCP999.Player999 && SCP999.Schematic999 != null)
        {
            SCP999.Kill();
        }
    }
    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999)
        {
            SCP999.Kill();
        }
        else
        {
            if (SCP999.Player999 != null) {
                if (ev.NewRole.RoleTypeId == PlayerRoles.RoleTypeId.Spectator) {
                    Timing.CallDelayed(0.3f, () => ev.Player.SpawnNetworkIdentity(SCP999.Player999.ReferenceHub.netIdentity));
                }
                else {
                    Timing.CallDelayed(0.3f, () => ev.Player.DestroyNetworkIdentity(SCP999.Player999.ReferenceHub.netIdentity));
                }
            }
        }
    }

    // Events Blocked
    public override void OnPlayerDroppingItem(PlayerDroppingItemEventArgs ev) {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnPlayerPickedUpItem(PlayerPickedUpItemEventArgs ev) {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999) {
            ev.Item.DropItem();
        }
    }
    public override void OnPlayerPickedUpAmmo(PlayerPickedUpAmmoEventArgs ev)
    {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999) {
            ev.AmmoPickup.Destroy();
        }
    }
    public override void OnPlayerPickedUpArmor(PlayerPickedUpArmorEventArgs ev) {
        ev.BodyArmorItem.DropItem();
    }
    public override void OnPlayerCuffing(PlayerCuffingEventArgs ev) {
        if (SCP999.Player999 != null && ev.Target == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    // SCP Events Blocked
    public override void OnScp096AddingTarget(Scp096AddingTargetEventArgs ev) {
        if (SCP999.Player999 != null && ev.Target == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnScp049UsingSense(Scp049UsingSenseEventArgs ev) {
        if (SCP999.Player999 != null && ev.Target == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnScp106TeleportingPlayer(Scp106TeleportingPlayerEvent ev) {
        if (SCP999.Player999 != null && ev.Target == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnPlayerEnteringHazard(PlayerEnteringHazardEventArgs ev) {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnPlayerActivatingGenerator(PlayerActivatingGeneratorEventArgs ev) {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnScp079GainingExperience(Scp079GainingExperienceEventArgs ev) {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnScp079Recontaining(Scp079RecontainingEventArgs ev) {
        if (SCP999.Player999 != null && ev.Activator == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnScp173AddingObserver(Scp173AddingObserverEventArgs ev) {
        if (ev.Target == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnScp914ProcessingInventoryItem(Scp914ProcessingInventoryItemEventArgs ev) {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999) { 
            ev.IsAllowed = false;
        }
    }
    public override void OnPlayerInteractingScp330(PlayerInteractingScp330EventArgs ev) {
        if (ev.Player == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnPlayerHurting(PlayerHurtingEventArgs ev) {
        if (ev.Player != null && SCP999.Player999 != null && SCP999.Player999.IsAlive && ev.Player == SCP999.Player999) {
            if (ev.Attacker != null && ev.Attacker.IsSCP) {
                ev.IsAllowed = false;
            }
        }
    }
}
