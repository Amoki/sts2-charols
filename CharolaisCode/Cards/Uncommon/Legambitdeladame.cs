using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Legambitdeladame() : CharolaisCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(10M, ValueProp.Move),
        new ChestVar(1)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        var powerAmount = cardPlay.Target.IsAlive ? cardPlay.Target.GetPowerAmount<ChestPower>() : 0;
        if (this.IsUpgraded)
        {
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3")
                .Execute(choiceContext);
            await PowerCmd.Apply<ChestPower>(choiceContext, cardPlay.Target ?? throw new InvalidOperationException(), decimal.Multiply(2, powerAmount), this.Owner.Creature, this);
        }
        else
        {
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3")
                .Execute(choiceContext);
            await PowerCmd.Apply<ChestPower>(choiceContext, cardPlay.Target ?? throw new InvalidOperationException(), powerAmount, this.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        
    }
}