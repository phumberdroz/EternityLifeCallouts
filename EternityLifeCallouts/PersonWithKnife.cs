using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using EternityLifeCallouts.Extensions;
using FivePD.API;
using FivePD.API.Utils;

namespace EternityLifeCallouts
{
    [CalloutProperties("Person with Knife", "meat", "1.0")]
    public class PersonWithKnife : Callout
    {
        public PersonWithKnife()
        {
            InitInfo(World.GetNextPositionOnStreet(
                Game.PlayerPed.Position.Around(RandomUtils.GetRandomNumber(100, 700))));
            ShortName = "Person With Knife";
            CalloutDescription = "911 Call : Person with knife spotted.";
            ResponseCode = 3;
            StartDistance = 200;
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            var ped = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            ped.Weapons.Give(Weapons.MeleeWeapons.SelectRandom(), 600, true, true);

            var scenarios = new List<Action>
            {
                () => ScenarioWanderAround(ped),
                () => ScenarioAttackPolice(ped)
            };

            scenarios.SelectRandom()();
        }

        private void ScenarioWanderAround(Ped suspect)
        {
            suspect.Task.WanderAround();
        }

        private void ScenarioAttackPolice(Ped suspect)
        {
            suspect.MakeAggressiveAgainstPlayers();
            suspect.Task.WanderAround(suspect.Position, 5);
        }

        public override async Task OnAccept()
        {
            InitBlip(100f);
            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info",
                "Search the area for the subject.");
        }

        public async Task OnTick()
        {
            // await BaseScript.Delay(this.random.Next(900, 1200));
            // if (this.FirstTickTask)
            //     return;
            // Vector3 position = ((Entity) this.suspect1).Position;
            // if ((double) ((Vector3) ref position ).DistanceToSquared(((Entity) Game.PlayerPed).Position) <= 45.0)
            // {
            //     object pedData = (object) new ExpandoObject();
            //     List<object> items = new List<object>();
            //     object usedNeedles = (object) new
            //     {
            //         Name = "Used Needles",
            //         IsIllegal = true
            // };
            // object marijuana = (object) new
            // {
            //     Name = "Marijuana",
            //     IsIllegal = true
            // };
            // // ISSUE: reference to a compiler-generated field
            // if (PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
            // {
            //     // ISSUE: reference to a compiler-generated field
            //     PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__0 =
            //         CallSite<Action<CallSite, Utilities.SetPedDelegate, int, object>>.Create(Binder.Invoke(
            //             CSharpBinderFlags.ResultDiscarded, typeof(PersonWithKnife.PersonWithKnife),
            //             (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
            //             {
            //                 CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            //                 CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            //                 CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            //             }));
            // }
            //
            // // ISSUE: reference to a compiler-generated field
            // // ISSUE: reference to a compiler-generated field
            // PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__0.Target(
            //     (CallSite) PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__0,
            //     (Utilities.SetPedDelegate) Utilities.SetPedData, ((Entity) this.suspect1).NetworkId, pedData);
            // int z = this.random.Next(1, 101);
            // if (z <= 20)
            // {
            //     this.suspect1.Task.ReactAndFlee(Game.PlayerPed);
            //     items.Add(marijuana);
            // }
            // else
            // {
            //     this.suspect1.Task.FightAgainst(Game.PlayerPed);
            //     // ISSUE: reference to a compiler-generated field
            //     if (PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
            //     {
            //         // ISSUE: reference to a compiler-generated field
            //         PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__1 =
            //             CallSite<Func<CallSite, object, double, object>>.Create(Binder.SetMember(
            //                 CSharpBinderFlags.None, "alcoholLevel", typeof(PersonWithKnife.PersonWithKnife),
            //                 (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            //                 {
            //                     CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            //                     CSharpArgumentInfo.Create(
            //                         CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant,
            //                         (string) null)
            //                 }));
            //     }
            //
            //     // ISSUE: reference to a compiler-generated field
            //     // ISSUE: reference to a compiler-generated field
            //     object obj = PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__1.Target(
            //         (CallSite) PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__1, pedData, 0.23);
            //     items.Add(usedNeedles);
            // }
            //
            // // ISSUE: reference to a compiler-generated field
            // if (PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__2 == null)
            // {
            //     // ISSUE: reference to a compiler-generated field
            //     PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__2 =
            //         CallSite<Func<CallSite, object, List<object>, object>>.Create(Binder.SetMember(
            //             CSharpBinderFlags.None, "items", typeof(PersonWithKnife.PersonWithKnife),
            //             (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            //             {
            //                 CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            //                 CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            //             }));
            // }
            //
            // // ISSUE: reference to a compiler-generated field
            // // ISSUE: reference to a compiler-generated field
            // object obj1 = PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__2.Target(
            //     (CallSite) PersonWithKnife.PersonWithKnife.\u003C\u003Eo__6.\u003C\u003Ep__2, pedData, items);
            // this.FirstTickTask = true;
            // pedData = (object) null;
            // items = (List<object>) null;
            // usedNeedles = (object) null;
            // marijuana = (object) null;
        }
    }
}