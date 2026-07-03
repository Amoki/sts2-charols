using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Common;

public class Lancerdeboule() : CharolaisCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(3, ValueProp.Move)];
    
    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).WithHitCount(3).FromCard(this, cardPlay).Targeting(cardPlay.Target)
            .WithAttackerAnim("Cast", 1f)
            .WithAttackerFx(() => NMinionDiveBombVfx.Create(cardPlay.Card.Owner.Creature, cardPlay.Target))
            .Execute(choiceContext);
    }
    
    protected override void OnUpgrade() => this.DynamicVars.Damage.UpgradeValueBy(1M);
}