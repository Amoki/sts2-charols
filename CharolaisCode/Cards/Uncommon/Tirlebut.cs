using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Tirlebut() : CharolaisCard(0,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(0)
    ];
    
    protected override bool HasEnergyCostX => true;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        int energyAmount = this.ResolveEnergyXValue();
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(energyAmount)
            .WithHitCount(energyAmount).FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitVfxNode((Func<Creature, Node2D>) (t => (Node2D) NStabVfx.Create(t, true, VfxColor.Gold)))
            .Execute(choiceContext);
        await PlayerCmd.GainEnergy((Decimal) this.DynamicVars.Energy.IntValue, this.Owner);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Energy.UpgradeValueBy(1M);
    }
}