using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Cards.Common;

public class Burger() : CharolaisCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<VigorPower>(5M)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<VigorPower>())];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<VigorPower>(choiceContext, this.Owner.Creature, this.DynamicVars["VigorPower"].IntValue, this.Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars["VigorPower"].UpgradeValueBy(3M);
    }
}