using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp0492Events;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MEC;

namespace ZombiCustom;


public class EventsHandler : CustomEventsHandler
{
    static System.Random rnd = new System.Random();
    // Figlio di 106
    public override void OnPlayerHurt(PlayerHurtEventArgs ev) {
        if (ev.Attacker != null && ev.Player != null && !ev.Player.IsSCP && ev.Attacker.CustomInfo == "Figlio di 106") {
            if (rnd.Next(1, 4) == 1) {
                PocketDimension.ForceInside(ev.Player);

                Timing.CallDelayed(0.04f, () =>
                {
                    PocketDimension.ForceExit(ev.Player);
                    ev.Player.DisableEffect<CustomPlayerEffects.PocketCorroding>();
                    ev.Player.DisableEffect<CustomPlayerEffects.Corroding>();
                    ev.Player.DisableEffect<CustomPlayerEffects.Sinkhole>();
                });

                Timing.CallDelayed(0.15f, () =>
                {
                    PocketDimension.ForceInside(ev.Player);
                });
            } 

        }
    }
    // Global
    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        if (ev.Player != null && ev.NewRole.RoleTypeId == PlayerRoles.RoleTypeId.Spectator && ev.OldRole == PlayerRoles.RoleTypeId.Scp0492) {
            ev.Player.CustomInfo = null;
        }
        else if (ev.Player != null && ev.NewRole.RoleTypeId == PlayerRoles.RoleTypeId.Scp0492) {
            ZombiManager.SelectRarity(ev.Player);
        }

    }
    // Texiano
    public override void OnScp0492ConsumedCorpse(Scp0492ConsumedCorpseEventArgs ev)
    {
        if (ev.Player != null && ev.Player.CustomInfo == "Texiano")
        {
            ev.Player.AddAmmo(ItemType.Ammo44cal, Plugin.Singleton.Config.TexianoBulletForBody);
        }
    }
    public override void OnPlayerDroppingItem(PlayerDroppingItemEventArgs ev)
    {
        if (ev.Player != null && ev.Player.IsSCP && ev.Player.Role == PlayerRoles.RoleTypeId.Scp0492)
        {
            ev.IsAllowed = false;
        }
    }
}
