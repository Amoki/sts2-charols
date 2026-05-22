using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Tirenraspaille() : CharolaisCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(8m),
        new ExtraDamageVar(1m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) => card.Owner.Creature.GetPowerAmount<StrengthPower>() * 2),
        new RepeatVar(2)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>()
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        var combatState = this.CombatState;
        if (combatState != null)
                await DamageCmd.Attack(base.DynamicVars.CalculatedDamage)
                    .WithHitCount(this.DynamicVars.Repeat.IntValue)
                    .FromCard(this).Targeting(cardPlay.Target ?? throw new InvalidOperationException())
                    .WithHitFx("vfx/vfx_attack_slash")
                    .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Repeat.UpgradeValueBy(1M);
    }
}