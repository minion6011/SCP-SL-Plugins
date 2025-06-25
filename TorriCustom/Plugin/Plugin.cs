namespace TorreCustom;

using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using MEC;
using System;

public class Plugin : Plugin<Config>
{
    public static Plugin Singleton { get; set; } = null!;
    public override string Name { get; } = "TorriCustom";
    public override string Author { get; } = "Coso.Man";
    public override string Description { get; } = "Made by Coso.Man";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.Medium;
    public override void Enable()
    {
        Singleton = this;
        Timing.RunCoroutine(TorreCustomTp.TeleportCoroutine(), "TpPositionCoroutine");

    }
    public override void Disable()
    {
        Singleton = null;
    }

}
