using CustomBreachScenarios.API;
using CustomBreachScenarios.API.Objects;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using LightContainmentZoneDecontamination;
using MEC;
using PlayerRoles;
using Respawning.Waves;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static LightContainmentZoneDecontamination.DecontaminationController;

namespace CustomBreachScenarios;


public class EventsHandler : CustomEventsHandler
{
    /// <summary>
    /// Gets or sets current loaded scenario.
    /// </summary>
    public static BreachScenario SelectedScenario { get; set; }

    /// <summary>
    /// Gets currently loaded Scenarios.
    /// </summary>
    public static List<BreachScenario> LoadedScenarios { get; internal set; } = new();


    private bool decontaminationActivated = false;

    // Server - Scenarios

    public override void OnServerWaitingForPlayers()
    {
        foreach (CoroutineHandle coroutine in BreachAPI.DelayedScpSpawnCoroutines)
        {
            Timing.KillCoroutines(coroutine);
        }
        BreachAPI.DelayedScpSpawnCoroutines.Clear();

        foreach (CoroutineHandle coroutine in BreachAPI.DelayedCommandsCoroutines)
        {
            Timing.KillCoroutines(coroutine);
        }
        BreachAPI.DelayedCommandsCoroutines.Clear();

        LoadedScenarios.Clear();

        LoadedScenarios = BreachAPI.GetAllScenarios(Plugin.CustomBreachScenariosPath).ToList();
        SelectedScenario = BreachAPI.DrawScenario(LoadedScenarios);
    }

    public override void OnServerRoundStarted()
    {
        BreachAPI.PlayScenario(SelectedScenario);
    }

    public override void OnServerWaveRespawning(WaveRespawningEventArgs ev)
    {
        if (SelectedScenario is null)
        {
            return;
        }

        switch (ev.Wave.Faction)
        {
            case Faction.FoundationStaff when !SelectedScenario.CustomConditions.CanNtfSpawn:
                if (SelectedScenario.CustomConditions.CanChiSpawn)
                    Respawning.WaveManager.Spawn(new ChaosSpawnWave());
                else
                    ev.IsAllowed = SelectedScenario.CustomConditions.CanChiSpawn;
                break;
            case Faction.FoundationEnemy when !SelectedScenario.CustomConditions.CanChiSpawn:
                if (SelectedScenario.CustomConditions.CanNtfSpawn)
                    Respawning.WaveManager.Spawn(new NtfSpawnWave());
                else
                    ev.IsAllowed = SelectedScenario.CustomConditions.CanNtfSpawn;
                break;
        }
    }

    public override void OnServerRoundEnded(RoundEndedEventArgs ev)
    {
        foreach (CoroutineHandle coroutines in BreachAPI.DelayedScpSpawnCoroutines)
        {
            Timing.KillCoroutines(coroutines);
        }
        BreachAPI.DelayedScpSpawnCoroutines.Clear();

        foreach (CoroutineHandle coroutine in BreachAPI.DelayedCommandsCoroutines)
        {
            Timing.KillCoroutines(coroutine);
        }
        BreachAPI.DelayedCommandsCoroutines.Clear();
        
        LoadedScenarios.Clear();
    }

    public override void OnServerLczDecontaminationAnnounced(LczDecontaminationAnnouncedEventArgs ev)
    {
        if (SelectedScenario.DecontaminationError.Chance >= Random.Range(1, 101) && !decontaminationActivated)
        {
            decontaminationActivated = true;
            
            

            Timing.CallDelayed(SelectedScenario.DecontaminationError.Time - SelectedScenario.DecontaminationError.TimeTolerance, () =>
            {
                Decontamination.Status = DecontaminationStatus.Disabled;
                Decontamination.Offset = 1100;
                Decontamination.ElevatorsText = SelectedScenario.DecontaminationError.ElevatorText;
                foreach (int delay in SelectedScenario.DecontaminationError.Commands.Keys)
                {
                    BreachAPI.DelayedCommandsCoroutines.Add(
                        Timing.RunCoroutine(
                            BreachAPI.DelayCommands(delay, SelectedScenario.DecontaminationError.Commands[delay])
                        )
                    );
                
                }
            });
        }
    }

    // Player - Scenarios
    public override void OnPlayerChangingRole(PlayerChangingRoleEventArgs ev)
    {
        base.OnPlayerChangingRole(ev);
        if (ev.Player is null)
            return;

        if (SelectedScenario is null)
            return;

        if (SelectedScenario.DelayedScpSpawns.Any(x => x.Role == ev.NewRole))
        {
            ev.NewRole = RoleTypeId.ClassD;
        }
    }

    public override void OnPlayerTriggeringTesla(PlayerTriggeringTeslaEventArgs ev)
    {
        if (SelectedScenario.CustomConditions.TeslasDisabled)
        {
            ev.IsAllowed = false;
        }
    }
}