using LabApi.Features.Wrappers;
using System.Collections.Generic;


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

}