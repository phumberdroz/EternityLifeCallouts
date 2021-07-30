using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API.Utils;

namespace EternityLifeCallouts.Extensions
{
    public static class EntityExtensions
    {
        public static void MakeAggressiveAgainstPlayers(this Ped ped)
        {
            API.SetPedRelationshipGroupHash(ped.Handle, (uint) API.GetHashKey("HATES_PLAYER"));
            API.SetRelationshipBetweenGroups(5, (uint) API.GetHashKey("HATES_PLAYER"), (uint) API.GetHashKey("PLAYER"));
            API.SetRelationshipBetweenGroups(5, (uint) API.GetHashKey("PLAYER"), (uint) API.GetHashKey("HATES_PLAYER"));
            API.SetPedCombatAttributes(ped.Handle, 46, true);
            API.SetPedCombatAbility(ped.Handle, 100);
            API.SetPedCombatMovement(ped.Handle, 2);
            API.SetPedCombatRange(ped.Handle, 0);
        }

        public static void GiveRandomHandGun(this Ped ped)
        {
            ped.Weapons.Give(Weapons.Handguns.SelectRandom(), RandomUtils.GetRandomNumber(100, 500), true, true);
        }

        public static void GiveRandomWeapon(this Ped ped)
        {
            ped.Weapons.Give(Weapons.Guns.SelectRandom(), RandomUtils.GetRandomNumber(100, 500), true, true);
        }
    }
}