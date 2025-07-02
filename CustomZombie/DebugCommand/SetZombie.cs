using CommandSystem;
using LabApi.Features.Wrappers;
using System;
using System.Linq;
using ZombiCustom;

namespace Zombi.Commands;


[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class SpawnCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count >= 1 && arguments.ElementAt(0) != null && arguments.ElementAt(0) != null)
        {
            if (arguments.ElementAt(0) == "Common")
            {
                if (int.TryParse(arguments.ElementAt(1), out int n))
                {
                    ZombiManager.SpawnCommon(Player.Get(sender), n);
                }
            }
            else if (arguments.ElementAt(0) == "Uncommon")
            {
                if (int.TryParse(arguments.ElementAt(1), out int n))
                {
                    ZombiManager.SpawnUncommon(Player.Get(sender), n);
                }
            }
            else if (arguments.ElementAt(0) == "Rare")
            {
                if (int.TryParse(arguments.ElementAt(1), out int n))
                {
                    ZombiManager.SpawnRare(Player.Get(sender), n);
                }
            }
            else if (arguments.ElementAt(0) == "Epic")
            {
                if (int.TryParse(arguments.ElementAt(1), out int n))
                {
                    ZombiManager.SpawnEpic(Player.Get(sender), n);
                }
            }
            else if (arguments.ElementAt(0) == "Leggendary")
            {
                if (int.TryParse(arguments.ElementAt(1), out int n))
                {
                    ZombiManager.SpawnLeggendary(Player.Get(sender), n);
                }
            }
        }
        response = "Usage: setzombie <rarity> <zombi_type>\n\n" +
                   "Rarity: 'Common': \n1 > Il Nano\n2 > Velocista\n3 > Tank\n4 > Kamikaze\n5 > Il Cagatore\n6 > Il Lanciatore\n\n" +
                   "Rarity: 'Uncommon': \n1 > Urlatore\n2 > Light Eater\n3 > Battalion’s Backup\n\n" +
                   "Rarity: 'Rare': \n1 > Supporter\n2 > Il Ruttatore\n\n" +
                   "Rarity: 'Epic': \n1 > Figlio di 106\n2 > Lo slender\n3 > Texiano\n\n" +
                   "Rarity: 'Leggendary':\n1 > Femboy";
        return true;
    }

    public string Command { get; } = "setzombie";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Trasforma chi usa il comando in uno Zombi Custom [DEBUG]";
}
