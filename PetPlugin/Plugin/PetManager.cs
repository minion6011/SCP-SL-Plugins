using LabApi.Features.Wrappers;
using MEC;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace PetPlugin;

public class PetManager
{
    public static Dictionary<Player, SchematicObject> SchematicsPets { get; private set; } = new Dictionary<Player, SchematicObject>();
    private static bool CoroutineRunning { get; set; } = false;
    public static void SpawnPet(Player player, string petname)
    {
        if (SchematicsPets.ContainsKey(player)) {
            RemovePet(player);
        }
        SchematicsPets.Add(player, ObjectSpawner.SpawnSchematic(petname, player.Position + new Vector3(-0.3f, 0.3f, 0.5f), player.Rotation, Vector3.one));

        if (!CoroutineRunning)
        {
            Timing.RunCoroutine(PositionCoroutine(), "PetPositionUpdate");
            CoroutineRunning = true;
        }
    }
    public static void RemovePet(Player player)
    {
        if (SchematicsPets.ContainsKey(player))
        {
            SchematicsPets[player].Destroy();
            SchematicsPets.Remove(player);
        }
    }

    internal static IEnumerator<float> PositionCoroutine()
    {
        while (true)
        {
            foreach (var el in SchematicsPets) {

                Vector3 forward = el.Key.Rotation * Vector3.forward;
                Vector3 right = el.Key.Rotation * Vector3.right;
                el.Value.Position = el.Key.Position + (right * 0.6f) + (-forward * 0.25f)  + new Vector3(0, 0.3f, 0);

                el.Value.Rotation = el.Key.Rotation;
            }

            yield return Timing.WaitForSeconds(0.015f);
        }
    }

}