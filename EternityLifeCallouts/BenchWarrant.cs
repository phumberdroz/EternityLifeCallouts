using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using EternityLifeCallouts.Extensions;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("Bench Warrent", "meat", "1.0")]
    public class BenchWarrant : Callout
    {
        private static readonly string[] ShortNames =
        {
            "Bench Warrant : Assault",
            "Bench Warrant : Child Support",
            "Bench Warrant : Domestic Violence",
            "Bench Warrant : Eluding",
            "Bench Warrant : Evading",
            "Bench Warrant : Homicide",
            "Bench Warrant : Rapist",
            "Bench Warrant : Tax Evasion",
            "Bench Warrant : Trespassing"
        };

        private static readonly WeaponHash[] AggressiveWeapons =
        {
            WeaponHash.MicroSMG,
            WeaponHash.SweeperShotgun,
            WeaponHash.CombatPistol
        };

        private Ped suspect;

        public BenchWarrant()
        {
            InitInfo(World.GetNextPositionOnStreet(
                Game.PlayerPed.Position.Around(RandomUtils.GetRandomNumber(250, 700))));
            ShortName = ShortNames.SelectRandom();
            CalloutDescription =
                "911 Call : Tip line hit confirms sighting of wanted individual. More Information will follow!";
            ResponseCode = 2;
            StartDistance = 180;
        }

        public override async Task OnAccept()
        {
            InitBlip();
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            var handle = API.RegisterPedheadshot(suspect.Handle);
            while (!API.IsPedheadshotReady(handle) ||
                   !API.IsPedheadshotValid(handle))
                await BaseScript.Delay(800);

            var data = await suspect.GetData();
            data.Warrant = ShortName;
            suspect.SetData(data);
            suspect.AttachBlip();
            var txd = API.GetPedheadshotTxdString(handle);
            ShowNetworkedNotification("SAN ANDREAD COURT : OFFICIAL BENCH WARRANT", "commonmenu", "mp_alerttriangle",
                "911 Dispatch:", "~y~Additional Info", StartDistance);
            ShowNetworkedNotification(
                "Name: ~y~" + data.FirstName + " " + data.LastName + "~w~. Suspects most recent mugshot attached.", txd,
                txd, "911 Dispatch:", "~y~Additional Info", StartDistance
            );
            var scenarios = new List<Action>
            {
                () => Aggressive(),
                () => NonAggressiveHandsUp(),
                () => NonAggressiveFlee()
            };

            scenarios.SelectRandom()();
        }

        private void NonAggressiveFlee()
        {
            var closestPlayer = this.GetClosestPlayer(suspect.Position);
            suspect.Task.FleeFrom(closestPlayer);
        }

        private void NonAggressiveHandsUp()
        {
            suspect.Task.WanderAround(Location, 50);
            Tick += OnTickNonAggressiveHandsUp;
        }

        public async Task OnTickNonAggressiveHandsUp()
        {
            var closestPlayer = this.GetClosestPlayer(suspect.Position);
            if (closestPlayer.Position.DistanceToSquared(suspect.Position) <= 50.0)
            {
                suspect.Task.HandsUp(-1);
                Tick -= OnTickNonAggressiveHandsUp;
            }
        }

        private void Aggressive()
        {
            suspect.Weapons.Give(AggressiveWeapons.SelectRandom(), 600, false, true);
            suspect.Task.WanderAround(Location, 50);
            Debug.WriteLine($"Are they aggressive {suspect.GetRelationshipWithPed(Game.PlayerPed)}");
            Tick += OnTickAggressive;
        }

        public async Task OnTickAggressive()
        {
            var closestPlayer = this.GetClosestPlayer(suspect.Position);
            if (closestPlayer.Position.DistanceToSquared(suspect.Position) <= 20.0 &&
                closestPlayer.GetRelationshipWithPed(suspect) != Relationship.Hate)
            {
                ShowDialog("~r~Suspect~w~: Fuck you I'm not going back to jail!", 5000, 25f);
                suspect.MakeAggressiveAgainstPlayers();
                suspect.Task.FightAgainst(closestPlayer, 1);
                Tick -= OnTickAggressive;
            }
        }
    }
}