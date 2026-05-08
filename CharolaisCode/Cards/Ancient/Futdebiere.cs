using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Ancient;

public class Futdebiere() : CharolaisCard(1, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(20, ValueProp.Move),
        new DynamicVar("Power", 5)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3")
                    .Execute(choiceContext);
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, DynamicVars["Power"].IntValue, this.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(5m);
        base.DynamicVars["Power"].UpgradeValueBy(3m);
        this.EnergyCost.UpgradeBy(-1);
    }
}