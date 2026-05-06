using CommandSystem;
using LabApi.Features.Wrappers;
using System;

namespace OnlineMute.Commands;


[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class OnlineUnmuteCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count == 1)
        {
            string userId = arguments[0];
            if (Plugin.Singleton.PlayersToCheck.ContainsKey(userId))
            {
                Player player = null;


                player = Player.Get(userId);

                if (player != null && Plugin.Singleton.PlayersToCheck.ContainsKey(userId))
                {
                    Plugin.Singleton.PlayersToCheck.Remove(userId);
                    DataStorage.Save(Plugin.Singleton.PlayersToCheck);
                    player.Unmute(false);
                    player.SendHint(Plugin.Singleton.Config.UnmuteHint, Plugin.Singleton.Config.DurationHint);

                    response = $"Ho smutato il player {player.DisplayName}";
                }
                else
                {
                    response = "Impossibile trovare il giocatore";
                }
            }
            else
            {
                response = "Il player non è mutato";
            }
        }
        else {
            response = "Argomenti del comando invalidi, Esempio: onlineUnmute <steamid@steam>";
        }
        return true;
    }

    public string Command { get; } = "onlineunmute";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Smuta un giocatore mutato temporaneamente dal comando (onlineMute)";
}