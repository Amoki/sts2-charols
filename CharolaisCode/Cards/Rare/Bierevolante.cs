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

public class Bierevolante() : CharolaisCard(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0m),
        new ExtraDamageVar(1m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, target) => card.Owner.Creature.GetPowerAmount<PintPower>()),
        new RepeatVar(2),
        new CardsVar(1),
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
            await DamageCmd.Attack(base.DynamicVars.CalculatedDamage)
                .WithHitCount(this.DynamicVars.Repeat.IntValue).FromCard(this).TargetingAllOpponents(CombatState)
                .WithHitFx("vfx/vfx_giant_horizontal_slash")
                .Execute(choiceContext);
        var pile = PileType.Hand.GetPile(base.Owner);
        var card = base.Owner.RunState.Rng.CombatCardSelection.NextItem(pile.Cards);
        if (card == null)
            return;
        await CardCmd.Discard(choiceContext, card);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Repeat.UpgradeValueBy(1M);
    }
}