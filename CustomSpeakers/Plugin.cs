namespace CustomSpeakers;

using CustomSpeakers.Objects;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using System;
using System.Linq;

public class SpeakerPlugin : Plugin
{
    public static SpeakerPlugin Singleton { get; set; } = null!;
    public override string Name { get; } = "CustomSpeakers";
    public override string Author { get; } = "Coso.Man";
    public override string Description { get; } = "Made by Coso.Man";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.Low;
    public EventsHandler Events { get; } = new();

    public SpeakersConfig SpeakersConfig;
    private bool _hasIncorrectSettings = false;
    public override void Enable()
    {
        if (_hasIncorrectSettings)
        {
            LabApi.Features.Console.Logger.Error("Detected incorrect settings, not loading");
            return;
        }
        // Loads Clips
        foreach (SpeakerObject value in SpeakersConfig.speakers)
        {
            if (!AudioClipStorage.LoadClip(value.clipPath, value.name)) {
                LabApi.Features.Console.Logger.Error($"Error loading clip: '{value.name}'");
                return;
            }
        }
        Singleton = this;
        CustomHandlersManager.RegisterEventsHandler(Events);
    }

    public override void LoadConfigs()
    {
        base.LoadConfigs();
        _hasIncorrectSettings = !this.TryLoadConfig("speakers.yml", out SpeakersConfig);
    }

    public override void Disable()
    {
        Singleton = null;
        CustomHandlersManager.UnregisterEventsHandler(Events);
    }
}