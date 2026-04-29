namespace CustomBreachScenarios.API.Objects;

using MapGeneration;
using PlayerRoles;

/// <summary>
/// Delayed SCP Spawn Object.
/// </summary>
public class DelayedScpSpawnObject
{
    /// <summary>
    /// Gets or sets Spawn delay.
    /// </summary>
    public int Delay { get; set; }

    /// <summary>
    /// Gets or sets <see cref="RoleTypeId"/> that player will be spawned as.
    /// </summary>
    public RoleTypeId Role { get; set; }

    /// <summary>
    /// Gets or sets <see cref="RoomName"/> where player will spawn.
    /// </summary>
    public RoomName Room { get; set; }
}