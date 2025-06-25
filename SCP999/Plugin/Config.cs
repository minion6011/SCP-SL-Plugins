namespace SCP999;

public class Config
{
    public int MinPlayer { get; set; } = 12;
    public bool CassieMsg { get; set; } = true;
    public float AbilityRadius { get; set; } = 5.0f;
    public string HintMsg { get; set; } = "<color=orange><b>SCP-999</b></color>\\nAiuta gli umani\\nCura gli umani\\nRallenta gli SCP";
    public float HintDuration { get; set; } = 10.0f;
}
