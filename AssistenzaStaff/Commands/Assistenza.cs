using CommandSystem;
using System;
using LabApi.Features.Wrappers;

namespace AssistenzaStaff.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class AssistenzaCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        bool ThereStaff = false;
        if (!Player.Get(sender).RemoteAdminAccess)
        {
            foreach (var player in Player.ReadyList)
            {
                if (player.RemoteAdminAccess)
                {
                    player.SendBroadcast(message: $"<b><color=red>Il giocatore {sender.LogName} ha richiesto assistenza dello staff.</b></color>", duration: Plugin.Singleton.Config.BroadcastDuration);
                    ThereStaff = true;
                }
            }
            if (!ThereStaff)
            {
                response = "Non ci sono membri dello staff online al momento.";
                return true;
            }
            response = "Richiesta Staff effettuata";
            return true;
        }
        response = "Non puoi fare richieste staff essendo tu uno staff";
        return true;
    }

    public string Command { get; } = "assistenza";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Chiama lo staff per assistenza.";
}
