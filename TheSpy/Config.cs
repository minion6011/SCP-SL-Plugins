namespace TheSpy;

public class Config
{
    public string DamageReason { get; set; } = "Sei stato ucciso dal team avversario";
    public string SpyHint { get; set; } = "<color=red>Spia</color>\\nTradisci il tuo team";
    public ushort SpyHintDuration { get; set; } = 10;
    public int SpyShield { get; set; } = 30;
    public int MinWaveSize { get; set; } = 7;
}