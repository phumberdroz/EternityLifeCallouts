using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using EternityLifeCallouts.Extensions;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("Armed Robbery", "meat", "1.0")]
    public class ArmedRobbery : Callout
    {
        private static readonly string[] ShortNames =
        {
            "Unknown Trouble",
            "Individuals with Firearms",
            "911 Hangup",
            "Armed Robbery"
        };

        private static readonly Vector3[] CalloutPositions =
        {
            new Vector3(299.49f, -1000.71f, 29.28f),
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
            new Vector3(-105.67f, 1910.52f, 196.89f),
            new Vector3(1245.14f, 1862.83f, 79.35f),
            new Vector3(1754.8f, 3323.72f, 41.21f),
            new Vector3(1344.1f, 4386.62f, 44.34f),
            new Vector3(1533.31f, 6327.17f, 24.25f),
            new Vector3(-574.78f, 5256.4f, 70.46f),
            new Vector3(1710.73f, -1564.98f, 112.62f),
            new Vector3(691.3f, -721.82f, 25.66f),
            new Vector3(-486.38f, -451.56f, 34.2f),
            new Vector3(-1097.39f, -461.57f, 35.34f),
            new Vector3(133.99f, -1230.16f, 29.51f)
        };

        public ArmedRobbery()
        {
            var calloutCord = Utils.GetLocation(CalloutPositions, Game.PlayerPed.Position);
            this.InitInfo(calloutCord);
            this.ShortName = ShortNames.SelectRandom();
            this.ResponseCode = 3;
            this.StartDistance = 150;
        }

        public override async Task OnAccept()
        {
            this.InitBlip(75f, (BlipColor) 66, (BlipSprite) 9, 100);
            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info",
                "~w~Reports of an armed robbery taking place.");
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            var suspects = new List<Ped>();
            for (int i = 0; i < 3; i++)
            {
                var spawnedPed = await this.SpawnPed(RandomUtils.GetRandomPed(), this.Location.Around(5));
                suspects.Add(spawnedPed);
                // Todo add illegal items like Stolen CreditCards and other things
            }

            var victim = await this.SpawnPed(RandomUtils.GetRandomPed(), this.Location.Around(5));
            victim.AlwaysKeepTask = true;
            victim.BlockPermanentEvents = true;

            var functions = new List<Action>
            {
                () => VictimKilled(victim, suspects),
                () => SuspectsKilled(victim, suspects),
                () => VictimSurvived(victim, suspects),
            };

            functions.SelectRandom()();
        }

        private void VictimKilled(Ped victim, List<Ped> suspects)
        {
            API.SetEntityHealth(victim.Handle, 10);
            foreach (var suspect in suspects)
            {
                suspect.GiveRandomHandGun();
                suspect.RelationshipGroup = "AMBIENT_GANG_WEICHENG";
                suspect.Task.ShootAt(victim);
            }
            API.Wait(5000);
            foreach (var suspect in suspects)
            {
                suspect.Task.Wait(-1);
                suspect.MakeAggressiveAgainstPlayers();
            }
        }

        private void SuspectsKilled(Ped victim, List<Ped> suspects)
        {
    
            victim.GiveRandomHandGun();
            victim.Task.ShootAt(suspects.SelectRandom());
            API.Wait(5000);
            foreach (var suspect in suspects)
            {
                suspect.Kill();
            }
            victim.Task.AimAt(suspects.SelectRandom(), -1);
        }
        private void VictimSurvived(Ped victim, List<Ped> suspects)
        {
            foreach (var suspect in suspects)
            {
                suspect.MakeAggressiveAgainstPlayers();
            }
            victim.Task.HandsUp(-1);
        }
    }
}