using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;


public class Burp() : CharolaisCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(4, ValueProp.Move),
        new PowerVar<WeakPower>(1m)
    ];
    
    protected override bool ShouldGlowGoldInternal => base.Owner.Creature.GetPowerAmount<PintPower>() >= 1;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<WeakPower>())];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        var alcoolPower = base.Owner.Creature.GetPowerAmount<PintPower>();
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue + alcoolPower).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        
        if (alcoolPower > 4m)
        {
            await PowerCmd.Apply<WeakPower>(choiceContext, cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(3m);
        base.DynamicVars.Weak.UpgradeValueBy(1m);
    }
}