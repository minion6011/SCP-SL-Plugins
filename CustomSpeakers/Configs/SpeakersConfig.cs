using CustomSpeakers.Objects;
using System.Collections.Generic;

namespace CustomSpeakers;

public class SpeakersConfig
{
    public List<SpeakerObject> speakers { get; set; } = new() {
        new SpeakerObject()
    };
}