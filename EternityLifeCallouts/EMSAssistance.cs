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
    [CalloutProperties("EMS Assistance", "meat", "1.0")]
    public class EMSAssistance : Callout
    {
        public EMSAssistance()
        {
            InitInfo(World.GetNextPositionOnStreet(
                Game.PlayerPed.Position.Around(RandomUtils.GetRandomNumber(100, 700))));
            ShortName = "EMS Assistance";
            CalloutDescription = "911 Call : Reports of EMS requiring assistance.";
            ResponseCode = 3;
            StartDistance = 150;
        }

        public override async Task OnAccept()
        {
            InitBlip();
        }

        public override async void OnStart(Ped closest)
        {
            var paramedics = new List<Ped>();
            for (var i = 0; i < 2; i++)
            {
                var paramedic = await SpawnPed(PedHash.Paramedic01SMM, Location.Around(5));
                paramedics.Add(paramedic);
            }

            var patients = new List<Ped>();
            for (var i = 0; i < RandomUtils.GetRandomNumber(1, 3); i++)
            {
                var patient = await SpawnPed(RandomUtils.GetRandomPed(), Location.Around(5));
                patients.Add(patient);
            }

            // Spawn Ambulance
            var model = new Model("Ambulance");
            var ambulance = await SpawnVehicle(model, Location);
            API.SetVehicleDoorOpen(ambulance.Handle, 0, true, true);
            API.SetVehicleEngineOn(ambulance.Handle, true, true, true);
            API.SetVehicleIndicatorLights(ambulance.Handle, 0, true);
            API.SetVehicleIndicatorLights(ambulance.Handle, 1, true);
            API.SetVehicleSiren(ambulance.Handle, true);
            API.SetVehicleHasMutedSirens(ambulance.Handle, true);

            base.OnStart(closest);
            var scenarios = new List<Action>
            {
                () => ParamedicsFlee(paramedics, patients),
                () => ParamedicsFleeAndPatientsAttack(paramedics, patients),
                () => ParamedicsAndPatientsFight(paramedics, patients),
                () => ParamedicsFlee2(paramedics, patients)
            };

            scenarios.SelectRandom()();
        }

        private void ParamedicsFlee(List<Ped> paramedics, List<Ped> patients)
        {
            foreach (var paramedic in paramedics) paramedic.Task.ReactAndFlee(patients.SelectRandom());

            foreach (var patient in patients)
            {
                patient.GiveRandomHandGun();
                patient.MakeAggressiveAgainstPlayers();
            }
        }

        private void ParamedicsFleeAndPatientsAttack(List<Ped> paramedics, List<Ped> patients)
        {
            foreach (var paramedic in paramedics)
            {
                paramedic.Task.ReactAndFlee(patients.SelectRandom());
                paramedic.RelationshipGroup = "PLAYER";
            }

            foreach (var patient in patients)
            {
                patient.Weapons.Give(Weapons.MeleeWeapons.SelectRandom(), 600, true, true);
                patient.MakeAggressiveAgainstPlayers();
            }
        }

        private void ParamedicsAndPatientsFight(List<Ped> paramedics, List<Ped> patients)
        {
            foreach (var paramedic in paramedics)
            {
                paramedic.Task.FightAgainst(patients.SelectRandom());
                paramedic.RelationshipGroup = "PLAYER";
            }

            foreach (var patient in patients) patient.MakeAggressiveAgainstPlayers();
        }

        private void ParamedicsFlee2(List<Ped> paramedics, List<Ped> patients)
        {
            foreach (var paramedic in paramedics) paramedic.Task.ReactAndFlee(patients.SelectRandom());
            paramedics.SelectRandom().Kill();
            foreach (var patient in patients)
            {
                patient.Weapons.Give(Weapons.ShotGuns.SelectRandom(), 600, true, true);
                patient.MakeAggressiveAgainstPlayers();
            }
        }
    }
}