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
            InitInfo(
                World.GetNextPositionOnStreet(Game.PlayerPed.Position.Around(RandomUtils.GetRandomNumber(100, 700))));
            ShortName = "Stolen Prison Bus";
            CalloutDescription = "911 Call : Stolen Prison Bus.";
            ResponseCode = 3;
            StartDistance = 200;
        }

        public override async Task OnAccept()
        {
            InitBlip(100f);
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            // Scenrario onlyBus
            // Scenrario handcuffedDriver
            // Scenrario PrisonersEscapingWithBus
            var scenarios = new List<Action>
            {
                () => ScenarioOnlyBus()
                // () => ScenarioHandcuffedDriver(ped),
            };

            scenarios.SelectRandom()();
        }

        private void ScenarioOnlyBus()
        {
            SpawnVehicle(VehicleHash.PBus, Location);
        }
    }
}