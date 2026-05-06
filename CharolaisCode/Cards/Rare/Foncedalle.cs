using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Foncedalle() : CharolaisCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(1),
        new DynamicVar("Power", 1)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        this.EnergyHoverTip,
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var power = base.Owner.Creature.GetPowerAmount<PintPower>();
        await PlayerCmd.GainEnergy(power, this.Owner);
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, decimal.Negate(power), this.Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}