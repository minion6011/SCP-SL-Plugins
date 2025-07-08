using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace PetPlugin;


public class EventsHandler : CustomEventsHandler
{
    public override void OnPlayerLeft(PlayerLeftEventArgs ev)
    {
        if (PetManager.SchematicsPets.ContainsKey(ev.Player))
        {
            PetManager.RemovePet(ev.Player);
        }
    
    }
}
