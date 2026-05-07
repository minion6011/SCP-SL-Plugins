using CommandSystem;
using CommandSystem.Commands.RemoteAdmin.MutingAndIntercom;
using HarmonyLib;
using LabApi.Features.Wrappers;
using System;

namespace OnlineMute.Patches;

[HarmonyPatch(typeof(MuteCommand), nameof(MuteCommand.Execute))]
public static class OnlineMuteCommand
{
    [HarmonyPrefix]
    public static bool Prefix(MuteCommand __instance, bool __result, ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count == 2 && float.TryParse(arguments[1], out float time))
        {
            string userId = arguments[0];
            Player player = Player.Get(userId);

            if (player != null)
            {

                time *= Plugin.Singleton.Config.CommandMultiplier; // float time != 0 

                if (!Plugin.Singleton.PlayersToCheck.ContainsKey(userId))
                {
                    Plugin.Singleton.PlayersToCheck.Add(userId, time);
                    DataStorage.Save(Plugin.Singleton.PlayersToCheck);
                    player.Mute(true);
                    player.SendHint(Plugin.Singleton.Config.MuteHint.Replace("$value", (time / 60).ToString()));

                    response = $"Ho mutato il player {player.DisplayName} per {time}±{Plugin.Singleton.Config.CheckTollerance}s";
                    __result = true;
                    return false;
                }
                else
                {
                    response = "Il player è già mutato";
                }
            }
            else
            {
                response = "Impossibile trovare il player";
            }
        }
        else {
            response = "Argomenti del comando invalidi, Esempio: mute <playerId oppure steamid@steam> <time>";
        }
        return false;
    }
}

// Options
[HarmonyPatch(typeof(MuteCommand), nameof(MuteCommand.Description), MethodType.Getter)]
public static class MuteDescriptionPatch
{
    [HarmonyPrefix]
    public static bool Prefix(ref string __result)
    {
        __result = "Muta temporaneamento un giocatore (il tempo scala quando è nel server)";
        return false;
    }
}

[HarmonyPatch(typeof(MuteCommand), nameof(MuteCommand.Usage), MethodType.Getter)]
public static class MuteUsagePatch
{
    [HarmonyPrefix]
    public static bool Prefix(ref string[] __result)
    {
        __result = new[] { "SteamID", "Duration" };
        return false;
    }
}