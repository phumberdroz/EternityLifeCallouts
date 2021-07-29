using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;

namespace EternityLifeCallouts
{
#if DEBUG
    [CalloutProperties("Solicitation", "meat", "1.0")]
    public class Solicitation : Callout
    {
        private static readonly Vector3[] CalloutPositions =
        {
            new Vector3(80.02f, -1147.83f, 29.29f),
            new Vector3(-1169.2f, -630.83f, 23f),
            new Vector3(-1403.23f, -828.93f, 18.92f),
            new Vector3(527.63f, -316.1f, 43.54f),
            new Vector3(1193.93f, -1842.92f, 37.27f),
            new Vector3(-872.45f, -630.23f, 28.37f),
            new Vector3(463.07f, -2034.99f, 24.44f),
            new Vector3(-1156.38f, -1309.19f, 5.1f),
            new Vector3(2437.06f, 2857.62f, 49f),
            new Vector3(2376.3f, 2961.43f, 49.18f),
            new Vector3(-1509.03f, 2143.9f, 56.18f),
            new Vector3(-2513.91f, 2331.28f, 33.06f),
            new Vector3(2803.28f, 4386.13f, 49.35f),
            new Vector3(2633.95f, 5095.4f, 44.87f),
            new Vector3(1712.68f, 3498.46f, 36.74f),
            new Vector3(159.68f, 6550.94f, 31.91f)
        };

        public Solicitation()
        {
            this.InitInfo(Utils.GetLocation(CalloutPositions, Game.PlayerPed.Position));
            this.ShortName = "Solicitation";
            this.CalloutDescription = "911 Call : Reports of Solicitation.";
            this.ResponseCode = 3;
            this.StartDistance = 65;
        }

        public override async Task OnAccept()
        {
            this.InitBlip(75f, (BlipColor) 66, (BlipSprite) 9, 100);
            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info", "~w~ Reports of possible solicitation.");
        }

        public override void OnStart(Ped closest)
        {
            base.OnStart(closest);
            //
            // if (y <= 30)
            // {
            //     Ped ped = await this.SpawnPed((PedHash) 42647445, (Vector3) this.Location, 0.0f);
            //     this.suspect1 = ped;
            //     ped = (Ped) null;
            // }
            // else if (y > 30 && y <= 60)
            // {
            //     Ped ped = await this.SpawnPed((PedHash) 348382215, (Vector3) this.Location, 0.0f);
            //     this.suspect1 = ped;
            //     ped = (Ped) null;
            // }
            // else
            // {
            //     Ped ped = await this.SpawnPed((PedHash) 51789996, (Vector3) this.Location, 0.0f);
            //     this.suspect1 = ped;
            //     ped = (Ped) null;
            //     this.suspect1.AlwaysKeepTask = true;
            //     this.suspect1.BlockPermanentEvents = true;
            // }
        }
    }
#endif
}