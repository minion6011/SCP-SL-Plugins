using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using PlayerRoles;

namespace LoggerDS;


public class EventsHandler : CustomEventsHandler
{
    public override void OnServerRoundStarted()
    {
        RequestManager.SendRequest("Round Started", "", Plugin.Singleton.Config.ColorGreen);
    }
    public override void OnServerRoundEnded(RoundEndedEventArgs ev)
    {
        RequestManager.SendRequest("Round Ended", $"**Leading Team:** `{ev.LeadingTeam.ToString()}`", Plugin.Singleton.Config.ColorRed);
    }


    public override void OnPlayerInteractedElevator(PlayerInteractedElevatorEventArgs ev)
    {
        RequestManager.SendRequest("Elevator Interaction",
            $"**Player Name:** `{ev.Player.DisplayName}` (`{ev.Player.UserId}`)\n" +
            $"**Player Role:** `{ev.Player.Role.GetAbbreviatedRoleName()}` - **Player Shield:** `{ev.Player.HumeShield.ToString()}`\n" +
            $"**Elevator Name:** `{ev.Elevator.Group.ToString()}`", 
            Plugin.Singleton.Config.ColorBlue);
    }

    public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
    {
        RequestManager.SendRequest("Player Joined", 
            $"**Player Name:** `{ev.Player.DisplayName}` (`{ev.Player.UserId}`)",
            Plugin.Singleton.Config.ColorGreen);
    }
    public override void OnPlayerLeft(PlayerLeftEventArgs ev)
    {
        RequestManager.SendRequest("Player Left", 
            $"**Player Name:** `{ev.Player.DisplayName}` (`{ev.Player.UserId}`)",
            Plugin.Singleton.Config.ColorRed);
    }
    public override void OnPlayerDeath(PlayerDeathEventArgs ev)
    {
        RequestManager.SendRequest("Player Death",
            $"**Player Name:** `{ev.Player.DisplayName}` (`{ev.Player.UserId}`)\n" +
            $"**Player Old Role:** `{ev.OldRole.GetAbbreviatedRoleName()}`\n" +
            $"**Reason:** `{ev.DamageHandler.DeathScreenText}`\n" +
            $"**Attacker Name:** `{ev.Attacker?.DisplayName ?? "server"}` (`{ev.Attacker?.UserId ?? "null"}`)\n" +
            $"**Attacker Old Role:** `{ev.Attacker?.Role.GetAbbreviatedRoleName().ToString() ?? "null"}` - **Attacker Shield:** `{ev.Attacker?.HumeShield.ToString() ?? "null"}`",
            Plugin.Singleton.Config.ColorRed);
    }

    public override void OnServerCommandExecuted(CommandExecutedEventArgs ev)
    {
        RequestManager.SendRequest("Server Command Executed",
            $"**Player Name:** `{Player.Get(ev.Sender)?.DisplayName ?? "server"}` (`{Player.Get(ev.Sender)?.UserId ?? "null"}`)\n" +
            $"**Command:** `{ev.CommandName}`",
            Plugin.Singleton.Config.ColorYellow
            );
    }

    public override void OnServerWaveRespawned(WaveRespawnedEventArgs ev)
    {
        string description = $"**Wave Type:** `{ev.Wave.Faction.ToString()}`\n\n## Players:\n";
        foreach (var playerWave in ev.Players) {
            description += $"**Player Name:** `{playerWave.DisplayName}` (`{playerWave.UserId}`) - **Role:** `{playerWave.Role}`\n";
        }
        RequestManager.SendRequest("Wave Respawned", description, Plugin.Singleton.Config.ColorBlue);
    }
    public override void OnPlayerUsedIntercom(PlayerUsedIntercomEventArgs ev) {
        RequestManager.SendRequest("Player Muted", $"**Player Name:** `{ev.Player?.DisplayName ?? "server"}` (`{ev.Player?.UserId ?? "null"}`)", Plugin.Singleton.Config.ColorBlue);
    }

    public override void OnPlayerBanned(PlayerBannedEventArgs ev)
    {
        RequestManager.SendRequest("Player Banned",
            $"**Player Name:** `{ev.Player?.DisplayName ?? "null"}` (`{ev.Player?.UserId ?? "null"}`)\n" +
            $"**Issuer Name:** `{ev.Issuer?.DisplayName ?? "server"}` (`{ev.Issuer?.UserId ?? "null"}`)\n" +
            $"**Reason:** `{ev.Reason ?? "null"}`\n" +
            $"**Duration:** `{ev.Duration.ToString()}`",
            Plugin.Singleton.Config.ColorRed);
    }
    public override void OnPlayerMuted(PlayerMutedEventArgs ev)
    {
        RequestManager.SendRequest("Player Muted",
            $"**Player Name:** `{ev.Player?.DisplayName ?? "null"}` (`{ev.Player?.UserId ?? "null"}`)\n" +
            $"**Issuer Name:** `{ev.Issuer?.DisplayName ?? "server"}` (`{ev.Issuer?.UserId ?? "null"}`)\n" +
            $"**Is Intercom:** `{ev.IsIntercom.ToString() ?? "false"}`\n",
            Plugin.Singleton.Config.ColorRed);
    }
    public override void OnPlayerUnmuted(PlayerUnmutedEventArgs ev)
    {
        RequestManager.SendRequest("Player Unmuted",
            $"**Player Name:** `{ev.Player?.DisplayName ?? "null"}` (`{ev.Player?.UserId ?? "null"}`)\n" +
            $"**Issuer Name:** `{ev.Issuer?.DisplayName ?? "server"}` (`{ev.Issuer?.UserId ?? "null"}`)\n" +
            $"**Is Intercom:** `{ev.IsIntercom.ToString() ?? "false"}`\n",
            Plugin.Singleton.Config.ColorGreen);
    }
    public override void OnPlayerKicked(PlayerKickedEventArgs ev)
    {
        RequestManager.SendRequest("Player Kicked",
            $"**Player Name:** `{ev.Player?.DisplayName ?? "null"}` (`{ev.Player?.UserId ?? "null"}`)\n" +
            $"**Issuer Name:** `{ev.Issuer?.DisplayName ?? "server"}` (`{ev.Issuer?.UserId ?? "null"}`)\n" +
            $"**Reason:** `{ev.Reason ?? "null"}`",
            Plugin.Singleton.Config.ColorRed);
    }
}