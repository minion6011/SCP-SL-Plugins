using CommandSystem;
using CommandSystem.Commands.RemoteAdmin.MutingAndIntercom;
using HarmonyLib;
using LabApi.Features.Wrappers;
using System;

namespace OnlineMute.Patches;

[HarmonyPatch(typeof(UnmuteCommand), nameof(UnmuteCommand.Execute))]
public static class UnmuteCommandPatch
{
    [HarmonyPrefix]
    public static bool Prefix(UnmuteCommand __instance, bool __result, ArraySegment<string> arguments, ICommandSender sender, out string response)
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
                    __result = true;
                    response = $"Ho smutato il player {player.DisplayName}";
                    return false;
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
            response = "Argomenti del comando invalidi, Esempio: unmute <steamid@steam>";
        }
        return false;
    }
}

// Options
[HarmonyPatch(typeof(UnmuteCommand), nameof(UnmuteCommand.Description), MethodType.Getter)]
public static class UnmuteDescriptionPatch
{
    [HarmonyPrefix]
    public static bool Prefix(ref string __result)
    {
        __result = "Smuta un giocatore mutato temporaneamente dal comando (mute)";
        return false;
    }
}

[HarmonyPatch(typeof(UnmuteCommand), nameof(UnmuteCommand.Usage), MethodType.Getter)]
public static class UnmuteUsagePatch
{
    [HarmonyPrefix]
    public static bool Prefix(ref string[] __result)
    {
        __result = new[] { "SteamID" };
        return false;
    }
}