using LabApi.Features.Wrappers;
using System.Linq;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace SCP_999.SSSetting
{
    /// <summary>
    /// Provides a example implementation of a server-specific settings that add custom abilities.
    /// <br /> This is a relatively simple implementation, good for understanding the basics of the framework.
    /// </summary>
    public class AbilityKey : CustomSettingsBase
    {

        public override string Name => "SCP-999 Abilità";
        public override string Description => "Tasti per le abilità di SCP-999";


        public override void Activate()
        {

            ServerSpecificSettingBase[] NewSettings = 
            {
                new SSGroupHeader("SCP-999"),
                new SSKeybindSetting(id: (int)IdKey.AbilityKey, label: "Abilità", suggestedKey: KeyCode.F, hint: "Utilizza l'abilità di SCP-999."),
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
            if (player != null && player == SCP999.Player999)
            {
                SCP999.ActivateAbility();
            }
        }

        private enum IdKey
        {
            AbilityKey,
        }
    }
}
