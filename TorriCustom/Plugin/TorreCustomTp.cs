
using LabApi.Features.Wrappers;
using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace TorreCustom;

public class TorreCustomTp
{
    internal static IEnumerator<float> TeleportCoroutine()
    {
        while (true)
        {
            foreach (var player in Player.ReadyList) { 
                var positionDoor1 = new Vector3(-10.519f, 300.956f, -29.237f); // Replace with actual position of Door1
                var positionDoor2 = new Vector3(-19.113f, 314.555f, -32.182f); // Replace with actual position of Door1
                if ( // Positon X
                    player.Position.x <= positionDoor1.x + Plugin.Singleton.Config.Door1Radius
                    &&
                    player.Position.x >= positionDoor1.x - Plugin.Singleton.Config.Door1Radius
                    && // Position Y
                    player.Position.y <= positionDoor1.y + Plugin.Singleton.Config.Door1Radius
                    &&
                    player.Position.y >= positionDoor1.y - Plugin.Singleton.Config.Door1Radius
                    && // Position Z
                    player.Position.z <= positionDoor1.z + Plugin.Singleton.Config.Door1Radius
                    &&
                    player.Position.z >= positionDoor1.z - Plugin.Singleton.Config.Door1Radius
                    )
                {
                    player.Position = new Vector3(-16.6f, 314.474f, -32.2f);
                }
                else if ( // Positon X
                    player.Position.x <= positionDoor2.x + Plugin.Singleton.Config.Door2Radius
                    &&
                    player.Position.x >= positionDoor2.x - Plugin.Singleton.Config.Door2Radius
                    && // Position Y
                    player.Position.y <= positionDoor2.y + Plugin.Singleton.Config.Door2Radius
                    &&
                    player.Position.y >= positionDoor2.y - Plugin.Singleton.Config.Door2Radius
                    && // Position Z
                    player.Position.z <= positionDoor2.z + Plugin.Singleton.Config.Door2Radius
                    &&
                    player.Position.z >= positionDoor2.z - Plugin.Singleton.Config.Door2Radius
                    )
                {
                    player.Position = new Vector3(-10.445f, 300.960f, -31.264f);
                }
            }
            yield return Timing.WaitForSeconds(0.02f);
        }
    }

}