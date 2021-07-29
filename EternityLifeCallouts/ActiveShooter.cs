using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using EternityLifeCallouts.Extensions;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("Active Shooter", "meat", "1.0")]
    public class ActiveShooter : Callout
    {
        private static readonly string[] ShortNames =
        {
            "Unknown Trouble",
            "Individuals with Firearms",
            "911 Hangup",
            "Active Shooter"
        };

        private static readonly Vector3[] CalloutPositions =
        {
            new Vector3(-1727.32f, 163.04f, 64.37f),
            new Vector3(-1605.71f, 226.09f, 59.34f),
            new Vector3(-2266.98f, 329.86f, 174.6f),
            new Vector3(-1055.66f, -2553.19f, 20.21f),
            new Vector3(1140.54f, -646.04f, 56.74f),
            new Vector3(506.71f, 2683.23f, 42.79f),
            new Vector3(1356.22f, 4368.05f, 44.35f),
            new Vector3(475.1f, 6452.12f, 30.14f),
            new Vector3(174.57f, -677.99f, 43.14f),
            new Vector3(-100.65f, -433.67f, 36.14f),
            new Vector3(-922.33f, -782.89f, 15.89f),
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
            new Vector3(-305.87f, -935.58f, 31.08f),
            new Vector3(856.06f, -2307.02f, 30.35f),
            new Vector3(127.07f, -2231.99f, 6.03f),
            new Vector3(1055.83f, -3038.75f, 5.9f),
            new Vector3(-1018.67f, -496.64f, 37.14f),
            new Vector3(452.73f, -2170.02f, 5.92f),
            new Vector3(-183.69f, -1325.05f, 31.27f),
            new Vector3(-69.38f, -2429.18f, 6f),
            new Vector3(-421.89f, -2786.53f, 6f),
            new Vector3(1168.33f, -2973.68f, 5.9f),
            new Vector3(-1018.71f, -2197.1f, 8.98f),
            new Vector3(-701.06f, -2232.3f, 5.84f),
            new Vector3(1168.96f, -1551.38f, 34.69f),
            new Vector3(853.16f, -953.68f, 26.28f),
            new Vector3(-1416.86f, -253.49f, 46.37f),
            new Vector3(-1279.88f, 305.43f, 64.98f),
            new Vector3(581.08f, 130.43f, 98.04f),
            new Vector3(2728.29f, 1652.4f, 24.56f),
            new Vector3(587.06f, 2793.6f, 42.1f),
            new Vector3(-2559.92f, 2320.12f, 33.06f),
            new Vector3(-379.76f, 6241.7f, 31.49f),
            new Vector3(-370.22f, 6221.99f, 31.49f),
            new Vector3(2182.05f, 5586.42f, 54.28f),
            new Vector3(-3238.89f, 1108.59f, 2.6f),
            new Vector3(-1677.79f, -1118.93f, 13.15f)
        };

        public ActiveShooter()
        {
            this.InitInfo(Utils.GetLocation(CalloutPositions, Game.PlayerPed.Position));
            this.ShortName = ShortNames.SelectRandom();
            this.ResponseCode = 3;
            this.StartDistance = 150;
        }

        public override async Task OnAccept()
        {
            this.InitBlip(75f, (BlipColor) 66, (BlipSprite) 9, 100);
            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info",
                "~w~Regroup with other units and head to the scene.");
        }

        public override async void OnStart(Ped player)
        {
            base.OnStart(player);
            for (int i = 0; i < 5; i++)
            {
                var spawnedPed = await this.SpawnPed(RandomUtils.GetRandomPed(), this.Location.Around(5), 0.0f);
                // Todo add weapon variety 
                spawnedPed.Weapons.Give(WeaponHash.HeavySniper, 600, true, true);
                spawnedPed.MakeAggressiveAgainstPlayers();
                spawnedPed.Task.WanderAround();
            }
        }
    }
}