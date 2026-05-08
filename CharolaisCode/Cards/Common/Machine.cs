using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
namespace Charolais.CharolaisCode.Cards.Common;



public class Machine() : CharolaisCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(7, ValueProp.Move),
        new CalculationBaseVar(0m),
        new ExtraDamageVar(1m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, target) => card.Owner.Creature.GetPowerAmount<PintPower>())
    ];
    
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PintPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        var alcoolPower = base.Owner.Creature.GetPowerAmount<PintPower>();
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue + alcoolPower).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        
        var players = base.CombatState?.Players;
        if (players == null) { return; }
        if (alcoolPower > 0)
        {
            Player randomPlayer = base.Owner.RunState.Rng.CombatTargets.NextItem(players) ?? throw new InvalidOperationException();
            await Cmd.Wait(0.5f);
            await DamageCmd.Attack(alcoolPower).Unpowered().FromCard(this).Targeting(randomPlayer.Creature)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        }
        
        
    }
    
    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(3m);
    }
}