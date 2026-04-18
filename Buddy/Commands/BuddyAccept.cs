using CommandSystem;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Buddy.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class BuddyAcceptCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = "";
        Player player = Player.Get(sender);
        response = HandleBuddyAcceptCommand(player, arguments.ToArray());
        return true;
    }


    private string HandleBuddyAcceptCommand(Player p, string[] args)
    {
        //checks
        if (!Plugin.BuddiesRequests.ContainsKey(p.UserId))
        {
            return Plugin.Singleton.Config.NoRequestMsg;
        }

        //set the buddy
        Player buddy = null;

        if (!Plugin.BuddiesRequests.TryGetValue(p.UserId, out List<Player> buddies) || buddies == null) 
            return Plugin.Singleton.Config.ErrorMsg;
        if (args.Length != 1) buddy = buddies.Last();
        else
        {
            string lower = args[0].ToLower();
            foreach (Player player in buddies)
            {
                if (player == null) continue;
                if (player.Nickname.ToLower().Contains(lower) && player.UserId != p.UserId)
                {
                    buddy = player;
                    break;
                }
            }
        }

        if (buddy == null || (buddy != null && Plugin.Buddies.ContainsKey(buddy.UserId)))
        {
            Plugin.Buddies.Remove(p.UserId);
            Plugin.Singleton.RemovePerson(p.UserId);
            return Plugin.Singleton.Config.ErrorMsg;
        }
        try
        {
            if (Plugin.Buddies.ContainsKey(p.UserId))
            {
                Plugin.Buddies.Remove(p.UserId);
                Plugin.Singleton.RemovePerson(p.UserId);
            }
        }
        catch (ArgumentNullException e)
        {
            return Plugin.Singleton.Config.ErrorMsg;
        }

        Plugin.Buddies[p.UserId] = buddy.UserId;
        Plugin.Buddies[buddy.UserId] = p.UserId;
        Plugin.BuddiesRequests.Remove(p.UserId);
        buddy.SendConsoleMessage(Plugin.Singleton.Config.RequestAcceptHint.Replace("$name", p.Nickname), "yellow");
        if (Plugin.Singleton.Config.SendHints)
            buddy.SendHint(Plugin.Singleton.Config.RequestAcceptHint.Replace("$name", p.Nickname), Plugin.Singleton.Config.HintDuration);
        return Plugin.Singleton.Config.SuccessMsg.Replace("$name", buddy.Nickname);
    }


    public string Command { get; } = "baccept";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Accetta la richiesta di diventare il Buddy di qualcuno.";
}