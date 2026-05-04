using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Tirenraspaille() : CharolaisCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(5,ValueProp.Move),
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
        var strePower = base.Owner.Creature.GetPowerAmount<StrengthPower>();
        var doubleStrePower = strePower * 2;
        await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner.Creature, doubleStrePower, this.Owner.Creature, (CardModel) this);
        var combatState = this.CombatState;
        if (combatState != null)
            await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).WithHitCount(this.DynamicVars.Repeat.IntValue)
                .FromCard((CardModel)this).Targeting(cardPlay.Target ?? throw new InvalidOperationException()).WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner.Creature, Decimal.Negate(doubleStrePower), this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Repeat.UpgradeValueBy(1M);
    }
}