using LabApi.Features.Extensions;
using LabApi.Features.Wrappers;
using MEC;
using System.Collections.Generic;
using System;

namespace ScpQuit;

public class ScpQuitManager
{
    public static List<Player> PlayersClaim { get; set; } = new List<Player> ();
    public static bool RequestInProgress { get; private set; } = false;

    private static Random rnd = new Random();
    public static void ScpRequest(Player playerSender)
    {
        foreach (var player in Player.ReadyList)
        {
            if (!player.IsSCP && player != playerSender)
            {
                RequestInProgress = true;
                player.SendBroadcast(message: $"<b><color=red>{playerSender.Role.GetFullName()}</color> <color=yellow>vuole essere sostitutito come SCP\nUsate </color><color=red>.scpclaim</color><color=yellow> per sostituirlo</color></b>", duration: ((ushort)Plugin.Singleton.Config.MaxTimeClaim_Sec), shouldClearPrevious: true);
                Timing.CallDelayed(Plugin.Singleton.Config.MaxTimeClaim_Sec, () => {
                    if (PlayersClaim.Count != 0) {
                        int r = rnd.Next(PlayersClaim.Count);
                        Player playeyClaimed = PlayersClaim[r];

                        // Messagge Player Claimed
                        playeyClaimed.SendConsoleMessage($"Sei stato scelto come sostiuto del {playerSender.Role.GetFullName()}");
                        playeyClaimed.SendBroadcast(message: $"<color=green><b>Sei il nuovo {playerSender.Role.GetFullName()}</b></color>", duration: Plugin.Singleton.Config.TimeBrodcastResult_Sec);

                        playeyClaimed.SetRole(newRole: playerSender.Role);
                        playerSender.SetRole(newRole: PlayerRoles.RoleTypeId.Spectator);

                        // Messagge Player Sender
                        playerSender.SendBroadcast(message: "<color=green><b>Sei stato sostiuto</b></color>", duration: Plugin.Singleton.Config.TimeBrodcastResult_Sec);
                        playerSender.SendConsoleMessage("Sei stato sostituito");


                        // Messagge All
                        foreach (Player player in Player.ReadyList)
                        {
                            if (player != playeyClaimed && player != playerSender)
                            {
                                player.SendBroadcast(message: $"{playeyClaimed.Role.GetFullName()} è stato sostituito", duration: Plugin.Singleton.Config.TimeBrodcastResult_Sec);
                            }
                        }
                        // Reset
                        PlayersClaim = new List<Player>();
                        RequestInProgress = false;
                    }
                    else {
                        playerSender.SendBroadcast(message: "<color=red><b>Richiesta di sostituzione rifiutata</b></color>", duration: Plugin.Singleton.Config.TimeBrodcastResult_Sec);
                        playerSender.SendConsoleMessage("Nessuno ha accettato la tua richiesta di sostituzione", color: "red");
                    }
                });
            }
        }  
    }

}