using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Convulsions() : CharolaisCard(3, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0m),
        new ExtraDamageVar(1m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, Creature? target) => card.Owner.Creature.GetPowerAmount<PintPower>()),
        new RepeatVar(3)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    // public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (CombatState != null)
        {
            AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars.CalculatedDamage)
                .WithHitCount(this.DynamicVars.Repeat.IntValue).FromCard(this).TargetingAllOpponents(CombatState)
                .WithHitFx("vfx/vfx_giant_horizontal_slash")
                .Execute(choiceContext);
        }

        var players = base.CombatState?.Players;
        if (players == null) { return; }
        var alcoolPower = base.Owner.Creature.GetPowerAmount<PintPower>();
        if (alcoolPower > 0)
        {
            int index = new Random().Next(players.Count);
            Player randomPlayer = players[index];
            await Cmd.Wait(0.5f);
            await DamageCmd.Attack(alcoolPower).FromCard(this).Targeting(randomPlayer.Creature)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Repeat.UpgradeValueBy(2M);
    }
}