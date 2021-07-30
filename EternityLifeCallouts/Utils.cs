using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace EternityLifeCallouts
{
    public static class Utils
    {
        public static Vector3 GetLocation(IEnumerable<Vector3> locations, Vector3 playerPosition)
        {
            var nearestLocation = 0.0f;
            var calloutCord = playerPosition;
            foreach (var vector3 in locations)
            {
                var distance = World.GetDistance(Game.PlayerPed.Position, vector3);
                if (nearestLocation != 0.0 && !(nearestLocation > (double) distance)) continue;
                nearestLocation = distance;
                if (nearestLocation > 450.0)
                    calloutCord = vector3;
            }

            return calloutCord;
        }

        [Obsolete("AdvNotify is depcrated use Callout.ShowNetworkedNotification instead")]
        public static void AdvNotify(
            string textureDict,
            string textureName,
            bool flash,
            int iconType,
            string sender,
            string subject,
            string message)
        {
            API.BeginTextCommandThefeedPost("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandThefeedPostMessagetext(textureDict, textureName, flash, iconType,
                sender, subject);
            API.EndTextCommandThefeedPostTicker(true, false);
        }

        public static void DrawSubtitle(string message, int duration)
        {
            API.BeginTextCommandPrint("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandPrint(duration, false);
        }

        public static void Notify(string message)
        {
            API.BeginTextCommandThefeedPost("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandThefeedPostTicker(false, true);
        }
    }
}