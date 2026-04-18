namespace Buddy;

using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

public class Plugin : Plugin<Config>
{
    public static Plugin Singleton { get; set; } = null!;
    public override string Name { get; } = "Buddy";
    public override string Author { get; } = "Coso.Man";
    public override string Description { get; } = "Adaptaion of Buddy by PintTheDragon";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.Low;
    public EventsHandler Events { get; } = new();

    // UserId, [SenderPlayer, BuddyPlayer]
    public static Dictionary<string, List<Player>> BuddiesRequests = new Dictionary<string, List<Player>>();
    // PlayerUserId, BuddyUserId
    public static Dictionary<string, string> Buddies = new Dictionary<string, string>();


    public void RemovePerson(string userID)
    {
        try
        {
            foreach (var item in Buddies.Where(x => x.Value == userID).ToList())
            {
                try
                {
                    Buddies.Remove(item.Key);
                }
                catch (ArgumentException) { }
            }
        }
        catch (ArgumentException) { }
    }

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
}