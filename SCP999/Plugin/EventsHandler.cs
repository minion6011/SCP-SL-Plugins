using Interactables.Interobjects.DoorUtils;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp049Events;
using LabApi.Events.Arguments.Scp079Events;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp106Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.Arguments.Scp914Events;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using ProjectMER.Features.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace SCP_999;


public class EventsHandler : CustomEventsHandler
{
    static System.Random rnd = new System.Random();
    public override void OnServerRoundStarted()
    {
        if (Server.PlayerCount >= Plugin.Singleton.Config.MinPlayer)
        {
            List<Player> players = new List<Player>();
            foreach (var player in Player.ReadyList)
            {
                if (player.Role != PlayerRoles.RoleTypeId.Overwatch) {
                    players.Add(player);
                }
            }

            int r = rnd.Next(players.Count);
            SCP999.Spawn(players[r]);
            players.Remove(players[r]);
        }
    }

    public override void OnPlayerDying(PlayerDyingEventArgs ev) {
        if (ev.Player == SCP999.Player999) {
            SCP999.Player999.ClearInventory();
            KeycardItem.CreateCustomKeycardMetal(
                targetPlayer: SCP999.Player999,
                itemName: "SCP-999 Keycard",
                holderName: "SCP-999",
                cardLabel: "SCP-999",
                permissions: new KeycardLevels(3, 3, 3),
                keycardColor: Color.yellow,
                permissionsColor: Color.yellow,
                labelColor: Color.white,
                wearLevel: 0,
                serialLabel: "SCP-999"
            );
        }
    }

    public override void OnPlayerLeft(PlayerLeftEventArgs ev)
    {
        if (ev.Player == SCP999.Player999 && SCP999.Schematic999 != null)
        {
            SCP999.Kill();
        }
    }
    public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
    {
        try
        {
            // Size Fix
            if (SCP999.Player999 != null && ev.Player != null)
            {
                ev.Player.SpawnNetworkIdentity(SCP999.Player999.ReferenceHub.netIdentity);
            }
        } catch {}
    }
    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        try { 
            // Size Fix
            if (SCP999.Player999 != null && ev.Player != null)
            {
                ev.Player.SpawnNetworkIdentity(SCP999.Player999.ReferenceHub.netIdentity);
            }
            // SCP 999 Death
            if (SCP999.Player999 != null && ev.Player != null && ev.Player == SCP999.Player999)
            {
                SCP999.Kill();
            }
        } catch {}
    }

    // Events Blocked
    public override void OnPlayerDroppingItem(PlayerDroppingItemEventArgs ev) {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
    public override void OnPlayerPickingUpItem(PlayerPickingUpItemEventArgs ev)
    {
        // SCP-1162 Fix
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999)
        {
            if (ev.Pickup.Type == ItemType.SCP500) 
            {
                ev.IsAllowed = false;
                ev.Pickup.IsInUse = false;
            }
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
            if (ev.Player.Ammo.Count > 0) {
                ev.Player.DropAmmo(item: ev.AmmoType, amount: ev.AmmoAmount);
            }
            
        }
    }
    public override void OnPlayerPickedUpArmor(PlayerPickedUpArmorEventArgs ev) {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999){
            ev.BodyArmorItem.DropItem();
        }
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
    public override void OnPlayerSpawningRagdoll(PlayerSpawningRagdollEventArgs ev) {
        if (SCP999.Player999 != null && ev.Player == SCP999.Player999) {
            ev.IsAllowed = false;
        }
    }
}
