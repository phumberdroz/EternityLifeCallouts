using System;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
#if DEBUG
    public class BenchWarrant : Callout
    {
        private static readonly string[] ShortNames =
        {
            "Bench Warrant : Assault",
            "Bench Warrant : Child Support",
            "Bench Warrant : Domestic Violence",
            "Bench Warrant : Eluding",
            "Bench Warrant : Evading",
            "Bench Warrant : Homicide",
            "Bench Warrant : Rapist",
            "Bench Warrant : Tax Evasion",
            "Bench Warrant : Trespassing"
        };

        public BenchWarrant()
        {
            // var random = ambulanceom();
            this.InitInfo(World.GetNextPositionOnStreet(
                Game.PlayerPed.Position.Around(RandomUtils.GetRandomNumber(250, 700))));
            this.ShortName = ShortNames.SelectRandom();
            this.CalloutDescription = "911 Call : Tip line hit confirms sighting of wanted individual.";
            this.ResponseCode = 2;
            this.StartDistance = 180;
        }

        public override async Task OnAccept()
        {
            this.InitBlip(75f, (BlipColor) 66, (BlipSprite) 9, 100);
            // return base.OnAccept();
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            var suspect = await this.SpawnPed(RandomUtils.GetRandomPed(), this.Location, 0f);
            var handle = CitizenFX.Core.Native.API.RegisterPedheadshot(suspect.Handle);
            while (!CitizenFX.Core.Native.API.IsPedheadshotReady(handle) ||
                   !CitizenFX.Core.Native.API.IsPedheadshotValid(handle))
            {
                await BaseScript.Delay(800);
            }

            var txd = CitizenFX.Core.Native.API.GetPedheadshotTxdString(handle);
            // ExpandoObject expandoObject = await ((Utilities.GetPedDataDelegate) Utilities.GetPedData).Invoke(((Entity) suspect).NetworkId);
            // object data = (object) expandoObject;
        }
    }
#endif
}