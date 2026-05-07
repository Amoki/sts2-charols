using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Bierevolante() : CharolaisCard(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
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
        var alcoolPower = base.Owner.Creature.GetPowerAmount<PintPower>();
        if (CombatState != null)
            await DamageCmd.Attack(alcoolPower)
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