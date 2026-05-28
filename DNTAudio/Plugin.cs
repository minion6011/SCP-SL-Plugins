namespace DNTAudio;

using DNTAudio.SSSetting;
using HarmonyLib;
using LabApi.Features;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using System;
using System.Collections.Generic;

public class Plugin : Plugin<Config>
{
    public static Plugin Singleton { get; set; } = null!;
    public override string Name { get; } = "DNTAudio";
    public override string Author { get; } = "Coso.Man";
    public override string Description { get; } = "Made by Coso.Man";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.Medium;

    public static List<Player> PlayerList { get; set; }

    private Harmony _harmony;

    public override void Enable()
    {
        Singleton = this;

        // Enable SS Settings
        CustomSettingsBase[] CustomSettings =
        {
            new AudioCustomSettings(),
        };
        foreach (var customSettings in CustomSettings)
        {
            customSettings.Activate();
        }

        _harmony = new Harmony($"com.onlinemute.patch.{DateTime.Now.Ticks}");
        _harmony.PatchAll();
    }
    public override void Disable()
    {
        Singleton = null;

        _harmony.UnpatchAll(_harmony.Id);
        _harmony = null;
    }
}