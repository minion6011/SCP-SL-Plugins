using CustomPlayerEffects;
using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Wrappers;
using MEC;
using ProjectMER.Features;
using ProjectMER.Features.Extensions;
using ProjectMER.Features.Objects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SCP999;

public class SCP999
{
    public static Player Player999 { get; private set; } = null;
    public static SchematicObject Schematic999 { get; set; } = null;
    private static bool IsStarted { get; set; } = false;

    public static void Spawn(Player player)
    {
        player.SendHint(text: Plugin.Singleton.Config.HintMsg, duration: Plugin.Singleton.Config.HintDuration);
        Door.Get("GR18").IsOpened = true;
        Door.Get("GR18_INNER").IsOpened = true;
        player.SetRole(newRole: PlayerRoles.RoleTypeId.Tutorial);
        player.CustomInfo = "SCP-999";
        Player999 = player;
        player.MaxHealth = 999;
        player.Health = 999;
        if (!player.Items.Any()) {
            KeycardItem.CreateCustomKeycardMetal(
                targetPlayer: player,
                itemName: "SCP-999 Keycard",
                holderName: "SCP-999",
                cardLabel: "SCP-999",
                permissions: new KeycardLevels(1, 0, 1),
                keycardColor: Color.yellow,
                permissionsColor: Color.yellow,
                labelColor: Color.white,
                wearLevel: 0,
                serialLabel: "SCP-999"
            );
            player.AddItem(item: ItemType.Lantern);
            // Full invetory - SCP-1162 Fix
            player.AddItem(item: ItemType.Lantern);
            player.AddItem(item: ItemType.Lantern);
            player.AddItem(item: ItemType.Lantern);
            player.AddItem(item: ItemType.Lantern);
            player.AddItem(item: ItemType.Lantern);
            player.AddItem(item: ItemType.Lantern);
        }
        Timing.CallDelayed(0.4f, () => player.Scale = new(0.4f, 0.4f, 0.4f));

        Schematic999 = ObjectSpawner.SpawnSchematic("SCP999", player.Position + new Vector3(0, -0.45f, 0), player.Rotation, new Vector3(1.35f, 1.35f, 1.35f));
        player.Position = Door.Get("GR18_INNER").Position + new Vector3(1, 2, 1);

        foreach (var networkIdentity in Schematic999.NetworkIdentities)
        {
            player.DestroyNetworkIdentity(networkIdentity);
        }
        if (Plugin.Singleton.Config.CassieMsg) {
            Cassie.Message(message: "SCP 999 detected in the facility.", isNoisy: true, isSubtitles: true, customSubtitles: "SCP-999 rilevato nella struttura");
        }
        if (!IsStarted) {
            IsStarted = true;
            Timing.RunCoroutine(PositionCoroutine(), "SCP999PositionCoroutine");
        }
        
    }

    public static void Kill()
    {
        if (Player999 != null)
        {
            Player999.CustomInfo = null;
            if (Schematic999 != null)
            {
                SchematicObject.Destroy(Schematic999);
                Schematic999 = null;
            }
            foreach (var player in Player.ReadyList)
            {
                player.SpawnNetworkIdentity(Player999.ReferenceHub.netIdentity);
            }

            Player999 = null;
            if (Plugin.Singleton.Config.CassieMsg)
            {
                Cassie.Message(message: "SCP 999 Successful Terminated Termination Cause Unspecified", isNoisy: true, isSubtitles: true, customSubtitles: "SCP-999 ricontenuto con successo. Causa Anomala.");
            }
        }
    }

    internal static IEnumerator<float> PositionCoroutine()
    {
        while (true)
        {
            if (Player999 != null && Player999.IsAlive)
            {
                Player999.Heal(0.1f);

                if (Schematic999 != null)
                {
                    Schematic999.Position = Player999.Position + new Vector3(0, -0.45f, 0);
                    Schematic999.Rotation = Player999.Rotation;
                }
                // Ability
                foreach (var player in Player.ReadyList) {
                    if (
                        player != Player999 
                        && // Position X
                        player.Position.x <= Player999.Position.x + Plugin.Singleton.Config.AbilityRadius
                        &&
                        player.Position.x >= Player999.Position.x - Plugin.Singleton.Config.AbilityRadius
                        && // Position Y
                        player.Position.y <= Player999.Position.y + Plugin.Singleton.Config.AbilityRadius
                        &&
                        player.Position.y >= Player999.Position.y - Plugin.Singleton.Config.AbilityRadius
                        && // Position Z
                        player.Position.z <= Player999.Position.z + Plugin.Singleton.Config.AbilityRadius
                        &&
                        player.Position.z >= Player999.Position.z - Plugin.Singleton.Config.AbilityRadius
                        )
                    {
                        if (player.IsSCP)
                        {
                            player.EnableEffect<Slowness>(intensity: 20, duration: 0.15f);
                        }
                        else {
                            player.Heal(0.05f);
                        }
                    }

                }
            }
            yield return Timing.WaitForSeconds(0.015f);
        }
    }

}
