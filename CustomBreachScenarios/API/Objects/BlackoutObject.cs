using MapGeneration;
using System.Collections.Generic;


namespace CustomBreachScenarios.API.Objects
{
    public class BlackoutObject
    {
        /// <summary>
        /// Gets or sets Blackout delay.
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// Gets or sets Blackout time.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets Blackout chance.
        /// </summary>
        public int Chance { get; set; }

        /// <summary>
        /// Gets or sets List of <see cref="FacilityZone">Zones</see> affected by Blackout.
        /// </summary>
        public List<FacilityZone> Zones { get; set; } = new List<FacilityZone>();
    }
}
