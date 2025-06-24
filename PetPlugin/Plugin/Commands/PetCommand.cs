using CommandSystem;
using LabApi.Features.Wrappers;
using System;
using System.Linq;
using System.Collections.Generic;
namespace PetPlugin.Commands;


[CommandHandler(typeof(ClientCommandHandler))]
public class PetCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count != 0)
        {
            // Add Start
            if (arguments.ElementAt(0).Contains("add"))
            {
                string PetNames = "";
                List<string> PetList = new List<string> { };

                foreach (string role in Plugin.Singleton.Config.PetLists.Keys)
                {
                    foreach (string id in Plugin.Singleton.Config.UserList[role])
                    {
                        if (id == Player.Get(sender).UserId)
                        {
                            foreach (var pet in Plugin.Singleton.Config.PetLists[role.ToString()])
                            {
                                PetNames = $"{PetNames}'{pet}', ";
                                PetList.Add(pet);
                            }
                        }
                    }
                }
                if (arguments.Count >= 2 && PetList.Contains(arguments.ElementAt(1)))
                {
                    PetManager.SpawnPet(Player.Get(sender), arguments.ElementAt(1));
                    response = "Pet Spawnato";
                    return true;
                }
                else
                {
                    if (PetNames == "")
                    {
                        response = $"Non possiedi pet";
                        return true;
                    }
                    else
                    {
                        response = $"Devi inserire il nome del pet\nComando: `.pet add <nome-pet>`\nLista dei pet: {PetNames}";
                        return true;
                    }
                }
            }
            // Remove Start
            else if (arguments.ElementAt(0).Contains("remove")) 
            {
                Player player = Player.Get(sender);
                if (PetManager.SchematicsPets.ContainsKey(player))
                {
                    PetManager.RemovePet(player);
                    response = "Pet rimosso";
                    return true;
                }
                else 
                {
                    response = "Non hai nessun Pet attivo al momento";
                    return true;
                }
            }
        }
        response = $"Devi scegliere un azione valida da eseguire\nUsa `.pet add <nome-pet>` per spawnare un pet\nUsa `.pet remove` per rimuovere il pet che stai utilizzando";
        return true;
    }

    public string Command { get; } = "pet";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Spawna un pet";
}
