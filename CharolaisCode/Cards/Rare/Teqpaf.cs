using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Teqpaf() : CharolaisCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<PintPower>(1)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int powerAmount = this.Owner.Creature.GetPowerAmount<PintPower>();
        if (this.IsUpgraded)
        {
            await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, decimal.Multiply(2, powerAmount), this.Owner.Creature, (CardModel) this);
        }
        else
        {
            await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, (powerAmount), this.Owner.Creature, (CardModel) this);
        }
    }
    
    protected override void OnUpgrade()
    {
        
    }
}