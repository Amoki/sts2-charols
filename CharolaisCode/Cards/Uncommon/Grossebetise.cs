using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Grossebetise() : CharolaisCard(3,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(30M, ValueProp.Move),
        new HpLossVar(3M)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var random = base.Owner.RunState.Rng.CombatTargets.NextInt(0, 100);
        if (random >= 50)
        {
            if (CombatState != null)
                await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).WithHitCount(1)
                    .FromCard((CardModel)this).TargetingAllOpponents(CombatState).WithHitFx("vfx/vfx_attack_slash")
                    .Execute(choiceContext);
        }
        else
        {
            await CreatureCmd.Damage(choiceContext, this.Owner.Creature, this.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, (CardModel) this);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(8M);
    }
}