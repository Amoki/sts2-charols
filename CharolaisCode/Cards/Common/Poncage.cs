using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Common;

public class Poncage() : CharolaisCard(2,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new DamageVar(14M, ValueProp.Move),
        new PowerVar<PintPower>(1)
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
        int powerAmount = this.Owner.Creature.GetPowerAmount<PintPower>();
        if (powerAmount >= 0)
        {
            await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, (Decimal.Negate(powerAmount)), this.Owner.Creature, (CardModel) this);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(4M);
    }
}