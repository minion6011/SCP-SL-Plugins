using UserSettings.ServerSpecific;

namespace SCP_999.SSSetting
{
    public abstract class CustomSettingsBase
    {
        private static CustomSettingsBase _instance;

        /// <summary>
        /// Name
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Quick description of the setting
        /// </summary>
        public abstract string Description { get; }

        public abstract void Activate();
        public abstract void Deactivate();
        public abstract void ProcessUserInput(ReferenceHub hub, ServerSpecificSettingBase setting);
    }
}
