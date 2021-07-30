using System.Linq;
using System.Runtime.CompilerServices;
using CitizenFX.Core;
using FivePD.API;

namespace EternityLifeCallouts.Extensions
{
    public static class CalloutExtensisions
    {
        public static Ped GetClosestPlayer(this Callout callout, Vector3 position)
        {
            var closestPlayer = callout.AssignedPlayers.First();
            var distance = 999f;
            foreach (var player in callout.AssignedPlayers)
            {
                var distanceToPosition = player.Position.DistanceToSquared(position);
                if (distance > distanceToPosition)
                {   
                    Debug.WriteLine($"Distance was {distance} is now {distanceToPosition}");
                    closestPlayer = player;
                    distance = distanceToPosition;
                }
            }
            return closestPlayer;
        }
    }
}