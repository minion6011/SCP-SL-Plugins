using System.ComponentModel;

namespace Heavy3114;

public class Config
{
    public int MinPlayer { get; set; } = 25;
    public string SCP3114Hint { get; set; } = "<color=red>SCP-3114</color>\\nAspetta 45s prima che la porta si apra";
    public ushort HintDuration { get; set; } = 15;
    [Description("Tempo dopo il quale la porta di 3114 si aprirà (Stanza di SCP-127)")]
    public float DoorOpenTime { get; set; } = 45;
}