using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using System.Collections.Generic;
using System.Linq;


namespace TheSpy;

public class SpyManager
{
    public static List<Player> SpyPlayers { get; private set; } = new List<Player>();

    public static void Spawn(Player player)
    {
        if (!SpyPlayers.Contains(player))
        {
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

    public static void EndRoundCheck()
    {
        if (Player.ReadyList.Count() > 1 && SpyPlayers.Count() > 0 && !Round.IsLocked)
        {
            int totalPlayers = 0;
            int totalNTF = 0;
            int totalChaos = 0;
            foreach (Player playerList in Player.ReadyList)
            {
                if (playerList != null && playerList.IsAlive)
                {
                    totalPlayers += 1;
                    if ((playerList.IsNTF && !SpyManager.SpyPlayers.Contains(playerList)) || playerList.Role == PlayerRoles.RoleTypeId.Scientist || playerList.Role == PlayerRoles.RoleTypeId.FacilityGuard || (playerList.IsChaos && SpyManager.SpyPlayers.Contains(playerList)))
                    {
                        totalNTF += 1;
                    }
                    else if ((playerList.IsChaos && !SpyManager.SpyPlayers.Contains(playerList)) || playerList.Role == PlayerRoles.RoleTypeId.ClassD || (playerList.IsNTF && SpyManager.SpyPlayers.Contains(playerList)))
                    {
                        totalChaos += 1;
                    }
                }
            }
            // End Round Check
            if (totalPlayers == totalNTF || totalPlayers == totalChaos)
            {
                Round.End();
            }
        }
    }

}
