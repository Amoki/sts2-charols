using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Convulsions() : CharolaisCard(3, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0m),
        new ExtraDamageVar(1m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, target) => card.Owner.Creature.GetPowerAmount<PintPower>()),
        new RepeatVar(4)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (CombatState != null)
        {
            await DamageCmd.Attack(this.DynamicVars.CalculatedDamage)
                .WithHitCount(this.DynamicVars.Repeat.IntValue).FromCard(this).TargetingAllOpponents(CombatState)
                .WithHitFx("vfx/vfx_giant_horizontal_slash")
                .Execute(choiceContext);
        }

        var allies = base.CombatState?.Allies.Where(ally => ally.IsAlive);
        if (allies == null) { return; }
        var ally = base.Owner.RunState.Rng.CombatTargets.NextItem(allies) ?? throw new InvalidOperationException();
        await Cmd.Wait(0.5f);
        await DamageCmd.Attack(8).Unpowered().FromCard(this).Targeting(ally)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Repeat.UpgradeValueBy(2M);
        this.EnergyCost.UpgradeBy(-1);
    }
}