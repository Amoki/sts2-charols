using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Common;

public class Dortoir() : CharolaisCard(2,
    CardType.Skill, CardRarity.Common,
    TargetType.AnyAlly)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new BlockVar(14m, ValueProp.Move),
        new DynamicVar("Power", 2M)
    ];
    
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.Owner.RunState.Rng.CombatTargets.NextBool())
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
            if (cardPlay.Target != null) await CreatureCmd.GainBlock(cardPlay.Target, base.DynamicVars.Block, cardPlay);
        }
        else
        {
            var power = DynamicVars["Power"].IntValue;
            await PowerCmd.Apply<FrailPower>(choiceContext, this.Owner.Creature, power, this.Owner.Creature, this);
            if (cardPlay.Target != null)  await PowerCmd.Apply<FrailPower>(choiceContext, cardPlay.Target, power, this.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(8M);
        this.DynamicVars["Power"].UpgradeValueBy(1M);
    }
}