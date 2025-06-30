using LabApi.Features.Wrappers;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZombiCustom;

public class ZombiAbilityManager
{
    static Dictionary<Player, Int32> PlayerCooldown = new Dictionary<Player, Int32>();
    static System.Random rnd = new System.Random();
    public static void ActivateAbility(Player player)
    {
        List<Player> playerlistSorted = new List<Player>();
        int unixTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        // - Common
        // Kamikaze
        if (player.CustomInfo == "Kamikaze")
        {
            TimedGrenadeProjectile.SpawnActive(pos: player.Position, type: ItemType.GrenadeHE, owner: player);
        }
        // Il cagatore
        else if (player.CustomInfo == "Il Cagatore")
        {
            if ((PlayerCooldown.ContainsKey(player) && unixTime >= PlayerCooldown[player]) || !PlayerCooldown.ContainsKey(player))
            {
                TantrumHazard.Spawn(position: player.Position, rotation: player.Rotation, scale: Vector3.one);
                PlayerCooldown[player] = unixTime + Plugin.Singleton.Config.CagatoreCooldown;

            }
            else
            {
                player.SendHint($"<color=red>Cooldown</color>\nAspetta {PlayerCooldown[player] - unixTime}s prima di\npoter riutilizzare l'abilità", Plugin.Singleton.Config.HintCooldownDuration);
            }
        }
        // Il lanciatore
        else if (player.CustomInfo == "Il Lanciatore")
        {
            if ((PlayerCooldown.ContainsKey(player) && unixTime >= PlayerCooldown[player]) || !PlayerCooldown.ContainsKey(player))
            {
                Item item = player.AddItem(ItemType.GrenadeHE);
                player.CurrentItem = item;
                PlayerCooldown[player] = unixTime + Plugin.Singleton.Config.LanciatoreCooldown;
            }
            else
            {
                player.SendHint($"<color=red>Cooldown</color>\nAspetta {PlayerCooldown[player] - unixTime}s prima di\npoter riutilizzare l'abilità", Plugin.Singleton.Config.HintCooldownDuration);
            }
        }
        // - Uncommon
        // Urlatore
        else if (player.CustomInfo == "Urlatore")
        {
            if ((PlayerCooldown.ContainsKey(player) && unixTime >= PlayerCooldown[player]) || !PlayerCooldown.ContainsKey(player))
            {
                foreach (Player playerlist in Player.ReadyList)
                {
                    if (// Position X
                        playerlist.Position.x <= player.Position.x + Plugin.Singleton.Config.UrlatoreAbilityRadius
                        &&
                        playerlist.Position.x >= player.Position.x - Plugin.Singleton.Config.UrlatoreAbilityRadius
                        && // Position Y
                        playerlist.Position.y <= player.Position.y + Plugin.Singleton.Config.UrlatoreAbilityRadius
                        &&
                        playerlist.Position.y >= player.Position.y - Plugin.Singleton.Config.UrlatoreAbilityRadius
                        && // Position Z
                        playerlist.Position.z <= player.Position.z + Plugin.Singleton.Config.UrlatoreAbilityRadius
                        &&
                        playerlist.Position.z >= player.Position.z - Plugin.Singleton.Config.UrlatoreAbilityRadius
                        )
                    {
                        if (playerlist != player && (!playerlist.IsSCP || playerlist.Role != PlayerRoles.RoleTypeId.Tutorial))
                        {
                            playerlist.EnableEffect<CustomPlayerEffects.Deafened>(intensity: 255, duration: Plugin.Singleton.Config.UrlatoreEffectDuration);
                        }
                    }
                }
                PlayerCooldown[player] = unixTime + Plugin.Singleton.Config.UrlatoreCooldown;
            }
            else
            {
                player.SendHint($"<color=red>Cooldown</color>\nAspetta {PlayerCooldown[player] - unixTime}s prima di\npoter riutilizzare l'abilità", Plugin.Singleton.Config.HintCooldownDuration);
            }
        }
        // Light Eater
        else if (player.CustomInfo == "Light Eater")
        {
            if ((PlayerCooldown.ContainsKey(player) && unixTime >= PlayerCooldown[player]) || !PlayerCooldown.ContainsKey(player))
            {
                player.Room.LightController.LightsEnabled = false;
                PlayerCooldown[player] = unixTime + Plugin.Singleton.Config.LightEaterCooldown;
                Timing.CallDelayed(Plugin.Singleton.Config.LightEaterAbilityDuration, () => player.Room.LightController.LightsEnabled = true);

            }
            else
            {
                player.SendHint($"<color=red>Cooldown</color>\nAspetta {PlayerCooldown[player] - unixTime}s prima di\npoter riutilizzare l'abilità", Plugin.Singleton.Config.HintCooldownDuration);
            }
        }
        // Battalion’s Backup
        else if (player.CustomInfo == "Battalion’s Backup")
        {
            if ((PlayerCooldown.ContainsKey(player) && unixTime >= PlayerCooldown[player]) || !PlayerCooldown.ContainsKey(player))
            {
                foreach (Player playerlist in Player.ReadyList)
                {
                    if (// Position X
                        playerlist.Position.x <= player.Position.x + Plugin.Singleton.Config.BattalionAbilityRadius
                        &&
                        playerlist.Position.x >= player.Position.x - Plugin.Singleton.Config.BattalionAbilityRadius
                        && // Position Y
                        playerlist.Position.y <= player.Position.y + Plugin.Singleton.Config.BattalionAbilityRadius
                        &&
                        playerlist.Position.y >= player.Position.y - Plugin.Singleton.Config.BattalionAbilityRadius
                        && // Position Z
                        playerlist.Position.z <= player.Position.z + Plugin.Singleton.Config.BattalionAbilityRadius
                        &&
                        playerlist.Position.z >= player.Position.z - Plugin.Singleton.Config.BattalionAbilityRadius
                        )
                    {
                        if (playerlist != player && playerlist.Role == PlayerRoles.RoleTypeId.Scp0492)
                        {
                            playerlist.EnableEffect<CustomPlayerEffects.DamageReduction>(intensity: Plugin.Singleton.Config.BattalionAbilityIntesity, duration: Plugin.Singleton.Config.BattalionAbilityDuration);
                        }
                    }
                }
                PlayerCooldown[player] = unixTime + Plugin.Singleton.Config.BattalionCooldown;
            }
            else
            {
                player.SendHint($"<color=red>Cooldown</color>\nAspetta {PlayerCooldown[player] - unixTime}s prima di\npoter riutilizzare l'abilità", Plugin.Singleton.Config.HintCooldownDuration);
            }
        }
        // - Rare
        // Supporter
        else if (player.CustomInfo == "Supporter")
        {
            if ((PlayerCooldown.ContainsKey(player) && unixTime >= PlayerCooldown[player]) || !PlayerCooldown.ContainsKey(player))
            {
                foreach (Player playerlist in Player.ReadyList)
                {
                    if (// Position X
                        playerlist.Position.x <= player.Position.x + Plugin.Singleton.Config.SupportertAbilityRadius
                        &&
                        playerlist.Position.x >= player.Position.x - Plugin.Singleton.Config.SupportertAbilityRadius
                        && // Position Y
                        playerlist.Position.y <= player.Position.y + Plugin.Singleton.Config.SupportertAbilityRadius
                        &&
                        playerlist.Position.y >= player.Position.y - Plugin.Singleton.Config.SupportertAbilityRadius
                        && // Position Z
                        playerlist.Position.z <= player.Position.z + Plugin.Singleton.Config.SupportertAbilityRadius
                        &&
                        playerlist.Position.z >= player.Position.z - Plugin.Singleton.Config.SupportertAbilityRadius
                        )
                    {
                        if (playerlist != player && (playerlist.IsSCP || playerlist.Role == PlayerRoles.RoleTypeId.Tutorial))
                        {
                            playerlist.Heal(amount: (playerlist.MaxHealth / 100) * Plugin.Singleton.Config.SupporterAbilityPercent);
                        }
                    }
                }
                PlayerCooldown[player] = unixTime + Plugin.Singleton.Config.SupporterCooldown;
            }
            else
            {
                player.SendHint($"<color=red>Cooldown</color>\nAspetta {PlayerCooldown[player] - unixTime}s prima di\npoter riutilizzare l'abilità", Plugin.Singleton.Config.HintCooldownDuration);
            }
        }
        // Il Ruttatore
        else if (player.CustomInfo == "Il Ruttatore")
        {
            if ((PlayerCooldown.ContainsKey(player) && unixTime >= PlayerCooldown[player]) || !PlayerCooldown.ContainsKey(player))
            {
                foreach (Player playerlist in Player.ReadyList)
                {
                    if (// Position X
                        playerlist.Position.x <= player.Position.x + Plugin.Singleton.Config.RuttatoreAbilityRadius
                        &&
                        playerlist.Position.x >= player.Position.x - Plugin.Singleton.Config.RuttatoreAbilityRadius
                        && // Position Y
                        playerlist.Position.y <= player.Position.y + Plugin.Singleton.Config.RuttatoreAbilityRadius
                        &&
                        playerlist.Position.y >= player.Position.y - Plugin.Singleton.Config.RuttatoreAbilityRadius
                        && // Position Z
                        playerlist.Position.z <= player.Position.z + Plugin.Singleton.Config.RuttatoreAbilityRadius
                        &&
                        playerlist.Position.z >= player.Position.z - Plugin.Singleton.Config.RuttatoreAbilityRadius
                        )
                    {
                        if (playerlist != player && !playerlist.IsSCP && playerlist.Role != PlayerRoles.RoleTypeId.Tutorial)
                        {
                            playerlist.EnableEffect<CustomPlayerEffects.Flashed>(intensity: 255, duration: Plugin.Singleton.Config.RuttatoreDurationFlashBang);
                            playerlist.EnableEffect<CustomPlayerEffects.Slowness>(intensity: Plugin.Singleton.Config.RuttatoreIntesitySlowness, duration: Plugin.Singleton.Config.RuttatoreDurationSlowness);
                        }
                    }
                }
                PlayerCooldown[player] = unixTime + Plugin.Singleton.Config.RuttatoreCooldown;
            }
            else
            {
                player.SendHint($"<color=red>Cooldown</color>\nAspetta {PlayerCooldown[player] - unixTime}s prima di\npoter riutilizzare l'abilità", Plugin.Singleton.Config.HintCooldownDuration);
            }
        }
        // - Epic
        // Lo slender
        else if (player.CustomInfo == "Lo slender")
        {
            if ((PlayerCooldown.ContainsKey(player) && unixTime >= PlayerCooldown[player]) || !PlayerCooldown.ContainsKey(player)) {


                foreach (Player playerlist in Player.ReadyList) {
                    if (playerlist != null && !playerlist.IsSCP && playerlist.Role != PlayerRoles.RoleTypeId.Tutorial) {
                        playerlistSorted.Add(playerlist);
                    }
                }
                Player playerSelected = playerlistSorted[rnd.Next(playerlistSorted.Count)];
                player.Position = playerSelected.Position;
                player.HumeShield = Plugin.Singleton.Config.SlenderAbilityShield;
                playerSelected.EnableEffect<CustomPlayerEffects.Blurred>(duration: Plugin.Singleton.Config.SlenderAbilityDuration);
                playerSelected.EnableEffect<CustomPlayerEffects.Slowness>(duration: Plugin.Singleton.Config.SlenderAbilityDuration, intensity: 20);
                PlayerCooldown[player] = unixTime + Plugin.Singleton.Config.SlenderCooldown;
            }
            else
            {
                player.SendHint($"<color=red>Cooldown</color>\nAspetta {PlayerCooldown[player] - unixTime}s prima di\npoter riutilizzare l'abilità", Plugin.Singleton.Config.HintCooldownDuration);
            }
        }
        // - Legendary
        // Femboy
        else if (player.CustomInfo == "Femboy")
        {
            if ((PlayerCooldown.ContainsKey(player) && unixTime >= PlayerCooldown[player]) || !PlayerCooldown.ContainsKey(player))
            {
                foreach (Player playerlist in Player.ReadyList)
                {
                    if (// Position X
                        playerlist.Position.x <= player.Position.x + Plugin.Singleton.Config.FemboyAbilityRadius
                        &&
                        playerlist.Position.x >= player.Position.x - Plugin.Singleton.Config.FemboyAbilityRadius
                        && // Position Y
                        playerlist.Position.y <= player.Position.y + Plugin.Singleton.Config.FemboyAbilityRadius
                        &&
                        playerlist.Position.y >= player.Position.y - Plugin.Singleton.Config.FemboyAbilityRadius
                        && // Position Z
                        playerlist.Position.z <= player.Position.z + Plugin.Singleton.Config.FemboyAbilityRadius
                        &&
                        playerlist.Position.z >= player.Position.z - Plugin.Singleton.Config.FemboyAbilityRadius
                        )
                    {
                        if (!playerlist.IsSCP && playerlist.Role != PlayerRoles.RoleTypeId.Tutorial) 
                        {
                            playerlist.DropItem(playerlist.CurrentItem);
                        }
                        else
                        {
                            playerlist.Heal(100);
                        }
                    }

                }
                PlayerCooldown[player] = unixTime + Plugin.Singleton.Config.RuttatoreCooldown;
            }
            
            else
            {
                player.SendHint($"<color=red>Cooldown</color>\nAspetta {PlayerCooldown[player] - unixTime}s prima di\npoter riutilizzare l'abilità", Plugin.Singleton.Config.HintCooldownDuration);
            }
        }
    }

}

