using System.Linq;
using CitizenFX.Core;

namespace EternityLifeCallouts.Extensions
{
    public class Weapons
    {
        
        public static readonly WeaponHash[] MeleeWeapons =
        {
            WeaponHash.Dagger,
            WeaponHash.Bat,
            WeaponHash.Bottle,
            WeaponHash.Crowbar,
            WeaponHash.Hammer,
            WeaponHash.Hatchet,
            WeaponHash.Knife,
            WeaponHash.Machete,
            WeaponHash.SwitchBlade,
            WeaponHash.BattleAxe,
            WeaponHash.StoneHatchet,
        };

        public static readonly WeaponHash[] Handguns =
        {
            WeaponHash.Pistol,
            WeaponHash.PistolMk2,
            WeaponHash.CombatPistol,
            WeaponHash.APPistol,
            WeaponHash.SNSPistol,
            WeaponHash.SNSPistolMk2,
            WeaponHash.HeavyPistol,
            WeaponHash.VintagePistol,
            WeaponHash.MarksmanPistol,
            WeaponHash.Revolver,
            WeaponHash.RevolverMk2,
            WeaponHash.DoubleAction,
            WeaponHash.Pistol50,
        };

        public static readonly WeaponHash[] SubMachineGuns =
        {
            WeaponHash.MicroSMG,
            WeaponHash.SMG,
            WeaponHash.SMGMk2,
            WeaponHash.AssaultSMG,
            WeaponHash.CombatPDW,
            WeaponHash.MachinePistol,
            WeaponHash.MiniSMG,
        };

        public static readonly WeaponHash[] ShotGuns =
        {
            WeaponHash.PumpShotgun,
            WeaponHash.PumpShotgunMk2,
            WeaponHash.SawnOffShotgun,
            WeaponHash.AssaultShotgun,
            WeaponHash.BullpupShotgun,
            WeaponHash.HeavyShotgun,
            WeaponHash.DoubleBarrelShotgun,
            WeaponHash.SweeperShotgun,
        };

        public static readonly WeaponHash[] AssaultRifles =
        {
            WeaponHash.AssaultRifle,
            WeaponHash.AssaultRifleMk2,
            WeaponHash.CarbineRifle,
            WeaponHash.CarbineRifleMk2,
            WeaponHash.AdvancedRifle,
            WeaponHash.SpecialCarbine,
            WeaponHash.SpecialCarbineMk2,
            WeaponHash.BullpupRifle,
            WeaponHash.BullpupRifleMk2,
            WeaponHash.CompactRifle,
        };

        public static readonly WeaponHash[] SniperRifles =
        {
            WeaponHash.SniperRifle,
            WeaponHash.HeavySniper,
            WeaponHash.HeavySniperMk2,
            WeaponHash.MarksmanRifle,
            WeaponHash.MarksmanRifleMk2,
        };

        public static readonly WeaponHash[] Guns = (new WeaponHash[] { })
            .Union(AssaultRifles)
            .Union(SniperRifles)
            .Union(ShotGuns)
            .Union(SubMachineGuns)
            .Union(Handguns)
            .ToArray();

    }
}