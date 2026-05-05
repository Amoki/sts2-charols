using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Common;

public class Labricotdejacoulot() : CharolaisCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new BlockVar(1M, ValueProp.Move),
        new PowerVar<PintPower>(1M)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<PintPower>())];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int powerAmount = this.Owner.Creature.GetPowerAmount<PintPower>();
        int blockAmount = this.DynamicVars.Block.IntValue;
        int totalAmount = powerAmount + blockAmount;
        BlockVar manualBlock = new BlockVar(0, ValueProp.Move);
        manualBlock.BaseValue = totalAmount;
        {
            await CreatureCmd.GainBlock(this.Owner.Creature, manualBlock, cardPlay);
        }
    }
    
    protected override void OnUpgrade() => this.DynamicVars.Block.UpgradeValueBy(3M);
}