using System.Linq;
using UserSettings.ServerSpecific;

namespace DNTAudio.SSSetting
{
    public class AudioCustomSettings : CustomSettingsBase
    {

        public override string Name => "DNTAudio";
        public override string Description => "Audio Settings";


        public override void Activate()
        {

            ServerSpecificSettingBase[] NewSettings =
            {
                new SSGroupHeader(Plugin.Singleton.Config.SHeader),
                new SSTwoButtonsSetting(Plugin.Singleton.Config.CassieID, Plugin.Singleton.Config.CassieSName, "No", "Si", hint: Plugin.Singleton.Config.CassieSHint),
                new SSTwoButtonsSetting(Plugin.Singleton.Config.AudioPlayerID, Plugin.Singleton.Config.AudioPlayerSName, "No", "Si", hint: Plugin.Singleton.Config.AudioPlayerSHint),
            };
            if (ServerSpecificSettingsSync.DefinedSettings == null)
                ServerSpecificSettingsSync.DefinedSettings = new ServerSpecificSettingBase[0];

            ServerSpecificSettingsSync.DefinedSettings = ServerSpecificSettingsSync.DefinedSettings
                .Concat(NewSettings)
                .ToArray();

            ServerSpecificSettingsSync.SendToAll();
        }

        public override void Deactivate()
        {
        }
    }
}