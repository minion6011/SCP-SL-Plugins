using System.ComponentModel;

namespace ZombiCustom;

public class Config {
    [Description("Percentuali di spawn delle rarità degli zombi")]
    public float RarityLeggendary { get; set; } = 1f;
    public float RarityEpic { get; set; } = 5;
    public float RarityRare { get; set; } = 10;
    public float RarityUncommon { get; set; } = 15;
    public float RarityCommon { get; set; } = 20;

    // Hint
    [Description("Durata dell'hint degli zombi")]
    public ushort HintDuration { get; set; } = 15;
    public ushort HintCooldownDuration { get; set; } = 5;
    // Zombi Common

    [Description("Zombi 'Il Nano'")]
    public string NanoHint { get; set; } = "<color=green>Il Nano</color>\\nSei più piccolo";
    public int NanoHealth { get; set; } = 300;

    [Description("Zombi 'Velocista'")]
    public string VelocistaHint { get; set; } = "<color=yellow>Velocista</color>\\nSei più veloce";
    public byte VelocistaSpeed { get; set; } = 25;

    [Description("Zombi 'Tank'")]
    public string TankHint { get; set; } = "<color=blue>Tank</color>\\nSei leggermente lento\\nHai molta vita";
    public byte TankSlowness { get; set; } = 25;
    public int TankHealth { get; set; } = 800;

    [Description("Zombi 'Kamikaze'")]
    public string KamikazeHint { get; set; } = "<color=red>Kamikaze</color>\\nPremi il tasto dell'abilità per detonare";
    public int KamikazeHealth { get; set; } = 200;

    [Description("Zombi 'Il Cagatore'")]
    public string CagatoreHint { get; set; } = "<color=red>Il cagatore</color>\\nPremi il tasto dell'abilità\\nPer creare una pozza di Tantrum";
    public int CagatoreCooldown { get; set; } = 40;

    [Description("Zombi 'Il Lanciatore'")]
    public string LanciatoreHint { get; set; } = "<color=red>Il Lanciatore</color>\\nPremi il tasto dell'abilità\\nPer ottenere e lanciare la granata";
    public int LanciatoreCooldown { get; set; } = 80;
    // Rarity Uncommon
    [Description("Zombi 'Urlatore'")]
    public string UrlatoreHint { get; set; } = "<color=blue>Urlatore</color>\\nPremi il tasto dell'abilità\\nPer assordare i giocatori vicini";
    public int UrlatoreCooldown { get; set; } = 50;
    public float UrlatoreAbilityRadius { get; set; } = 6;
    public int UrlatoreEffectDuration { get; set; } = 25;

    [Description("Zombi 'Light Eater'")]
    public string LightEaterHint { get; set; } = "<color=yellow>Light Eater</color>\\nPremi il tasto dell'abilità\\nPer spegnere le luci nella tua stanza";
    public float LightEaterAbilityDuration {  get; set; } = 20;
    public int LightEaterCooldown { get; set; } = 50;

    [Description("Zombi 'Battalion’s Backup'")]
    public string BattalionHint { get; set; } = "<color=red>Battalion’s Backup</color>\\nPremi il tasto dell'abilità\\nI tuoi compagni vicini\\nprendono il 20% di danni in meno";
    public int BattalionCooldown { get; set; } = 45;
    public int BattalionAbilityRadius { get; set; } = 6;
    public byte BattalionAbilityIntesity { get; set; } = 25;
    public float BattalionAbilityDuration { get; set; } = 15;

    // Rarity Rare
    [Description("Zombi 'Supporter'")]
    public string SupporterHint { get; set; } = "<color=#FF00FF>Supporter</color>\\nPremi il tasto dell'abilità\\nPer curare gli SCP vicini del 10%";
    public int SupporterCooldown { get; set; } = 50;
    public int SupporterAbilityPercent { get; set; } = 10;
    public int SupportertAbilityRadius { get; set; } = 6;

    [Description("Zombi 'Ruttatore'")]
    public string RuttatoreHint { get; set; } = "<color=red>Ruttatore</color>\\nPremi il tasto dell'abilità\\nPer accecare e rallentare i giocatori vicini";
    public int RuttatoreCooldown { get; set; } = 60;
    public float RuttatoreDurationFlashBang { get; set; } = 5;
    public float RuttatoreDurationSlowness { get; set; } = 10;
    public byte RuttatoreIntesitySlowness { get; set; } = 25;
    public int RuttatoreAbilityRadius { get; set; } = 6;

    [Description("Zombi 'Figlio di 106'")]
    public string Figlio106Hint { get; set; } = "<color=blue>Figlio di 106</color>\\nQuando hitti qualcuno hai il\\n25% di portarli nella pocket dimension";

    [Description("Zombi 'Slender'")]
    public string SlenderHint { get; set; } = "<color=red>Slender</color>\\nPremi il tasto dell'abilità\\nPer teletrasportarti da un giocatore casuale\\naccecarlo e rallentarlo";
    public float SlenderAbilityDuration { get; set; } = 15;
    public float SlenderAbilityShield { get; set; } = 100;
    public int SlenderCooldown { get; set; } = 90;

    // Rarity Leggendary
    [Description("Zombi 'Femboy'")]
    public string FemboyHint { get; set; } = "<color=#FF00FF>Femboy</color>\\nPremi il tasto dell'abilità\\nPer far droppare l'oggetto in mano a tutti\\ne curare tutti gli scp di 100hp";
    public int FemboyCooldown { get; set; } = 90;
    public int FemboyAbilityRadius { get; set; } = 8;

}
