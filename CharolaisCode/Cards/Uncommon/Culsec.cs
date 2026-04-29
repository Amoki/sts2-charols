using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Culsec() : CharolaisCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new BlockVar(7m, ValueProp.Move),
        new PowerVar<PintPower>(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        int powerAmount = this.Owner.Creature.GetPowerAmount<PintPower>();
        if (powerAmount >= 8)
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(3M);
    }
}