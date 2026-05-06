using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OnlineMute;

public class Config
{
    [Description("Does the loop that controls players run in the lobby?")]
    public bool IsLobbyValid { get; set; } = false;

    [Description("How often (in seconds) the loop that checks for timeout players runs; The maximum delay the 'onlineMute' command can make in starting the loop to mute the player")]
    public float CheckTollerance { get; set; } = 10;

    [Description("Type of value that is inserted into the command in the R.A. - (command_value * this) - 60 = value in minutes, 1 value in seconds")]
    public ushort CommandMultiplier { get; set; } = 60;

    [Description("Hints")]
    public string MuteHint { get; set; } = "Sei stato mutato, durata: $value minuti";
    public string UnmuteHint { get; set; } = "Sei stato smutato";
    public float DurationHint { get; set; } = 10;
}