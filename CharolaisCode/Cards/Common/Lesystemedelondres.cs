using BaseLib.Extensions;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Common;

public class Lesystemedelondres() : CharolaisCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(7, ValueProp.Move),
        new ChestVar(7)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_flying_slash")
            .Execute(choiceContext);
        await PowerCmd.Apply<ChestPower>(choiceContext, cardPlay.Target ?? throw new InvalidOperationException(), DynamicVars["Chest"].IntValue, this.Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3M);
        this.DynamicVars["Chest"].UpgradeValueBy(3M);
    }
}