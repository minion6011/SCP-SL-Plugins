using LabApi.Events.CustomHandlers;
using MEC;
using System.Collections.Generic;

namespace SCP_575;


public class EventsHandler : CustomEventsHandler
{
    public override void OnServerRoundStarted()
    {
        Timing.RunCoroutine(loop575());
    }
    private IEnumerator<float> loop575()
    {
        while (true)
        {
            yield return Timing.WaitForSeconds(Plugin.Singleton.Config.loopTime);
            // Spawn chance (0-99) < spawnChance
            if (Plugin.Singleton.rnd.Next(0, 100) < Plugin.Singleton.Config.spawnChance)
            {
                Plugin.Singleton.SpawnSCP575();
            }
        }
    }
}