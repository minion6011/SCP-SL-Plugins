using System.ComponentModel;

namespace CustomBreachScenarios;

public class Config
{
    [Description("Delay between trying to spawn spectator as SCP if there's not any")]
    public int DelayedSpawnInterval { get; private set; } = 6;
}