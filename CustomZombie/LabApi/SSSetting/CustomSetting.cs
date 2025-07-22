using LabApi.Features.Wrappers;
using System.Linq;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace ZombiCustom.SSSetting
{
    /// <summary>
    /// Provides a example implementation of a server-specific settings that add custom abilities.
    /// <br /> This is a relatively simple implementation, good for understanding the basics of the framework.
    /// </summary>
    public class AbilityKey : CustomSettingsBase
    {

        public override string Name => "Zombi Custom";
        public override string Description => "Impostazioni degli zombi custom";


        public override void Activate()
        {

            ServerSpecificSettingBase[] NewSettings =
            {
                new SSGroupHeader("Zombi Custom"),
                new SSKeybindSetting(id: (int)IdKey.AbilityKey, label: "Abilità", suggestedKey: KeyCode.F, hint: "Utilizza l'abilità del tuo zombi."),
            };
            if (ServerSpecificSettingsSync.DefinedSettings == null)
                ServerSpecificSettingsSync.DefinedSettings = new ServerSpecificSettingBase[0];

            ServerSpecificSettingsSync.DefinedSettings = ServerSpecificSettingsSync.DefinedSettings
                .Concat(NewSettings)
                .ToArray();

            ServerSpecificSettingsSync.SendToAll();

            ServerSpecificSettingsSync.ServerOnSettingValueReceived += ProcessUserInput;
        }

        public override void Deactivate()
        {
            ServerSpecificSettingsSync.ServerOnSettingValueReceived -= ProcessUserInput;
        }

        public override void ProcessUserInput(ReferenceHub sender, ServerSpecificSettingBase setting)
        {
            switch ((IdKey)setting.SettingId)
            {

                case IdKey.AbilityKey
                when setting is SSKeybindSetting keybind:
                    {
                        if (!keybind.SyncIsPressed)
                            break;

                        AbilityFunction(sender);

                    }
                    break;
            }
        }

        private void AbilityFunction(ReferenceHub sender)
        {
            // Code
            Player player = Player.Get(sender);
            if (player != null && player.IsSCP && player.Role == PlayerRoles.RoleTypeId.Scp0492)
            {
                ZombiAbilityManager.ActivateAbility(player);
            }
        }





        private enum IdKey
        {
            AbilityKey,
        }
    }
}
