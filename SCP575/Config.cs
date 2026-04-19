using System.ComponentModel;

namespace SCP_575;

public class Config
{
    [Description("Plugin Config, All times in seconds")]
    public float loopTime { get; set; } = 120;
    public int spawnChance { get; set; } = 50;
    public float spawnDelay { get; set; } = 5;
    public float duration575 { get; set; } = 10;
    public float damagePerSecond { get; set; } = 10;
    public bool SurfaceEnabled { get; set; } = false;
    [Description("Msg Config")]
    public string killMsg575 { get; set; } = "SCP-575 ti ha ucciso";
    public string warnHint575 { get; set; } = "SCP-575 spawnerà in questa stanza tra $value secondi";
    public float hintDuration { get; set; } = 5;
}