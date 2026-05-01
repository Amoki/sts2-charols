using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;


namespace Charolais.CharolaisCode.Cards.Rare;

public class Tourdefrance() : CharolaisCard(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new PowerVar<RegenPower>(1)
    ];

    protected override bool HasEnergyCostX => true;
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<RegenPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int powerAmount = this.ResolveEnergyXValue();
        if (this.IsUpgraded)
        {
            await PowerCmd.Apply<RegenPower>(choiceContext, this.Owner.Creature, (decimal.Add(1, powerAmount)),
                this.Owner.Creature, this);
        }
        else
        {
            await PowerCmd.Apply<RegenPower>(choiceContext, this.Owner.Creature, (decimal)powerAmount,
                this.Owner.Creature, this);
        }
    }


    protected override void OnUpgrade()
    {
        
    }
}