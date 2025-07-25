using System.ComponentModel;

namespace SCP_999;

public class Config
{
    public int MinPlayer { get; set; } = 12;
    public bool CassieMsg { get; set; } = true;
    public float AbilityRadius { get; set; } = 5.0f;
    public string HintMsg { get; set; } = "<color=orange><b>SCP-999</b></color>\\nAiuta gli umani\\nCura gli umani\\nRallenta gli SCP\\nPremi il tasto dell'abilità\\nPer dare un effetto curativo";
    public float HintDuration { get; set; } = 10.0f;

    [Description("Config dell'abilità di SCP-999")]
    public float HintCooldownDuration { get; set; } = 5;
    public float KeyAbilityCooldown { get; set; } = 45;
    public int KeyAbilityHp { get; set; } = 150;
    public float KeyAbilityRadius { get; set; } = 5.5f;
    public byte KeyAbilityIntesity { get; set; } = 5;
    public float KeyAbilityDuration { get; set; } = 25;
    
}
