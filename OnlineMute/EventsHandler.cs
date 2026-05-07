using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMute;


public class EventsHandler : CustomEventsHandler
{
    // Badge setup
    public void StartPlugin()
    {
        if (Plugin.Singleton.PlayerCheckCoroutine.IsValid)
            Timing.KillCoroutines(Plugin.Singleton.PlayerCheckCoroutine);

        Plugin.Singleton.PlayersToCheck = DataStorage.Load();
        Plugin.Singleton.PlayerCheckCoroutine = Timing.RunCoroutine(PlayersLoopCheck(Plugin.Singleton.Config.CheckTollerance));
    }

    public void TimeCheck(float reducedTime)
    {
        if (Plugin.Singleton.PlayersToCheck.Count == 0) return;

        foreach (string playerId in Plugin.Singleton.PlayersToCheck.Keys.ToList())
        {
            Player player = Player.Get(playerId);

            if (Player.ReadyList.Contains(player) && player != null)
            {
                if (Round.IsRoundStarted || Plugin.Singleton.Config.IsLobbyValid)
                    Plugin.Singleton.PlayersToCheck[playerId] -= reducedTime;
                if (Plugin.Singleton.PlayersToCheck[playerId] > 0)
                {
                    if (!player.IsMuted)
                    {
                        player.Mute(true);
                    }
                }
                else
                {
                    player.Unmute(false);
                    Plugin.Singleton.PlayersToCheck.Remove(playerId);
                    player.SendHint(Plugin.Singleton.Config.UnmuteHint, Plugin.Singleton.Config.DurationHint);
                }
            }
        }
    }

    public IEnumerator<float> PlayersLoopCheck(float time)
    {
        while (true)
        {
            yield return Timing.WaitForSeconds(time);
            try
            {
                TimeCheck(time);
                DataStorage.Save(Plugin.Singleton.PlayersToCheck);
            }
            catch (Exception e)
            {
                Logger.Error($"Errore nel loop di OnlineMute: {e}");
            }
        }
    }

    public override void OnServerWaitingForPlayers()
    {
        StartPlugin();
    }

    public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
    {
        double timeleft;
        if (Plugin.Singleton.PlayersToCheck.Keys.Contains(ev.Player.UserId)) {
            timeleft = Math.Ceiling(Plugin.Singleton.PlayersToCheck[ev.Player.UserId] / 60);
            ev.Player.SendHint(Plugin.Singleton.Config.MuteHint.Replace("$value", timeleft.ToString() ), Plugin.Singleton.Config.DurationHint);
        }
    }
    public override void OnServerRoundStarted()
    {
        double timeleft;
        foreach (Player player in Player.ReadyList)
        {
            if (Plugin.Singleton.PlayersToCheck.Keys.Contains(player.UserId))
            {
                timeleft = Math.Ceiling(Plugin.Singleton.PlayersToCheck[player.UserId] / 60);
                player.SendHint(Plugin.Singleton.Config.MuteHint.Replace("$value", timeleft.ToString()), Plugin.Singleton.Config.DurationHint);
            }
        }
    }
}