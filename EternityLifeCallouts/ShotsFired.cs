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
    [CalloutProperties("Shots Fired", "meat", "1.0")]
    public class ShotsFired : Callout
    {
        public ShotsFired()
        {
            InitInfo(World.GetNextPositionOnStreet(
                Game.PlayerPed.Position.Around(RandomUtils.GetRandomNumber(100, 700))));
            ShortName = "Shots Fired";
            CalloutDescription = "911 Call : Shots fired. Respond accordingly, remember to respond code 1 when nearby.";
            ResponseCode = 3;
            StartDistance = 200;
        }

        public override async Task OnAccept()
        {
            InitBlip();
            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info",
                "~w~ Caller reports multiple armed suspects shooting.");
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);

            var victim = await SpawnPed(RandomUtils.GetRandomPed(), Location.Around(2));
            victim.AlwaysKeepTask = true;
            victim.BlockPermanentEvents = true;
            var suspects = new List<Ped>();
            for (var i = 0; i < 2; i++)
            {
                var spawnPed = await SpawnPed(RandomUtils.GetRandomPed(), Location.Around(2));
                suspects.Add(spawnPed);
            }

            var scenarios = new List<Action>
            {
                () => Scenario1(victim, suspects),
                () => Scenario2(victim, suspects),
                () => Scenario3(victim, suspects),
                () => Scenario4(victim, suspects),
                () => Scenario5(victim, suspects)
            };

            scenarios.SelectRandom()();
        }

        private void Scenario1(Ped victim, List<Ped> suspects)
        {
            Utils.DrawSubtitle("~g~Victim~w~: Help!", 5000);
            victim.Task.ReactAndFlee(suspects.SelectRandom());
            victim.RelationshipGroup = "PLAYER";
            foreach (var suspect in suspects)
            {
                suspect.GiveRandomWeapon();
                suspect.RelationshipGroup = "HATES_PLAYER";
                suspect.Task.FightAgainstHatedTargets(StartDistance);
            }
        }

        private void Scenario2(Ped victim, List<Ped> suspects)
        {
            Utils.Notify("~y~Call Update:~w~ Reports of a possible ambush.");
            Utils.DrawSubtitle("~r~Suspect~w~: Fuck this, ape together strong!", 7000);
            victim.AlwaysKeepTask = false;
            victim.BlockPermanentEvents = false;
            suspects.Add(victim);
            foreach (var suspect in suspects)
            {
                suspect.GiveRandomHandGun();
                suspect.RelationshipGroup = "HATES_PLAYER";
                suspect.Task.FightAgainstHatedTargets(StartDistance);
            }
        }

        private void Scenario3(Ped victim, List<Ped> suspects)
        {
            Utils.Notify("~y~Call Update:~w~ Reports of possible gang related violence.");
            Utils.DrawSubtitle("~r~Suspect~w~: Run!", 7000);
            victim.Weapons.Give(Weapons.AssaultRifles.SelectRandom(), 600, true, true);
            victim.Task.FleeFrom(AssignedPlayers.First());

            foreach (var suspect in suspects)
            {
                suspect.GiveRandomHandGun();
                suspect.RelationshipGroup = "HATES_PLAYER";
                suspect.Task.FightAgainstHatedTargets(StartDistance);
            }
        }

        private void Scenario4(Ped victim, List<Ped> suspects)
        {
            Utils.Notify("~y~Call Update:~w~ Reports of possible gang related violence.");
            Utils.DrawSubtitle("~r~Suspect~w~: Run! Fuck you pigs!", 7000);
            foreach (var suspect in suspects)
            {
                suspect.GiveRandomWeapon();
                suspect.RelationshipGroup = "HATES_PLAYER";
                suspect.Task.FightAgainstHatedTargets(StartDistance);
            }

            victim.RelationshipGroup = "PLAYER";
            victim.Task.FleeFrom(suspects.First());
        }

        private void Scenario5(Ped victim, List<Ped> suspects)
        {
            Utils.Notify("~y~Call Update:~w~ Reports of possible gang related violence.");
            Utils.DrawSubtitle("~r~Suspect~w~: Run!", 7000);
            foreach (var suspect in suspects)
            {
                suspect.GiveRandomWeapon();
                suspect.RelationshipGroup = "HATES_PLAYER";
                suspect.Task.FightAgainstHatedTargets(StartDistance);
            }

            victim.Task.HandsUp(-1);
        }
    }
}