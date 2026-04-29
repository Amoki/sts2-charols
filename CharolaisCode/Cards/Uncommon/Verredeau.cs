using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Verredeau() : CharolaisCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new BlockVar(6m, ValueProp.Move),
        new PowerVar<PintPower>(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        int powerAmount = this.Owner.Creature.GetPowerAmount<PintPower>();
        if (powerAmount >= 0)
        {
            await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, (Decimal.Negate(powerAmount)), this.Owner.Creature, (CardModel) this);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(3M);
    }
}