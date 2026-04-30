namespace CustomBreachScenarios.API.Objects;

using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// Block decontamination.
/// </summary>
public class DecontaminationError
{
    /// <summary>
    /// Chance of decontamination block.
    /// </summary>
    public int Chance { get; set; }

    [Description("Time until decontamination is triggered")]
    public int Time { get; set; } = 900;

    [Description("Time-TimeTolerance = DecontaminationError Trigger")]
    public int TimeTolerance { get; set; } = 30;

    public string ElevatorText { get; set; } = "Decontamination Error";

    /// <summary>
    /// Commands that will be executed after an 'x' ​​delay from the event
    /// </summary>
    [Description("Usage: [<delay1> : ['command1...', 'command2...'], <delay2> : ... ] ")]
    public Dictionary<int, List<string>> Commands { get; set; } = new Dictionary<int, List<string>>() { { 10, new List<string>() { "/bc 10 test1 boradcast", "/bc 20 test2 boradcast" } } };

}