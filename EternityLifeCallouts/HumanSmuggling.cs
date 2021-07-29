using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("Human Trafficking", "meat", "1.0")]
    public class HumanSmuggling : Callout
    {
        public HumanSmuggling()
        {
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.Position.Around(RandomUtils.GetRandomNumber(100, 700)), false));
            ShortName = "Human Trafficking";
            CalloutDescription = "911 Call : Reports of human trafficking.";
            ResponseCode = 3;
            StartDistance = 240;
        }
        private static readonly PedHash[][] SkinCategories =
        {
            new[] {PedHash.MexGoon01GMY, PedHash.MexGoon02GMY, PedHash.MexGoon03GMY},
            new[] {PedHash.Misty01, PedHash.Stripper01SFY, PedHash.Stripper02SFY,}
        };

        public override async Task OnAccept()
        {
            InitBlip(150f, (BlipColor) 66, (BlipSprite) 9, 100);
        }

        public override async void OnStart(Ped closest)
        {
            var ped1 = await this.SpawnPed(PedHash.Mani, (Vector3) this.Location, 0.0f);
            var suspects = new List<Ped>();
            foreach (var pedHash in SkinCategories.SelectRandom())
            {
                var spawnedPed = await this.SpawnPed(pedHash, this.Location, 0.0f);
                suspects.Add(spawnedPed);
            }

            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info",
                "Search the area for the Police Impersonator and conduct a traffic stop.");
            var bicyclesModelNames = new[]
            {
                "paradise", "minivan", "rumpo", "Pony"
            };
            var model = new Model(bicyclesModelNames.SelectRandom());
            var vehicle = await this.SpawnVehicle(model, this.Location, 0.0f);
            ped1.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            foreach (var suspect in suspects)
            {
                suspect.SetIntoVehicle(vehicle, VehicleSeat.Any);
            }
            ped1.Task.CruiseWithVehicle(vehicle, 1f, 1);
            ped1.AlwaysKeepTask = true;
            ped1.BlockPermanentEvents = true;
            vehicle.IsPersistent = true;
            base.OnStart(closest);
            ped1.Task.CruiseWithVehicle(vehicle, 30f, 1);
            ped1.Weapons.Give((WeaponHash.CombatPistol), 600, true, true);
            // this.Tick += new Func<Task>(this.OnTick);
            // Todo add scenarios 
        }
    }
    //     public async Task OnTick()
    // {
    // await BaseScript.Delay(this.random.Next(900, 1000));
    // if (this.FirstTickTask)
    // return;
    //
    // Vector3 position = ((Entity) this.suspect1).Position;
    //     if ((double) ((Vector3) ref position).DistanceToSquared(((Entity) Game.PlayerPed).Position) <= 20.0)
    // {
    //     if (this.DangerLevel > 1)
    //     {
    //         int x = this.random.Next(1, 101);
    //         if (x <= 70)
    //         {
    //             this.suspect1.Task.CruiseWithVehicle(this.suspectCar, 90f, 262710);
    //             this.suspect2.Task.LeaveVehicle((LeaveVehicleFlags) 0);
    //             this.suspect2.Weapons.Give((WeaponHash) 487013001, 600, true, true);
    //             this.suspect2.Task.FightAgainst(Game.PlayerPed);
    //             this.suspect3.Weapons.Give((WeaponHash) 1593441988, 600, true, true);
    //             this.suspect3.Task.VehicleShootAtPed(Game.PlayerPed);
    //         }
    //         else if (x > 70 && x <= 75)
    //         {
    //             this.suspect1.Task.CruiseWithVehicle(this.suspectCar, 90f, 262710);
    //             this.suspect2.Task.LeaveVehicle((LeaveVehicleFlags) 0);
    //             this.suspect2.Weapons.Give((WeaponHash) 487013001, 600, true, true);
    //             this.suspect2.Task.FightAgainst(Game.PlayerPed);
    //             this.suspect3.Task.LeaveVehicle((LeaveVehicleFlags) 0);
    //             this.suspect3.Task.ReactAndFlee(Game.PlayerPed);
    //             this.suspect4.Task.LeaveVehicle((LeaveVehicleFlags) 0);
    //             this.suspect4.Task.ReactAndFlee(Game.PlayerPed);
    //         }
    //         else if (x > 75 && x <= 80)
    //         {
    //             this.suspect1.Task.CruiseWithVehicle(this.suspectCar, 90f, 262710);
    //             this.suspect3.Weapons.Give((WeaponHash) 1593441988, 600, true, true);
    //             this.suspect3.Task.VehicleShootAtPed(Game.PlayerPed);
    //         }
    //         else
    //             this.suspect1.Task.CruiseWithVehicle(this.suspectCar, 90f, 262710);
    //     }
    //     else
    //     {
    //         int x = this.random.Next(1, 101);
    //         if (x > 30)
    //         {
    //             if (x > 30 && x <= 40)
    //             {
    //                 this.suspect1.Task.CruiseWithVehicle(this.suspectCar, 90f, 262710);
    //                 this.suspect2.Task.LeaveVehicle((LeaveVehicleFlags) 0);
    //                 this.suspect2.Task.ReactAndFlee(Game.PlayerPed);
    //                 this.suspect3.Task.LeaveVehicle((LeaveVehicleFlags) 0);
    //                 this.suspect3.Task.ReactAndFlee(Game.PlayerPed);
    //                 this.suspect4.Task.LeaveVehicle((LeaveVehicleFlags) 0);
    //                 this.suspect4.Task.ReactAndFlee(Game.PlayerPed);
    //             }
    //             else
    //                 this.suspect1.Task.CruiseWithVehicle(this.suspectCar, 90f, 262710);
    //         }
    //     }
    //
    //     this.FirstTickTask = true;
    // }
}