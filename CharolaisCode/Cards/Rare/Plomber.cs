using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Plomber() : CharolaisCard(0,
    CardType.Attack, CardRarity.Rare,
    TargetType.AllEnemies)
{
    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(1M, ValueProp.Move),
        new RepeatVar(4)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var combatState = this.CombatState;
        if (combatState != null)
            await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).WithHitCount(this.DynamicVars.Repeat.IntValue)
                .FromCard(this).TargetingAllOpponents(combatState).WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Repeat.UpgradeValueBy(2M);
    }
}