using System.Collections.Generic;
using System.ComponentModel;

namespace TheSpy;

public class Config
{
    public string SpyHint { get; set; } = "<color=red>Spia</color>\\nTradisci il tuo team";
    public ushort SpyHintDuration { get; set; } = 20;
    public int SpyShield { get; set; } = 30;
    public int MinWaveSize { get; set; } = 7;

    [Description("Custom info di eventuali scp custom da ignorare nel controllo di fine round delle spie")]
    public List<string> ExcluedInfos { get; set; } = new List<string> { "\"Example Role\"" };

    [Description("I tutorial vengono contati come SCP?")]
    public bool CountTutorial { get; set; } = false;
    public bool Debug { get; set; } = false;
}
