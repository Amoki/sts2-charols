using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using static MegaCrit.Sts2.Core.Entities.Cards.TargetType;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Bouledevantbouledargent() : CharolaisCard(2,
    CardType.Skill, CardRarity.Uncommon,
    Self)
{

    public override bool GainsBlock => true;
    
    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(4M),
        new CalculationExtraVar(1M),
        (DynamicVar) new CalculatedBlockVar(ValueProp.Move).WithMultiplier((card, _) => (decimal)card.Owner.PlayerCombatState.AllCards.Count(c => c.Tags.Contains(PetanqueTag.Petanque)))
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (this.IsUpgraded)
        {
            int count = base.Owner.PlayerCombatState.AllCards.Count(c =>
                c.Tags != null && c.Tags.Contains(PetanqueTag.Petanque));

            decimal extraBonus = 2M;
            decimal totalBlock = 4M + (extraBonus * count);
            BlockVar manualBlock = new BlockVar(0, ValueProp.Move);
            manualBlock.BaseValue = totalBlock;
            
            await CreatureCmd.GainBlock(this.Owner.Creature, manualBlock, cardPlay);
        }
        else
        {
            int count = base.Owner.PlayerCombatState.AllCards.Count(c =>
                c.Tags != null && c.Tags.Contains(PetanqueTag.Petanque));

            decimal extraBonus = 1M;
            decimal totalBlock = 4M + (extraBonus * count);
            BlockVar manualBlock = new BlockVar(0, ValueProp.Move);
            manualBlock.BaseValue = totalBlock;
            
            await CreatureCmd.GainBlock(this.Owner.Creature, manualBlock, cardPlay);
        }
    }
    
    protected override void OnUpgrade() => this.DynamicVars.CalculationExtra.UpgradeValueBy(1M);
}