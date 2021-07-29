using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;

namespace EternityLifeCallouts
{
#if DEBUG
    [CalloutProperties("Drug Deal Gang Related", "meat", "1.0")]
    public class DrugDeal : Callout
    {
        public DrugDeal()
        {
            this.InitInfo(new Vector3(4.180113f, -1737.416f, 29.30312f));
            this.ShortName = "Gang: Drug Deal in progress";
            this.ResponseCode = 3;
            this.StartDistance = 150;
        }

        public override async Task OnAccept()
        {
            this.InitBlip(75f, (BlipColor) 66, (BlipSprite) 9, 100);
            Utils.AdvNotify("commonmenu", "mp_alerttriangle", false, 1, "911 Dispatch:", "~y~Additional Info",
                "~w~Regroup with other units and head to the scene.");
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            var dealer = await this.SpawnPed(PedHash.BallaEast01GMY, this.Location);
            var dealer2 = await this.SpawnPed(PedHash.Agent, this.Location);
            dealer.AlwaysKeepTask = true;
            dealer.BlockPermanentEvents = true;
            dealer2.AlwaysKeepTask = true;
            dealer2.BlockPermanentEvents = true;
            await BaseScript.Delay(2000);
            
            dealer.Task.TurnTo(dealer2);
            // dealer.MovementAnimationSet/**/
            dealer2.Task.TurnTo(dealer);
            await BaseScript.Delay(2000);
            dealer.Task.PlayAnimation("amb@world_human_drug_dealer_hard@male@idle_a", "base", -1, -1, AnimationFlags.None);

        }
    }
#endif
}