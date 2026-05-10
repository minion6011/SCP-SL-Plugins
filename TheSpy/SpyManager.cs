using LabApi.Features.Wrappers;
using System.Collections.Generic;
using System.Linq;


namespace TheSpy;

public class SpyManager
{
    public static List<Player> SpyPlayers { get; private set; } = new List<Player> ();

    public static void Spawn(Player player)
    {
        if (!SpyPlayers.Contains(player)) {
            SpyPlayers.Add(player);
            player.SendHint(text: Plugin.Singleton.Config.SpyHint, duration: Plugin.Singleton.Config.SpyHintDuration);
            player.HumeShield = Plugin.Singleton.Config.SpyShield;
        }
    }
    public static void Kill(Player player) 
    {
        if (SpyPlayers.Contains(player))
        {
            SpyPlayers.Remove(player);
        }
    }

    public static bool EndRoundCheck() {
        if (Player.ReadyList.Count() > 1 && SpyPlayers.Count() > 0 && !Round.IsLocked) {
            int totalPlayers = Player.ReadyList.Count();

            // int[4] >> totalNTF, totalChaos, totalFlamingo, totalSCP, 
            int[] classesList = new int[4] {0, 0, 0, 0};

            foreach (Player playerList in Player.ReadyList)
            {
                if (playerList != null && playerList.IsAlive)
                {
                    if (
                        ((playerList.Team == PlayerRoles.Team.FoundationForces && !SpyManager.SpyPlayers.Contains(playerList)) || playerList.Team == PlayerRoles.Team.Scientists)
                        ||
                        (playerList.Team == PlayerRoles.Team.ChaosInsurgency && SpyManager.SpyPlayers.Contains(playerList))
                        )
                        classesList[0] += 1;
                    else if (
                        ((playerList.Team == PlayerRoles.Team.ChaosInsurgency && !SpyManager.SpyPlayers.Contains(playerList)) || playerList.Team == PlayerRoles.Team.ClassD)
                        ||
                        (playerList.Team == PlayerRoles.Team.FoundationForces && SpyManager.SpyPlayers.Contains(playerList))
                    )
                        classesList[1]++;
                    else if (
                        (playerList.Team == PlayerRoles.Team.Flamingos)
                    )
                        classesList[2]++;
                    else if (
                        (playerList.Team == PlayerRoles.Team.SCPs)
                    )
                        classesList[3]++;
                }
            }
            // End Round Check
            foreach ( int value in classesList )
            {
                if (value == totalPlayers)
                {
                    return true;
                }
            }
        }
        return false;
    }

}