using CustomBreachScenarios.API;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using MEC;
using PlayerRoles;
using Respawning.Waves;
using System.Collections.Generic;
using System.Linq;

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


    // Server - Scenarios

    public override void OnServerWaitingForPlayers()
    {
        foreach (CoroutineHandle coroutine in BreachAPI.DelayedScpSpawnCoroutines)
        {
            Timing.KillCoroutines(coroutine);
        }

        BreachAPI.DelayedScpSpawnCoroutines.Clear();
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
        LoadedScenarios.Clear();
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