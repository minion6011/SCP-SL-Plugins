using System;
using System.Collections.Generic;
using CustomBreachScenarios.API.Objects;
using LabApi.Features.Enums;


namespace CustomBreachScenarios.API
{
    [Serializable]
    public class BreachScenario
    {
        public BreachScenario()
        {
        }

        public BreachScenario(string name) => Name = name;

        public string Name { get; internal set; } = "Default";

        public int Chance { get; internal set; }


        public List<string> Commands { get; internal set; } = new List<string>();

        public AutoNukeObject AutoNuke { get; internal set; } = new AutoNukeObject();

        public CustomConditionsObject CustomConditions { get; internal set; } = new CustomConditionsObject();

        public List<TimedCassieObject> Cassies { get; internal set; } = new List<TimedCassieObject>();

        public List<DelayedScpSpawnObject> DelayedScpSpawns { get; internal set; } = new List<DelayedScpSpawnObject>();

        public List<DoorLockdownObject> DoorLockdowns { get; internal set; } = new List<DoorLockdownObject>();

        public List<BlackoutObject> Blackouts { get; internal set; } = new List<BlackoutObject>();

        public List<ZoneColorObject> ZoneColors { get; internal set; } = new List<ZoneColorObject>();

        public Dictionary<DoorName, int> OpenedDoors { get; internal set; } = new Dictionary<DoorName, int>();

        public DecontaminationError DecontaminationError { get; internal set; } = new DecontaminationError();
    }
}
