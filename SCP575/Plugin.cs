namespace SCP_575;

using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using MEC;
using System;
using System.Collections.Generic;

public class Plugin : Plugin<Config>
{
    public static Plugin Singleton { get; set; } = null!;
    public override string Name { get; } = "SCP-575";
    public override string Author { get; } = "Coso.Man";
    public override string Description { get; } = "Made by Coso.Man";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.Medium;
    public EventsHandler Events { get; } = new();


    public System.Random rnd = new System.Random();

    public override void Enable()
    {
        Singleton = this;
        CustomHandlersManager.RegisterEventsHandler(Events);
    }
    public override void Disable()
    {
        Singleton = null;
        CustomHandlersManager.UnregisterEventsHandler(Events);
    }


    public void SpawnSCP575()
    {
        List<Player> players = new List<Player>();
        foreach (var player in Player.ReadyList)
        {
            if (player.IsAlive && player.IsHuman && (Plugin.Singleton.Config.SurfaceEnabled || player.Zone != MapGeneration.FacilityZone.Surface))
            {
                players.Add(player);
            }
        }
        if (players.Count == 0) return;
        int r = rnd.Next(players.Count);
        Player playerTarget = players[r];
        Timing.RunCoroutine(LightOffDamage(playerTarget.Room, Plugin.Singleton.Config.duration575));
    }
    private IEnumerator<float> LightOffDamage(Room roomTarget, float duration)
    {
        // Warning
        foreach (var player in roomTarget.Players)
        {
            if (player.IsAlive && !player.IsSCP)
            {
                player.SendHint(Plugin.Singleton.Config.warnHint575.Replace("$value", Plugin.Singleton.Config.hintDuration.ToString()), Plugin.Singleton.Config.hintDuration);
            }
        }
        yield return Timing.WaitForSeconds(Plugin.Singleton.Config.spawnDelay);
        // Lights Off
        roomTarget.LightController.FlickerLights(duration);
        // Damage
        System.DateTime endTime = System.DateTime.Now.AddSeconds(duration);
        while (System.DateTime.Now < endTime)
        {
            foreach (var player in roomTarget.Players)
            {
                if (player.IsAlive && !player.IsSCP)
                {
                    if (!(player.CurrentItem.Type == ItemType.Flashlight || player.CurrentItem.Type == ItemType.Lantern))
                        player.Damage(Plugin.Singleton.Config.damagePerSecond, Plugin.Singleton.Config.killMsg575);
                }
            }
            yield return Timing.WaitForSeconds(1f);
        }
    }
}