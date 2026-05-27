using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Common;

public class Poncage() : CharolaisCard(2,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(16M, ValueProp.Move),
        new DynamicVar("Power",8),
        new CardsVar(1)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3")
            .Execute(choiceContext);
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, decimal.Negate(DynamicVars["Power"].IntValue), this.Owner.Creature, this);
        await CardPileCmd.Draw(choiceContext, this.DynamicVars.Cards.BaseValue, this.Owner);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(6M);
        this.DynamicVars.Cards.UpgradeValueBy(1M);
    }
}