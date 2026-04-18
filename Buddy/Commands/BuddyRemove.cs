using CommandSystem;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Buddy.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class BuddyRemoveCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = "";
        Player player = Player.Get(sender);
        response = HandleUnBuddyCommand(player);
        return true;
    }

    private string HandleUnBuddyCommand(Player p)
    {
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
        return Plugin.Singleton.Config.SuccessUnMsg;
    }

    public string Command { get; } = "bremove";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Ti rimuove dall'essere il Buddy di qualcuno.";
}