using CommandSystem;
using LabApi.Features.Wrappers;
using System;

namespace OnlineMute.Commands;


[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class OnlineMuteCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count == 2 && float.TryParse(arguments[1], out float time))
        {
            string userId = arguments[0];

            if (!Plugin.Singleton.PlayersToCheck.ContainsKey(userId))
            {

                time *= Plugin.Singleton.Config.CommandMultiplier; // float time != 0 
                Player player = null;


                player = Player.Get(userId);

                if (player != null)
                {
                    Plugin.Singleton.PlayersToCheck.Add(userId, time);
                    DataStorage.Save(Plugin.Singleton.PlayersToCheck);
                    player.Mute(true);
                    player.SendHint(Plugin.Singleton.Config.MuteHint.Replace("$value", (time/60).ToString()));

                    response = $"Ho mutato il player {player.DisplayName} per {time}±{Plugin.Singleton.Config.CheckTollerance}s";
                }
                else
                {
                    response = "Impossibile trovare il player";
                }
            }
            else {
                response = "Il player è già mutato";
            }
        }
        else {
            response = "Argomenti del comando invalidi, Esempio: onlineMute <playerId oppure steamid@steam> <time>";
        }
        return true;
    }

    public string Command { get; } = "onlinemute";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Muta temporaneamento un giocatore (il tempo scala quando è nel server)";
}