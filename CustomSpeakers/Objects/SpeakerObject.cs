using MapGeneration;
using System.ComponentModel;
using UnityEngine;

namespace CustomSpeakers.Objects
{

    public class SpeakerObject
    {
        public bool enabled { get; set; } = true;
        [Description("All names must be different")]
        public string name { get; set; } = "Audio Player";
        public string clipPath { get; set; } = "Path...";
        public RoomName roomName { get; set; } = RoomName.LczToilets;
        public Vector3 relativePosition { get; set; } = new Vector3(0, 0, 0);
        public bool loop { get; set; } = true;
        public float volume { get; set; } = 1f;
        public float minDistance { get; set; } = 5f;
        public float maxDistance { get; set; } = 15f;
    }
}