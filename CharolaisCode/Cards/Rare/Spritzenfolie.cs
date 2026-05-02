using BaseLib.Extensions;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Spritzenfolie() : CharolaisCard(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new DamageVar(18M, ValueProp.Move),
        (DynamicVar) new AlcoolVar(6)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (CombatState != null)
        {
            AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue)
                .FromCard(this).Targeting(cardPlay.Target ?? throw new InvalidOperationException())
                .WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3")
                .Execute(choiceContext);
        }
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, DynamicVars["Alcool"].IntValue, this.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(6m);
        this.DynamicVars["Alcool"].UpgradeValueBy(4M);
    }
}