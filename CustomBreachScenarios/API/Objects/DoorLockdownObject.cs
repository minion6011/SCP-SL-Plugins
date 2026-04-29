using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Enums;
using LabApi.Features.Wrappers;
using System.ComponentModel;
using System.Security.Policy;


namespace CustomBreachScenarios.API.Objects
{
    public class DoorLockdownObject
    {
        /// <summary>
        /// Gets or sets Lockdown time.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets Lockdown chance.
        /// </summary>
        public int Chance { get; set; }

        /// <summary>
        /// Gets or sets <see cref="DoorName"/> to be Locked down.
        /// </summary>
        public DoorName DoorType { get; set; }

        [Description("None, Regular079, Lockdown079, Warhead, AdminCommand, DecontLockdown, DecontEvacuate, SpecialDoorFeature, NoPower, Isolation, Lockdown2176")]
        /// <summary>
        /// Gets or sets <see cref="DoorLockReason"/> that will be used on door.
        /// </summary>
        public DoorLockReason DoorLockType { get; set; }
    }
}
