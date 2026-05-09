namespace CustomBreachScenarios;

using CustomBreachScenarios.API;
using CustomBreachScenarios.API.Objects;
using Interactables.Interobjects.DoorUtils;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Features.Enums;
using LabApi.Loader.Features.Paths;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using LabApi.Loader.Features.Yaml;
using MapGeneration;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Plugin : Plugin<Config>
{
    public static Plugin Singleton { get; set; } = null!;
    public override string Name { get; } = "CustomBreachScenarios";
    public override string Author { get; } = "Coso.Man";
    public override string Description { get; } = "Adaptaion of CustomBreachScenarios by Ceglaa";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public override LoadPriority Priority { get; } = LoadPriority.Highest;
    public EventsHandler Events { get; } = new();

    public static bool IsActive { get; set; } = true; // Used in case of an error with the yaml
    // Vars
    public static string CustomBreachScenariosPath { get; } = Path.Combine(PathManager.LabApi.ToString(), "configs", "CustomBreachScenarios"); // Win: C:\Users\User\AppData\Roaming\SCP Secret Laboratory\LabAPI\CustomBreachScenarios

    public override void Enable()
    {
        Singleton = this;
        CustomHandlersManager.RegisterEventsHandler(Events);

        if (!Directory.Exists(CustomBreachScenariosPath))
        {
            Directory.CreateDirectory(CustomBreachScenariosPath);
        }
        CreateExampleScenario();
    }
    public override void Disable()
    {
        Singleton = null;
        CustomHandlersManager.UnregisterEventsHandler(Events);
    }

    private static void CreateExampleScenario()
    {
        if (Directory.EnumerateFiles(CustomBreachScenariosPath).Any())
            return;

        BreachScenario example = new("example")
        {
            Chance = 0,
            Name = "example",
            Cassies = new List<TimedCassieObject>
            {
                new()
                {
                    Delay = 20,
                    IsNoisy = true,
                    Announcement = "test",
                    Subtitles = "test in italiano",
                },
            },
            ZoneColors = new List<ZoneColorObject>()
            {
                new()
                {
                    ZoneType = FacilityZone.LightContainment,
                    Delay = 10,
                    Time = 70,
                    R = 1,
                    G = 0,
                    B = 0,
                    A = 0,
                },
            },
            CustomConditions = new CustomConditionsObject(),
            AutoNuke = new AutoNukeObject
            {
                Chance = 100,
                Delay = 1800,
            },
            DelayedScpSpawns = new List<DelayedScpSpawnObject>
            {
                new()
                {
                    Delay = 120,
                    Role = RoleTypeId.Scp096,
                    Room = RoomName.Hcz096,
                },
            },
            OpenedDoors = new Dictionary<DoorName, int>
            {
                { DoorName.Hcz096, 50 },
            },
            DoorLockdowns = new List<DoorLockdownObject>
            {
                new()
                {
                    Chance = 50,
                    DoorLockType = DoorLockReason.AdminCommand,
                    DoorType = DoorName.EzGateA,
                    Time = 120,
                },
            },
            Blackouts = new List<BlackoutObject>
            {
                new()
                {
                    Delay = 100,
                    Time = 100,
                    Zones = new List<FacilityZone>
                    {
                        FacilityZone.Entrance,
                        FacilityZone.LightContainment,
                    },
                },
            },
            DecontaminationError = new DecontaminationError()
        };

        string path = Path.Combine(CustomBreachScenariosPath, $"{example.Name}.yml");
        File.WriteAllText(path, YamlConfigParser.Serializer.Serialize(example));

        Logger.Info("Creating example scenario...");
    }

}