namespace TheSpy;

public class Config
{
    public string DamageReason { get; set; } = "Sei stato ucciso da una spia";
    public string SpyHint { get; set; } = "<color=red>Spia</color>\\nTradisci il tuo team";
    public ushort SpyHintDuration { get; set; } = 20;
    public int SpyShield { get; set; } = 30;
    public int MinWaveSize { get; set; } = 7;
}
