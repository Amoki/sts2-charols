using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Bouleenplastique() : CharolaisCard(0,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(3M, ValueProp.Move),
        new CardsVar(1),
        new EnergyVar(1)
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        await CardPileCmd.Draw(choiceContext, this.DynamicVars.Cards.BaseValue, this.Owner);
        await PlayerCmd.GainEnergy(this.DynamicVars.Energy.IntValue, this.Owner);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Cards.UpgradeValueBy(1M);
    }
}