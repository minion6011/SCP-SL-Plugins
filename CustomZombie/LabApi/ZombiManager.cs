using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using MEC;

namespace ZombiCustom;

public class ZombiManager
{
    static System.Random rnd = new System.Random();
    public static void SelectRarity(Player player) {
        int chance = rnd.Next(100);
        if (chance <= Plugin.Singleton.Config.RarityCommon) {
            // Common
            SpawnCommon(player);
        }
        else if (chance <= Plugin.Singleton.Config.RarityCommon + Plugin.Singleton.Config.RarityUncommon) {
            // Uncommon
            SpawnUncommon(player);
        }
        else if (chance <= Plugin.Singleton.Config.RarityCommon + Plugin.Singleton.Config.RarityUncommon + Plugin.Singleton.Config.RarityRare) { 
            // Rare
            SpawnRare(player);
        }
        else if (chance <= Plugin.Singleton.Config.RarityCommon + Plugin.Singleton.Config.RarityUncommon + Plugin.Singleton.Config.RarityRare + Plugin.Singleton.Config.RarityEpic) {
            // Epic
            SpawnEpic(player);
        }
        else if (chance <= Plugin.Singleton.Config.RarityCommon + Plugin.Singleton.Config.RarityUncommon + Plugin.Singleton.Config.RarityRare + Plugin.Singleton.Config.RarityEpic + Plugin.Singleton.Config.RarityLeggendary) {
            // Legendary
            SpawnLeggendary(player);
        }
    }

    public static void SpawnCommon(Player player, int chance = 0) {
        if (chance == 0)
        {
            chance = rnd.Next(1, 6);
        }
        if (chance == 1)
        {
            // Il Nano
            player.CustomInfo = "Il Nano";
            Timing.CallDelayed(0.4f, () => player.Scale = new(0.6f, 0.6f, 0.6f));
            player.MaxHealth = 300;
            player.SendHint(Plugin.Singleton.Config.NanoHint, Plugin.Singleton.Config.HintDuration);
        }
        else if (chance == 2)
        {
            // Velocista
            player.CustomInfo = "Velocista";
            player.EnableEffect<MovementBoost>(intensity: Plugin.Singleton.Config.VelocistaSpeed);
            player.SendHint(Plugin.Singleton.Config.VelocistaHint, Plugin.Singleton.Config.HintDuration);
        }
        else if (chance == 3)
        {
            // Tank
            player.CustomInfo = "Tank";
            Timing.CallDelayed(0.4f, () => player.Scale = new(1.7f, 1f, 1.7f));
            player.MaxHealth = Plugin.Singleton.Config.TankHealth;
            player.Health = Plugin.Singleton.Config.TankHealth;
            player.EnableEffect<Slowness>(intensity: Plugin.Singleton.Config.TankSlowness);
            player.SendHint(Plugin.Singleton.Config.TankHint, Plugin.Singleton.Config.HintDuration);
        }
        else if (chance == 4) 
        {
            // Kamikaze
            player.CustomInfo = "Kamikaze";
            player.MaxHealth = Plugin.Singleton.Config.KamikazeHealth;
            player.Health = Plugin.Singleton.Config.KamikazeHealth;
            player.SendHint(Plugin.Singleton.Config.KamikazeHint, Plugin.Singleton.Config.HintDuration);
        }
        else if (chance == 5)
        {
            // Il cagatore
            player.CustomInfo = "Il Cagatore";
            player.SendHint(Plugin.Singleton.Config.CagatoreHint, Plugin.Singleton.Config.HintDuration);
        }
        else if (chance == 6) 
        {
            // Il lanciatore
            player.CustomInfo = "Il Lanciatore";
            player.SendHint(Plugin.Singleton.Config.LanciatoreHint, Plugin.Singleton.Config.HintDuration);
        }
    }

    public static void SpawnUncommon(Player player, int chance = 0)
    {
        if (chance == 0)
        {
            chance = rnd.Next(1, 3);
        }
        else if (chance == 1)
        {
            // Urlatore
            player.CustomInfo = "Urlatore";
            player.SendHint(Plugin.Singleton.Config.UrlatoreHint, Plugin.Singleton.Config.HintDuration);
        }
        else if (chance == 2)
        {
            // LightEater
            player.CustomInfo = "Light Eater";
            player.SendHint(Plugin.Singleton.Config.LightEaterHint, Plugin.Singleton.Config.HintDuration);
        }
        else if (chance == 3)
        {
            // Battalion’s Backup
            player.CustomInfo = "Battalion’s Backup";
            player.SendHint(Plugin.Singleton.Config.BattalionHint, Plugin.Singleton.Config.HintDuration);

        }
    }

    public static void SpawnRare(Player player, int chance = 0) 
    {
        if (chance == 0)
        {
            chance = rnd.Next(1, 2);
        }
        else if (chance == 1)
        {
            // Supporter
            player.CustomInfo = "Supporter";
            player.SendHint(Plugin.Singleton.Config.SupporterHint, Plugin.Singleton.Config.HintDuration);
        }
        else if (chance == 2)
        {
            // Ruttatore
            player.CustomInfo = "Il Ruttatore";
            player.SendHint(Plugin.Singleton.Config.RuttatoreHint, Plugin.Singleton.Config.HintDuration);
        }
    }

    public static void SpawnEpic(Player player, int chance = 0)
    {
        if (chance == 0)
        {
            chance = rnd.Next(1, 3);
        }
        else if (chance == 1)
        {
            // Figlio di 106
            player.CustomInfo = "Figlio di 106";
            player.SendHint(Plugin.Singleton.Config.Figlio106Hint, Plugin.Singleton.Config.HintDuration);
            player.EnableEffect<CustomPlayerEffects.Sinkhole>();
        }
        else if (chance == 2)
        {
            // Ruttatore
            player.CustomInfo = "Lo slender";
            player.SendHint(Plugin.Singleton.Config.SlenderHint, Plugin.Singleton.Config.HintDuration);
        }
        else if (chance == 3) 
        {
            // Texiano
            player.CustomInfo = "Texiano";
            player.MaxHealth = Plugin.Singleton.Config.TexianoHealth;
            player.Health = Plugin.Singleton.Config.TexianoHealth;
            // Give revolver
            Item item = player.AddItem(ItemType.GunRevolver);
            player.CurrentItem = item;
            // Give Ammo
            player.AddAmmo(ItemType.Ammo44cal, Plugin.Singleton.Config.TexianoStartBullets);
            player.SendHint(Plugin.Singleton.Config.TexianoHint, Plugin.Singleton.Config.HintDuration);
        }
    }

    public static void SpawnLeggendary(Player player, int chance = 0) {
        if (chance == 0)
        {
            chance = rnd.Next(1, 1);
        }
        else if (chance == 1)
        {
            // Femboy
            player.CustomInfo = "Femboy";
            player.MaxHealth = 450;
            player.Health = 450;
            player.SendHint(Plugin.Singleton.Config.FemboyHint, Plugin.Singleton.Config.HintDuration);
        }
    }
}
