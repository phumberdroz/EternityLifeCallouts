using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using EternityLifeCallouts.Extensions;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("Attempted Kidnapping", "meat", "1.0")]
    public class AttemptedKidnapping : Callout
    {
        public AttemptedKidnapping()
        {
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.Position.Around(RandomUtils.GetRandomNumber(100, 700)), false));
            ShortName = "Attempted Kidnapping";
            CalloutDescription = "911 Call : Reports of attempted kidnapping.";
            ResponseCode = 3;
            StartDistance = 150;
        }

        public override async Task OnAccept()
        {
            InitBlip(75f, (BlipColor) 66, (BlipSprite) 9, 100);
            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info",
                "~w~Reports of a strange man chasing a woman.");
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            var victimPedHashes = new PedHash[]
            {
                PedHash.Beach01AFM,
                PedHash.Ballas01GFY,
                PedHash.Business01AFY,
            };
            var suspectPedHashes = new PedHash[]
            {
                PedHash.ChiCold01GMM,
                PedHash.Acult01AMY,
                PedHash.Acult01AMM,
            };
            var victim = await this.SpawnPed(victimPedHashes.SelectRandom(), this.Location.Around(5));
            var suspect = await this.SpawnPed(suspectPedHashes.SelectRandom(), this.Location.Around(5));

            var functions = new List<Action>
            {
                () => Scenario1(victim, suspect),
                () => Scenario2(victim, suspect),
                () => Scenario3(victim, suspect),
            };
            functions.SelectRandom()();
        }

        private void Scenario1(Ped victim, Ped suspect)
        {
            victim.Task.ReactAndFlee(suspect);
            suspect.Task.FightAgainst(victim);
            // Todo improve once suspect is killed or arrested make woman stand still or make her come back
        }

        private void Scenario2(Ped victim, Ped suspect)
        {
            victim.Weapons.Give(Weapons.MeleeWeapons.SelectRandom(), 600, true, true);
            victim.MakeAggressiveAgainstPlayers();

            suspect.Weapons.Give(WeaponHash.CombatPistol, 600, true, true);
            suspect.MakeAggressiveAgainstPlayers();
        }

        private void Scenario3(Ped victim, Ped suspect)
        {
            suspect.Weapons.Give(Weapons.ShotGuns.SelectRandom(), 600, true, true);
            victim.Task.ReactAndFlee(suspect);
            suspect.MakeAggressiveAgainstPlayers();
        }
    }
}