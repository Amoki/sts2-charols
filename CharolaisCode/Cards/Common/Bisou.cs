using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Common;

public class Bisou() : CharolaisCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new DynamicVar[2]
            {
                (DynamicVar) new BlockVar(2m, ValueProp.Move),
                (DynamicVar) new RepeatVar(1)
            };
        }
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int blockGains = 1;
        blockGains += this.DynamicVars.Repeat.IntValue;
        for (int i = 0; i < blockGains; ++i)
        {
            Decimal num = await CreatureCmd.GainBlock(this.Owner.Creature, this.DynamicVars.Block, cardPlay);
        }
    }
    
    protected override void OnUpgrade() => this.DynamicVars.Repeat.UpgradeValueBy(1M);
}