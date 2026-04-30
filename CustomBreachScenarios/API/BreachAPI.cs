using CustomBreachScenarios.API.Objects;
using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Enums;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Yaml;
using MEC;
using PlayerRoles;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using static LightContainmentZoneDecontamination.DecontaminationController;

namespace CustomBreachScenarios.API
{
    public static class BreachAPI
    {
        public static List<CoroutineHandle> DelayedScpSpawnCoroutines { get; set; } = new List<CoroutineHandle>();
        public static List<CoroutineHandle> DelayedCommandsCoroutines { get; set; } = new List<CoroutineHandle>();

        public static BreachScenario DrawScenario(IEnumerable<BreachScenario> inputList)
        {
            int randomValue = Random.Range(1, 101); // UnityEngine.Random
            return inputList.OrderBy(x => x.Chance).FirstOrDefault(x => x.Chance >= randomValue);
        }

        public static IEnumerable<BreachScenario> GetAllScenarios(string directoryPath)
        {
            return Directory.EnumerateFiles(directoryPath)
                .Select(file => YamlConfigParser.Deserializer.Deserialize<BreachScenario>(File.ReadAllText(file, Encoding.UTF8)))
                .ToList();
        }

        public static void PlayScenario(BreachScenario scenario)
        {
            if (scenario is null)
            {
                return;
            }

            if (scenario.AutoNuke.Chance >= Random.Range(1, 101))
            {
                Timing.CallDelayed(scenario.AutoNuke.Delay, () => Warhead.Start());
            }

            foreach (TimedCassieObject timedCassieObject in scenario.Cassies)
            {
                Timing.CallDelayed(timedCassieObject.Delay, () => Announcer.Message(timedCassieObject.Announcement, timedCassieObject.Subtitles, timedCassieObject.IsNoisy));
            }

            foreach (ZoneColorObject zoneColor in scenario.ZoneColors)
            {
                foreach (Room room in Room.List)
                {
                    if (zoneColor.ZoneType == room.Zone)
                    {
                        Timing.CallDelayed(zoneColor.Delay, () => ChangeRoomColorInternal(room, zoneColor));
                    }
                }
            }

            foreach (DelayedScpSpawnObject spawnObject in scenario.DelayedScpSpawns)
            {
                Timing.CallDelayed(spawnObject.Delay, () =>
                {
                    DelayedScpSpawnCoroutines.Add(
                        Timing.CallDelayed(spawnObject.Delay, () => {
                            Timing.RunCoroutine(DelaySpawnScp(spawnObject));
                        })
                    );
                });
            }

            foreach (DoorLockdownObject doorLockdownObject in scenario.DoorLockdowns)
            {
                ProcessTimedLockdown(doorLockdownObject);
            }

            foreach (BlackoutObject blackoutObject in scenario.Blackouts)
            {
                if (blackoutObject.Chance >= Random.Range(1, 101))
                {
                    Timing.CallDelayed(blackoutObject.Delay, () => Map.TurnOffLights(blackoutObject.Time, blackoutObject.Zones));
                }
            }

            foreach (string command in scenario.Commands)
            {
                Server.RunCommand(command);
            }

            foreach (Door door in Door.List) // Testing needed
            {
                foreach (DoorName selDoor in scenario.OpenedDoors.Keys)
                {
                    if (door.DoorName == selDoor)
                    {
                        if (scenario.OpenedDoors[selDoor] > Random.Range(1, 101))
                        {
                            door.IsOpened = true;
                        }
                    }
                }
            }

            if (scenario.CustomConditions.DecontaminationDisabled) {
                Decontamination.Status = DecontaminationStatus.Disabled;
            }
        }


        public static IEnumerator<float> DelayCommands(int delay, List<string> commands)
        {
            yield return Timing.WaitForSeconds(delay);
            foreach (string command in commands) {
                Server.RunCommand(command);
            }
        }


        public static IEnumerator<float> DelaySpawnScp(DelayedScpSpawnObject spawnObject)
        {
            while (Round.IsRoundStarted)
            {
                yield return Timing.WaitUntilTrue(() => Player.List.Any(x => x.Role == RoleTypeId.Spectator));
                List<Player> spectators = new List<Player>();
                foreach (var player in Player.ReadyList) if (player.Role == RoleTypeId.Spectator) spectators.Add(player);


                if (spectators.Count > 0)
                {
                    spectators[Random.Range(0, spectators.Count)].SetRole(spawnObject.Role); // Check parameters --> spawnObject.Role
                    yield break;
                }

                yield return Timing.WaitForSeconds(Plugin.Singleton.Config.DelayedSpawnInterval);
            }
        }

        public static void ProcessTimedLockdown(DoorLockdownObject doorLockdownObject)
        {
            List<Door> doors = Door.List.Where(x => x.DoorName == doorLockdownObject.DoorType).ToList(); // Testing needed

            foreach (Door door in doors)
            {
                if (doorLockdownObject.Chance < Random.Range(1, 101))
                    continue;
                DoorLockReason customReason = doorLockdownObject.DoorLockType; // Testing needed
                door.Lock(customReason, true);
                if (doorLockdownObject.Time > 0)
                {
                    Timing.CallDelayed(doorLockdownObject.Time, () => door.IsLocked = false); // Testing needed
                }
            }
        }

        public static void ChangeRoomColorInternal(Room room, ZoneColorObject zoneColorObject)
        {
            room.LightController.OverrideLightsColor = Color.clear;
            Color newcolor = new Color(zoneColorObject.R, zoneColorObject.G, zoneColorObject.B, zoneColorObject.A);
            room.LightController.OverrideLightsColor = newcolor;

            if (zoneColorObject.Time > 0)
            {
                Timing.CallDelayed(zoneColorObject.Time, () => { room.LightController.OverrideLightsColor = Color.clear; });
            }
        }
    }
}
