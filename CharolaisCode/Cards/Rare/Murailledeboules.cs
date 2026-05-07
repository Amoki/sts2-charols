using System.Globalization;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Murailledeboules() : CharolaisCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override bool GainsBlock => true;
    
    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(8m),
        new CalculationExtraVar(1m),
        new CalculatedBlockVar(ValueProp.Unpowered).WithMultiplier((card, _) => GetOwnerDextAmount(card) * 3)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<DexterityPower>())];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.CalculatedBlock.Calculate(cardPlay.Target), base.DynamicVars.CalculatedBlock.Props, cardPlay);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.CalculationBase.UpgradeValueBy(4M);
    }
    
    private static decimal GetOwnerDextAmount(CardModel card)
    {
        if (!card.IsMutable)
        {
            return 0m;
        }
        if (card.Owner == null)
        {
            return 0m;
        }
        if (card.Pile == null)
        {
            return 0m;
        }
        if (!card.Pile.IsCombatPile)
        {
            return 0m;
        }
        return card.Owner.Creature.GetPowerAmount<DexterityPower>();
    }
}