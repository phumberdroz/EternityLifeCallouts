using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using EternityLifeCallouts.Extensions;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("Neighbor Dispute", "meat", "1.0")]
    public class NeighborDispute : Callout
    {
        private static readonly Vector3[] CalloutPositions =
        {
            new Vector3(-145.45f, -2040.21f, 22.98f),
            new Vector3(-138.88f, -1983.07f, 22.81f),
            new Vector3(-491.03f, -688.61f, 33.21f),
            new Vector3(-829.62f, -114.3f, 37.58f),
            new Vector3(-527.13f, -1285.39f, 26.05f),
            new Vector3(424.68f, -624.03f, 28.5f),
            new Vector3(-245.31f, -339.24f, 29.98f),
            new Vector3(-199.18f, -1027.88f, 29.29f),
            new Vector3(-245.31f, -339.24f, 29.98f),
            new Vector3(-1170.01f, 381.69f, 71.53f),
            new Vector3(-1117.92f, 442.08f, 75.29f),
            new Vector3(-1301.76f, 502.8f, 97.56f),
            new Vector3(-1331.57f, 497.48f, 102.45f),
            new Vector3(-1396.54f, 613.08f, 131.56f),
            new Vector3(-1296.56f, 591.51f, 129.65f),
            new Vector3(37.5f, 430.65f, 142.92f),
            new Vector3(-2.26f, 512.03f, 170.63f),
            new Vector3(-182.23f, 651.86f, 199.64f),
            new Vector3(-74.4f, 816.94f, 227.75f),
            new Vector3(-1014.11f, 832.48f, 172.41f),
            new Vector3(-2025.79f, 354.88f, 94.48f),
            new Vector3(-1945.54f, 144.02f, 84.65f),
            new Vector3(-1853.32f, 654.33f, 130.29f),
            new Vector3(-1094.54f, -1625.16f, 4.73f),
            new Vector3(-1042.46f, -1119.72f, 2.16f),
            new Vector3(-1018.82f, -1165.07f, 2.16f),
            new Vector3(-990.54f, -872.55f, 2.15f),
            new Vector3(-969.93f, -926.96f, 2.15f),
            new Vector3(-1138.01f, -1063.8f, 2.15f),
            new Vector3(379.27f, -2070.71f, 21.18f),
            new Vector3(348.64f, -2006.46f, 22.51f),
            new Vector3(143.99f, -1922.64f, 21.15f),
            new Vector3(61.36f, -1956.56f, 20.96f),
            new Vector3(22f, -1911.59f, 22.17f),
            new Vector3(-24.48f, -1873.43f, 25.13f),
            new Vector3(-214.16f, -1586.84f, 34.87f),
            new Vector3(-72.8f, -1438.92f, 32.06f),
            new Vector3(-44.98f, -1426.69f, 32.06f),
            new Vector3(-0.51f, -1422.19f, 30.54f),
            new Vector3(15.69f, -1427.48f, 30.54f),
            new Vector3(1367.79f, -1740.97f, 65.2f),
            new Vector3(1322.36f, -1753.53f, 55.42f),
            new Vector3(1283.28f, -1759.52f, 52.03f),
            new Vector3(1255.24f, -1770.89f, 49.26f),
            new Vector3(1262.47f, -1710.09f, 54.66f),
            new Vector3(1258.42f, -1626.59f, 53.31f),
            new Vector3(1275.89f, -1614.87f, 54.22f),
            new Vector3(1392.97f, -1499.99f, 58.1f),
            new Vector3(1404.45f, -604.56f, 74.43f),
            new Vector3(1377.95f, -536.61f, 74.34f),
            new Vector3(1355.78f, -529.99f, 73.81f),
            new Vector3(1316.14f, -603.21f, 72.91f),
            new Vector3(1252.03f, -699.9f, 64.63f),
            new Vector3(1235.47f, -691.79f, 60.9f),
            new Vector3(1210.41f, -604.4f, 67.69f),
            new Vector3(1216.73f, -576.36f, 68.91f),
            new Vector3(1036.95f, -470.51f, 63.9f),
            new Vector3(962.49f, -438.39f, 62.6f),
            new Vector3(889.35f, -478.49f, 59.02f),
            new Vector3(824.21f, -538.17f, 56.42f),
            new Vector3(889.9f, -631.37f, 58.14f),
            new Vector3(958.76f, -711.55f, 58.17f),
            new Vector3(51.75f, -45.87f, 69.39f),
            new Vector3(57.58f, -77.98f, 62.54f),
            new Vector3(120.85f, -127.56f, 54.84f),
            new Vector3(468.37f, 2586.48f, 43.27f),
            new Vector3(387.84f, 2639.21f, 44.5f),
            new Vector3(1541.12f, 3588.32f, 38.77f),
            new Vector3(1590.6f, 3569.08f, 35.3f),
            new Vector3(1660.58f, 3743.19f, 34.51f),
            new Vector3(1722.24f, 3864.45f, 34.74f),
            new Vector3(1929.7f, 3820.6f, 31.99f),
            new Vector3(1650.17f, 4740.97f, 42.2f),
            new Vector3(1640.7f, 4771.91f, 42.11f),
            new Vector3(3734.03f, 4529.47f, 21.31f),
            new Vector3(3318.96f, 5191.75f, 18.42f),
            new Vector3(-29.15f, 6572.48f, 31.47f),
            new Vector3(7.21f, 6601.68f, 31.53f),
            new Vector3(-1.94f, 6581.26f, 32.74f),
            new Vector3(-153.97f, 6417.62f, 31.87f),
            new Vector3(-182.21f, 6397.98f, 31.88f),
            new Vector3(-222.03f, 6361.49f, 31.49f),
            new Vector3(-239.46f, 6354.47f, 31.49f),
            new Vector3(-289.29f, 6315.76f, 32.38f),
            new Vector3(-379.76f, 6241.7f, 31.49f),
            new Vector3(-370.22f, 6221.99f, 31.49f),
            new Vector3(2182.05f, 5586.42f, 54.28f),
            new Vector3(-3238.89f, 1108.59f, 2.6f),
            new Vector3(-3061.94f, 528.13f, 2.36f)
        };

        public NeighborDispute()
        {
            InitInfo(Utils.GetLocation(CalloutPositions, Game.PlayerPed.Position));
            ShortName = "Neighbor Dispute";
            CalloutDescription = "911 Call : Reports of neighbors in a dispute.";
            ResponseCode = 2;
            StartDistance = 150;
        }

        public override async Task OnAccept()
        {
            InitBlip();
            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info",
                "~w~Head to the scene and resolve the issue.");
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            var ped1 = await SpawnPed(RandomUtils.GetRandomPed(), Location);

            var ped2 = await SpawnPed(RandomUtils.GetRandomPed(), Location.Around(2));

            ped1.AlwaysKeepTask = true;
            ped1.BlockPermanentEvents = true;
            ped2.AlwaysKeepTask = true;
            ped2.BlockPermanentEvents = true;

            var scenarios = new List<Action>
            {
                () => Scenario1(ped1, ped2),
                () => Scenario2(ped1, ped2),
                () => Scenario3(ped1, ped2),
                () => Scenario4(ped1, ped2),
                () => Scenario5(ped1, ped2)
            };

            scenarios.SelectRandom()();
            // this.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "Callout Tip:", "~w~Making Contact",
            // "~w~Press ~y~Y ~w~when near the ped to interact.");
            // this.Tick += new Func<Task>(this.OnTick);
            // Todo add conversations
        }

        private void Scenario1(Ped neighbor1, Ped neighbor2)
        {
            neighbor1.Task.TurnTo(neighbor2);
            neighbor2.Task.TurnTo(neighbor1);
        }

        private void Scenario2(Ped neighbor1, Ped neighbor2)
        {
            neighbor1.Task.FightAgainst(neighbor2);
            neighbor2.Task.FightAgainst(neighbor1);
            neighbor1.RelationshipGroup = "AMBIENT_GANG_WEICHENG";
            neighbor2.RelationshipGroup = "AMBIENT_GANG_WEICHENG";
        }

        private void Scenario3(Ped neighbor1, Ped neighbor2)
        {
            neighbor1.Task.ReactAndFlee(neighbor2);
            neighbor2.Task.FightAgainst(neighbor1);
            Utils.DrawSubtitle("~g~Caller:~w~ Help, theyre trying to kill me!", 6000);
        }

        private void Scenario4(Ped neighbor1, Ped neighbor2)
        {
            var peds = new[] {neighbor1, neighbor2};
            foreach (var ped in peds)
            {
                ped.GiveRandomHandGun();
                ped.RelationshipGroup = "HATES_PLAYER";
                ped.Task.FightAgainstHatedTargets(StartDistance);
            }
        }

        private void Scenario5(Ped neighbor1, Ped neighbor2)
        {
            neighbor1.Kill();
            neighbor2.GiveRandomWeapon();
            neighbor2.Task.AimAt(AssignedPlayers.First(), -1);
        }
    }
}