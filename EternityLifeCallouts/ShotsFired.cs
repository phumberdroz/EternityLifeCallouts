using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("Shots Fired", "meat", "1.0")]
    public class ShotsFired : Callout
    {
        public ShotsFired()
        {
            var random = new Random();
            this.InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.Position.Around(random.Next(100, 700)), false));
            this.ShortName = "Shots Fired";
            this.CalloutDescription =
                "911 Call : Shots fired. Respond accordingly, remember to respond code 1 when nearby.";
            this.ResponseCode = 3;
            this.StartDistance = 200;
        }

        public override async Task OnAccept()
        {
            this.InitBlip(75f, (BlipColor) 66, (BlipSprite) 9, 100);
            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info",
                "~w~ Caller reports multiple armed suspects shooting.");
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);

            var victim = await this.SpawnPed(RandomUtils.GetRandomPed(), this.Location.Around(2), 0.0f);
            victim.AlwaysKeepTask = true;
            victim.BlockPermanentEvents = true;
            var suspects = new List<Ped>();
            for (int i = 0; i < 2; i++)
            {
                var spawnPed = await this.SpawnPed(RandomUtils.GetRandomPed(), this.Location.Around(2), 0.0f);
                suspects.Add(spawnPed);
            }

            var scenarios = new List<Action>
            {
                () => Scenario1(victim, suspects),
                () => Scenario2(victim, suspects),
                () => Scenario3(victim, suspects),
                () => Scenario4(victim, suspects),
                () => Scenario5(victim, suspects),
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
                suspect.Weapons.Give((WeaponHash.CombatPistol), 600, true, true);
                suspect.RelationshipGroup = "HATES_PLAYER";
                suspect.Task.FightAgainstHatedTargets(this.StartDistance);
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
                suspect.Weapons.Give(WeaponHash.CombatPistol, 600, true, true);
                suspect.RelationshipGroup = "HATES_PLAYER";
                suspect.Task.FightAgainstHatedTargets(this.StartDistance);
            }
        }

        private void Scenario3(Ped victim, List<Ped> suspects)
        {
            Utils.Notify("~y~Call Update:~w~ Reports of possible gang related violence.");
            Utils.DrawSubtitle("~r~Suspect~w~: Run!", 7000);
            victim.Weapons.Give(WeaponHash.CarbineRifle, 600, true, true);
            victim.Task.FleeFrom(this.AssignedPlayers.First());

            foreach (var suspect in suspects)
            {
                suspect.Weapons.Give(WeaponHash.CombatPistol, 600, true, true);
                suspect.RelationshipGroup = "HATES_PLAYER";
                suspect.Task.FightAgainstHatedTargets(this.StartDistance);
            }
        }

        private void Scenario4(Ped victim, List<Ped> suspects)
        {
            Utils.Notify("~y~Call Update:~w~ Reports of possible gang related violence.");
            Utils.DrawSubtitle("~r~Suspect~w~: Run! Fuck you pigs!", 7000);
            foreach (var suspect in suspects)
            {
                suspect.Weapons.Give(WeaponHash.CombatPistol, 600, true, true);
                suspect.RelationshipGroup = "HATES_PLAYER";
                suspect.Task.FightAgainstHatedTargets(this.StartDistance);
            }

            victim.RelationshipGroup = "PLAYER";
            victim.Task.FleeFrom(suspects.First(), -1);
        }

        private void Scenario5(Ped victim, List<Ped> suspects)
        {
            Utils.Notify("~y~Call Update:~w~ Reports of possible gang related violence.");
            Utils.DrawSubtitle("~r~Suspect~w~: Run!", 7000);
            foreach (var suspect in suspects)
            {
                suspect.Weapons.Give(WeaponHash.CombatPistol, 600, true, true);
                suspect.RelationshipGroup = "HATES_PLAYER";
                suspect.Task.FightAgainstHatedTargets(this.StartDistance);
            }

            victim.Task.HandsUp(-1);
        }
    }
}