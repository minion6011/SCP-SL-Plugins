namespace AssistenzaStaff;

using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using LabApi.Features.Console;

public class Plugin : Plugin<Config>
{
    public static Plugin Singleton { get; set; } = null!;
    public override string Name { get; } = "AssistenzaStaff";
    public override string Author { get;  } = "Coso.Man";
    public override string Description { get; } = "Made by Coso.Man";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.High;
    public override void Enable()
    {
        Singleton = this;
        Logger.Info($"Plugin {Name} by {Author} loaded successfully!");
    }
    public override void Disable()
    {
        Singleton = null;
    }
}