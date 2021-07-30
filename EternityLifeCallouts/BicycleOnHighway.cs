using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("Bicycle on Highway", "meat", "1.0")]
    public class BicycleOnHighway : Callout
    {
        private static readonly Vector3[] CalloutPositions =
        {
            new Vector3(1308.065f, 581.2718f, 79.78977f),
            new Vector3(1594.56f, 1009.64f, 78.95673f),
            new Vector3(1689.932f, 1352.383f, 87.02444f),
            new Vector3(1878.03f, 2336.891f, 55.68655f),
            new Vector3(2061.051f, 2644.353f, 52.16157f),
            new Vector3(2360.496f, 2856.932f, 40.1833f),
            new Vector3(2539.596f, 3042.605f, 42.92778f),
            new Vector3(2946.281f, 3813.16f, 52.26584f),
            new Vector3(2795.884f, 446.548f, 48.0796f),
            new Vector3(2649.814f, 4928.652f, 44.39455f),
            new Vector3(2331.14f, 5905.173f, 47.67876f),
            new Vector3(1436.997f, 6474.696f, 20.40421f),
            new Vector3(777.1246f, 6513.02f, 24.64042f),
            new Vector3(-589.123f, 5663.769f, 38.00635f),
            new Vector3(-1529.918f, 4981.798f, 62.08772f),
            new Vector3(-2329.675f, 4112.701f, 35.33438f),
            new Vector3(-589.123f, 5663.769f, 38.00635f),
            new Vector3(-1529.918f, 4981.798f, 62.08722f),
            new Vector3(-2329.675f, 4112.701f, 35.33438f),
            new Vector3(-2620.146f, 2824.454f, 16.38638f),
            new Vector3(-3039.727f, 1872.351f, 29.84845f),
            new Vector3(-3128.839f, 835.1783f, 16.17631f),
            new Vector3(-2539.692f, -185.8579f, 19.42014f),
            new Vector3(-1842.228f, -595.9995f, 11.09579f),
            new Vector3(-1842.228f, -595.9995f, 11.09579f),
            new Vector3(-765.19f, -528.42f, 25.16f),
            new Vector3(317.59f, -1220.15f, 38.28f),
            new Vector3(1627.3f, -960.47f, 62.87f),
            new Vector3(-396.24f, -810.06f, 38.25f),
            new Vector3(861.28f, 101.79f, 69.86f),
            new Vector3(2627.4f, 385.45f, 96.96f)
        };

        public BicycleOnHighway()
        {
            InitInfo(Utils.GetLocation(CalloutPositions, Game.PlayerPed.Position));
            ShortName = "Bicycle On Highway";
            CalloutDescription = "911 Call : Bicycle reported driving on the highway.";
            ResponseCode = 3;
            StartDistance = 100;
        }

        public override async Task OnAccept()
        {
            InitBlip();
            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info",
                "Reports of a bicycle driving on the highway.");
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            var ped = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            var bicyclesModelNames = new[]
            {
                "scorcher", "cruiser", "tribike"
            };

            var model = new Model(bicyclesModelNames.SelectRandom());
            var vehicle = await SpawnVehicle(model, World.GetNextPositionOnStreet(Location));
            ped.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            ped.Task.CruiseWithVehicle(vehicle, 1f, 1); // Todo validate Driving Style
            ped.AlwaysKeepTask = true;
            ped.BlockPermanentEvents = true;
            ped.IsPersistent = true;
        }
    }
}