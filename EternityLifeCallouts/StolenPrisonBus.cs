using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("Stolen Prison Bus", "meat", "1.0")]
    public class StolenPrisonBus : Callout
    {
        public StolenPrisonBus()
        {
            this.InitInfo(
                World.GetNextPositionOnStreet(Game.PlayerPed.Position.Around(RandomUtils.GetRandomNumber(100, 700)),
                    false));
            this.ShortName = "Stolen Prison Bus";
            this.CalloutDescription = "911 Call : Stolen Prison Bus.";
            this.ResponseCode = 3;
            this.StartDistance = 200;
        }

        public override async Task OnAccept()
        {
            this.InitBlip(100f, (BlipColor) 66, (BlipSprite) 9, 100);
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            // Scenrario onlyBus
            // Scenrario handcuffedDriver
            // Scenrario PrisonersEscapingWithBus
            var scenarios = new List<Action>
            {
                () => ScenarioOnlyBus(),
                // () => ScenarioHandcuffedDriver(ped),
            };

            scenarios.SelectRandom()();
        }

        private void ScenarioOnlyBus()
        {
            this.SpawnVehicle(VehicleHash.PBus, this.Location, 0f);
        }
    }
}