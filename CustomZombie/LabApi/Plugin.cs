namespace ZombiCustom;

using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using System;
using UserSettings.ServerSpecific;
using ZombiCustom.SSSetting;

public class Plugin : Plugin<Config>
{
    public static Plugin Singleton { get; set; } = null!;
    public override string Name { get; } = "ZombiCustom";
    public override string Author { get; } = "Coso.Man";
    public override string Description { get; } = "Made by Coso.Man";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.Medium;
    public EventsHandler Events { get; } = new();
    public override void Enable()
    {
        Singleton = this;
        CustomHandlersManager.RegisterEventsHandler(Events);
        // Enable SS Settings
        CustomSettingsBase[] CustomSettings =
        {
            new ZombiCustom.SSSetting.AbilityKey(),
        };
        foreach (var customSettings in CustomSettings)
        {
            customSettings.Activate();
        }
        // ServerSpecificSettingsSync.SendToAll();

    }
    public override void Disable()
    {
        Singleton = null;
        CustomHandlersManager.UnregisterEventsHandler(Events);
    }

}