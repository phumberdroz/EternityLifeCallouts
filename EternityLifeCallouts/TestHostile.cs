using System.Linq;
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
            this.InitInfo(new Vector3(-1084.096f, -769.7409f, 19.35719f));
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
            for (var i = 0; i < 5; i++)
            {
                var spawnedPed = await this.SpawnPed(RandomUtils.GetRandomPed(), this.Location.Around(5));
                spawnedPed.Weapons.Give(WeaponHash.PistolMk2, 600, true, true);
                spawnedPed.RelationshipGroup = "HATES_PLAYER";
                spawnedPed.Task.FightAgainstHatedTargets(this.StartDistance);
                spawnedPed.AlwaysKeepTask = true;
            }
        }
    }
    #endif
}