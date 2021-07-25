using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("EMS Assistance", "meat", "1.0")]
    public class EMSAssistance : Callout
    {
        public EMSAssistance()
        {
            var random = new Random();
            this.InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.Position.Around(random.Next(100, 700)), false));
            this.ShortName = "EMS Assistance";
            this.CalloutDescription = "911 Call : Reports of EMS requiring assistance.";
            this.ResponseCode = 3;
            this.StartDistance = 150;
        }

        public override async Task OnAccept()
        {
            this.InitBlip(75f, (BlipColor) 66, (BlipSprite) 9, 100);
        }

        public override async void OnStart(Ped closest)
        {
            var random = new Random();
            var paramedics = new List<Ped>();
            for (var i = 0; i < 2; i++)
            {
                var paramedic = await this.SpawnPed(PedHash.Paramedic01SMM, this.Location.Around(5), 0.0f);
                paramedics.Add(paramedic);
            }

            var patients = new List<Ped>();
            for (var i = 0; i < random.Next(1, 2); i++)
            {
                var patient = await this.SpawnPed(RandomUtils.GetRandomPed(), this.Location.Around(5), 0.0f);
                patients.Add(patient);
            }

            // Spawn Ambulance
            var model = new Model("Ambulance");
            var ambulance = await this.SpawnVehicle(model, this.Location, 0.0f);
            CitizenFX.Core.Native.API.SetVehicleDoorOpen(ambulance.Handle, 0, true, true);
            CitizenFX.Core.Native.API.SetVehicleEngineOn(ambulance.Handle, true, true, true);
            CitizenFX.Core.Native.API.SetVehicleIndicatorLights(ambulance.Handle, 0, true);
            CitizenFX.Core.Native.API.SetVehicleIndicatorLights(ambulance.Handle, 1, true);
            CitizenFX.Core.Native.API.SetVehicleSiren(ambulance.Handle, true);
            CitizenFX.Core.Native.API.SetVehicleHasMutedSirens(ambulance.Handle, true);

            base.OnStart(closest);
            var scenarios = new List<Action>
            {
                () => ParamedicsFlee(paramedics, patients),
                () => ParamedicsFleeAndPatientsAttack(paramedics, patients),
                () => ParamedicsAndPatientsFight(paramedics, patients),
                () => ParamedicsFlee2(paramedics, patients),
            };

            scenarios.SelectRandom()();
        }

        private void ParamedicsFlee(List<Ped> paramedics, List<Ped> patients)
        {
            foreach (var paramedic in paramedics)
            {
                paramedic.Task.ReactAndFlee(patients.SelectRandom());
            }

            foreach (var patient in patients)
            {
                patient.Weapons.Give(WeaponHash.CombatPistol, 600, true, true);
                patient.RelationshipGroup = "HATES_PLAYER";
                patient.Task.FightAgainstHatedTargets(this.StartDistance);
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
                patient.Weapons.Give(WeaponHash.Bat, 600, true, true);
                patient.RelationshipGroup = "HATES_PLAYER";
                patient.Task.FightAgainstHatedTargets(this.StartDistance);
            }
        }

        private void ParamedicsAndPatientsFight(List<Ped> paramedics, List<Ped> patients)
        {
            foreach (var paramedic in paramedics)
            {
                paramedic.Task.FightAgainst(patients.SelectRandom());
                paramedic.RelationshipGroup = "PLAYER";
            }
            foreach (var patient in patients)
            {
                patient.RelationshipGroup = "HATES_PLAYER";
                patient.Task.FightAgainstHatedTargets(this.StartDistance);
            }
        }

        private void ParamedicsFlee2(List<Ped> paramedics, List<Ped> patients)
        {
            foreach (var paramedic in paramedics)
            {
                paramedic.Task.ReactAndFlee(patients.SelectRandom());
            }
            paramedics.SelectRandom().Kill();
            foreach (var patient in patients)
            {
                patient.Weapons.Give(WeaponHash.SweeperShotgun, 600, true, true);
                patient.RelationshipGroup = "HATES_PLAYER";
                patient.Task.FightAgainstHatedTargets(this.StartDistance);
            }
        }
    }
}