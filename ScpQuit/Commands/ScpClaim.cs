using CommandSystem;
using LabApi.Features.Wrappers;
using System;

namespace ScpQuit.Commands;


[CommandHandler(typeof(ClientCommandHandler))]
public class ScpClaimCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (ScpQuitManager.RequestInProgress) {
            if (!Player.Get(sender).IsSCP) { 
                ScpQuitManager.PlayersClaim.Add(Player.Get(sender));
                response = "Hai richiesto di essere sostituito da SCP, attendi l'esito della richiesta.";
                return true;
            }
            response = "Non puoi diventare un sostituto di un altro SCP se sei già un SCP";
            return true;
        }
        response = "Non c'è nessuna richiesta in corso";
        return true;
    }

    public string Command { get; } = "scpclaim";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Diventa il sostituto di un SCP vuole essere sostiutito";
}
