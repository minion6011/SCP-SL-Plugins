namespace PetPlugin;

using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;

public class Plugin : Plugin<Config>
{
    public static Plugin Singleton { get; set; } = null!;
    public override string Name { get; } = "PetPlugin";
    public override string Author { get; } = "Coso.Man";
    public override string Description { get; } = "Made by Coso.Man";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.Medium;
    public override void Enable()
    {
        Singleton = this;
    }
    public override void Disable()
    {
        Singleton = null;
    }

}