using CommandSystem;
using LabApi.Features.Wrappers;
using System;
namespace ScpQuit.Commands;


[CommandHandler(typeof(ClientCommandHandler))]
public class ScpQuitCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (Round.IsRoundStarted && Round.Duration.TotalMinutes < Plugin.Singleton.Config.MaxTimeReq_Min)
        {
            if (Player.Get(sender).IsSCP)
            {
                if (ScpQuitManager.RequestInProgress)
                {
                    response = "C'è già una richiesta in corso, attendi che finisca";
                    return true;
                }
                else { 
                    ScpQuitManager.ScpRequest(playerSender: Player.Get(sender));
                    response = "Richiesta inviata saprai l'esito tra 20 secondi";
                    return true;
                }
            }
            else {
                response = "Devi essere SCP per eseguire questo comando";
                return true;
            }
        }
        response = $"Non puoi fare più la richiesta, il tempo massimo per fare la richiesta è {Plugin.Singleton.Config.MaxTimeReq_Min} min. dall'inizio del round";
        return true;
    }

    public string Command { get; } = "scpquit";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Chiedi di essere sostituito come SCP";
}