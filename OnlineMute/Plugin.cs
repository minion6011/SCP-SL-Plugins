namespace OnlineMute;

using HarmonyLib;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Paths;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using MEC;
using System;
using System.Collections.Generic;
using System.IO;

public class Plugin : Plugin<Config>
{
    public static Plugin Singleton { get; set; } = null!;
    public override string Name { get; } = "OnlineMute";
    public override string Author { get; } = "Coso.Man";
    public override string Description { get; } = "Made by Coso.Man";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.Low;
    public EventsHandler Events { get; } = new();

    public string MuteDBPath { get; } = Path.Combine(PathManager.LabApi.ToString(), "configs", "OnlineMute");

    public Dictionary<string, float> PlayersToCheck { get; set; } = new Dictionary<string, float>();
    public CoroutineHandle PlayerCheckCoroutine { get; set; }

    private Harmony _harmony;

    public override void Enable()
    {
        _harmony = new Harmony($"com.onlinemute.patch.{DateTime.Now.Ticks}");
        _harmony.PatchAll();

        Singleton = this;
        CustomHandlersManager.RegisterEventsHandler(Events);

        if (!Directory.Exists(MuteDBPath))
        {
            Directory.CreateDirectory(MuteDBPath);
            DataStorage.Save(new Dictionary<string, float>());
        }

    }
    public override void Disable()
    {
        _harmony.UnpatchAll(_harmony.Id);
        _harmony = null;

        Singleton = null;
        CustomHandlersManager.UnregisterEventsHandler(Events);
    }
}