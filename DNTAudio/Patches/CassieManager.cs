using Cassie;
using CentralAuth;
using HarmonyLib;
using LabApi.Features.Wrappers;
using Mirror;
using System;
using System.Collections.Generic;
using UserSettings.ServerSpecific;

namespace DNTAudio.Patches;

[HarmonyPatch(typeof(CassieAnnouncementDispatcher), "PlayNewAnnouncement")]
public static class CassieFilterPatch
{
    [HarmonyPrefix]
    public static bool Prefix(CassieAnnouncement annc)
    {
        annc.OnStartedPlaying();
        CassieTtsPayload payload = annc.Payload;
        float postAnnouncementCooldown = annc.PostAnnouncementCooldown;
        if (CassieTtsAnnouncer.TryPlay(payload, out var totalDuration))
        {
            double finischTime = NetworkTime.time + (double)totalDuration;

            AccessTools.Property(typeof(CassieAnnouncementDispatcher), nameof(CassieAnnouncementDispatcher.CurrentAnnouncement))
               .SetValue(null, annc);

            AccessTools.Property(typeof(CassieAnnouncementDispatcher), nameof(CassieAnnouncementDispatcher.AnnouncementFinishTime))
                .SetValue(null, finischTime);

            AccessTools.Property(typeof(CassieAnnouncementDispatcher), nameof(CassieAnnouncementDispatcher.NextAnnouncementTime))
                .SetValue(null, (finischTime + (double)postAnnouncementCooldown) );

            //payload.SendToAuthenticated();
            payload.SendToFiltered();
            return false;
        }
        return true;
    }

    public static void SendToFiltered<T>(this T message, int channelId = 0) where T : struct, NetworkMessage
    {
        foreach (ReferenceHub hub in ReferenceHub.AllHubs)
        {
            if (hub.Mode == ClientInstanceMode.Unverified) continue;

            Player p = Player.Get(hub);
            if (p == null) continue;

            if (!IsMuted(p, Plugin.Singleton.Config.CassieID))
                hub.networkIdentity.connectionToClient.Send(message, channelId);
        }
    }


    public static bool IsMuted(Player player, int settingId) {
        SSTwoButtonsSetting currentSetting = ServerSpecificSettingsSync.GetSettingOfUser<SSTwoButtonsSetting>(
            player.ReferenceHub, settingId
        );
        return currentSetting.SyncIsB;
    }

    [HarmonyPatch(typeof(AudioPlayer), nameof(AudioPlayer.CreateOrGet))]
    public static class AudioGet
    {
        [HarmonyPrefix]
        public static bool Prefix(ref AudioPlayer __result, string name, string autoPlayClip, Action<AudioPlayer> onAutoPlay, bool destroyWhenAllClipsPlayed, bool sendSoundGlobally, List<ReferenceHub> owners, byte controllerId, Action<AudioPlayer> onIntialCreation, ref Func<ReferenceHub, bool> condition)
        {
            var originalCondition = condition;
            condition = hub => (originalCondition?.Invoke(hub) ?? true) && !IsMuted(Player.Get(hub), Plugin.Singleton.Config.AudioPlayerID);

            if (AudioPlayer.TryGet(name, out var player))
            {
                if (!string.IsNullOrEmpty(autoPlayClip) && AudioClipStorage.AudioClips.ContainsKey(autoPlayClip))
                {
                    onAutoPlay?.Invoke(player);
                    player.AddClip(autoPlayClip);
                }

                __result = player;
                return false;
            }
            __result = AudioPlayer.Create(name, autoPlayClip, onAutoPlay, destroyWhenAllClipsPlayed, sendSoundGlobally, owners, controllerId, onIntialCreation, condition);
            return false;
        }
    }


}