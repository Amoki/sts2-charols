using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Foncedalle() : CharolaisCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar)new EnergyVar(1),
        new DynamicVar("Power", 1)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        this.EnergyHoverTip,
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int power = base.Owner.Creature.GetPowerAmount<PintPower>();
        var energy = DynamicVars["Power"].IntValue;
        await PlayerCmd.GainEnergy(power, this.Owner);
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, decimal.Negate(power), this.Owner.Creature, (CardModel) this);
    }
    
    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}