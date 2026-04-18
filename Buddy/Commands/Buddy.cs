using CommandSystem;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Buddy.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class BuddyCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = "";
        string[] args = arguments.ToArray();
        if (args.Length == 0) {
            response = Plugin.Singleton.Config.ErrNoPlayerMsg;
        }
        else {
            if (Server.PlayerCount < Plugin.Singleton.Config.MinPlayers) {
                response = Plugin.Singleton.Config.ErrMinPlayerMsg.Replace("$min", Plugin.Singleton.Config.MinPlayers.ToString());
            }
            else {
                response = HandleBuddyCommand(Player.Get(sender), args);
            }
        }
        return true;
    }

    private string HandleBuddyCommand(Player playerSender, string[] args) {
        Player playerBuddy = null;

        string lower = args[0].ToLower();
        foreach (Player player in Player.List)
        {
            if (player == null)
                continue;
            if (player.Nickname.ToLower().Contains(lower) && player.UserId != playerSender.UserId)
            {
                playerBuddy = player;
                break;
            }
        }

        if (playerBuddy == null)
            return Plugin.Singleton.Config.ErrNoPlayerFoundMsg;

        if (Plugin.BuddiesRequests.ContainsKey(playerSender.UserId) && Plugin.BuddiesRequests.TryGetValue(playerSender.UserId, out List<Player> buddies) && buddies.Where((player) => player.UserId == playerBuddy.UserId).Any() && !Plugin.Buddies.ContainsKey(playerBuddy.UserId))
        {
            Plugin.Buddies[playerSender.UserId] = playerBuddy.UserId;
            Plugin.Buddies[playerBuddy.UserId] = playerSender.UserId;
            Plugin.BuddiesRequests.Remove(playerSender.UserId);
            playerBuddy.SendConsoleMessage(Plugin.Singleton.Config.RequestAcceptMsg.Replace("$name", playerBuddy.Nickname), Plugin.Singleton.Config.MsgColor);
            if (Plugin.Singleton.Config.SendHints) 
                playerBuddy.SendHint(Plugin.Singleton.Config.RequestAcceptHint.Replace("$name", playerBuddy.Nickname), Plugin.Singleton.Config.HintDuration);
            return Plugin.Singleton.Config.RequestAcceptMsg.Replace("$name", playerBuddy.Nickname);
        }

        if (!Plugin.BuddiesRequests.ContainsKey(playerBuddy.UserId)) {
            Plugin.BuddiesRequests[playerBuddy.UserId] = new List<Player>();
        }

        Plugin.BuddiesRequests[playerBuddy.UserId].Add(playerSender);
        playerBuddy.SendConsoleMessage(Plugin.Singleton.Config.RequestSentMsg.Replace("$name", playerSender.Nickname), Plugin.Singleton.Config.MsgColor);
        if (Plugin.Singleton.Config.SendHints && !Round.IsRoundStarted)
            playerBuddy.SendHint(Plugin.Singleton.Config.RequestSentHint.Replace("$name", playerSender.Nickname), Plugin.Singleton.Config.HintDuration);
        return Plugin.Singleton.Config.RequestSent.Replace("$name", playerBuddy.Nickname);       
    }

    public string Command { get; } = "buddy";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Chiede a un utente di diventare il tuo Buddy.";
}