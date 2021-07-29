using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
#if DEBUG
    [CalloutProperties("Test Hostility", "Meat", "1.0.0")]
    public class TestHostile : Callout
    {
        public TestHostile()
        {
            this.InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.Position.Around(100)));
            this.ShortName = "test call should not happen ever";
            this.ResponseCode = 3;
            this.StartDistance = 150;
        }

        public override async Task OnAccept()
        {
            this.InitBlip(75f, (BlipColor) 66, (BlipSprite) 9, 100);
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            for (var i = 0; i < 1; i++)
            {
                var spawnedPed = await this.SpawnPed(RandomUtils.GetRandomPed(), this.Location.Around(5));
                spawnedPed.AttachBlip();
                spawnedPed.Weapons.Give(WeaponHash.CombatPistol, 600, true, true);
                Debug.WriteLine($"Player Relation Ship is {Game.PlayerPed.RelationshipGroup}");
                spawnedPed.RelationshipGroup = "HATES_PLAYER";
                API.SetPedRelationshipGroupHash(spawnedPed.Handle, (uint) API.GetHashKey("HATES_PLAYER"));
                API.SetRelationshipBetweenGroups(5, (uint) API.GetHashKey("HATES_PLAYER"),
                    (uint) API.GetHashKey("PLAYER"));
                API.SetRelationshipBetweenGroups(5, (uint) API.GetHashKey("PLAYER"),
                    (uint) API.GetHashKey("HATES_PLAYER"));
                Debug.WriteLine($"RElation ship is: {spawnedPed.GetRelationshipWithPed(Game.PlayerPed)}");
                API.SetPedCombatAttributes(spawnedPed.Handle, 46, true);
                API.SetPedCombatAbility(spawnedPed.Handle, 100);
                API.SetPedCombatMovement(spawnedPed.Handle, 2);
                API.SetPedCombatRange(spawnedPed.Handle, 0);
                spawnedPed.Task.WanderAround(spawnedPed.Position, 5);
            }
        }
    }
#endif
}