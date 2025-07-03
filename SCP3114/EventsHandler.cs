using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace Heavy3114;


public class EventsHandler : CustomEventsHandler
{
    static System.Random rnd = new System.Random();
    public override void OnServerRoundStarted()
    {
        if (Server.PlayerCount >= Plugin.Singleton.Config.MinPlayer)
        {
            List<Player> players = new List<Player>();
            foreach (var player in Player.ReadyList)
            {
                if (player != null && player.IsSCP)
                {
                    players.Add(player);
                }
            }
            if (players.Count > 0)
            {
                int r = rnd.Next(players.Count);
                Player SelectedPlayer = players[r];
                Door Scp127Door = Door.Get("HCZ_127_LAB");
                SelectedPlayer.SetRole(PlayerRoles.RoleTypeId.Scp3114);
                SelectedPlayer.SendHint(Plugin.Singleton.Config.SCP3114Hint, Plugin.Singleton.Config.HintDuration);
                Scp127Door.IsLocked = true;
                Scp127Door.IsOpened = false;
                SelectedPlayer.Position = Scp127Door.Position + new Vector3(0, 0.3f, 0);
                Timing.CallDelayed(Plugin.Singleton.Config.DoorOpenTime, () => { Scp127Door.IsLocked = false; Scp127Door.IsOpened = true; }); 
            }
        }
    }
}