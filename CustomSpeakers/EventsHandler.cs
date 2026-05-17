using CustomSpeakers.Objects;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using System;
using System.Linq;
using UnityEngine;

namespace CustomSpeakers;


public class EventsHandler : CustomEventsHandler
{
    public override void OnServerRoundStarted()
    {
        foreach (SpeakerObject speakerObject in SpeakerPlugin.Singleton.SpeakersConfig.speakers) {

            try
            {
                if (!speakerObject.enabled)
                    continue;
                Room RefRoom = Room.Get(speakerObject.roomName).First();
                Vector3 localOffset = speakerObject.relativePosition;
                Vector3 globalPosition = RefRoom.Transform.TransformPoint(localOffset);

                LabApi.Features.Console.Logger.Debug(globalPosition.ToString());

                AudioPlayer MusicPlayer = AudioPlayer.CreateOrGet(speakerObject.name, onIntialCreation: (p) =>
                {
                    Speaker speaker = p.AddSpeaker(
                        "main",
                        position: globalPosition,
                        isSpatial: true,

                        minDistance: speakerObject.minDistance,
                        maxDistance: speakerObject.maxDistance,
                        volume: speakerObject.volume);
                });

                MusicPlayer.AddClip(speakerObject.name, loop: speakerObject.loop);
            }
            catch (Exception ex) {
                LabApi.Features.Console.Logger.Error($"Error while creating the speaker '{speakerObject.name}', {ex}");
            }
        }
    }
}