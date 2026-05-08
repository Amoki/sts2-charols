using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Ptitblancdedixh() : CharolaisCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new MaxHpVar(3M),
        new AlcoolVar(3)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
            await CreatureCmd.GainMaxHp(this.Owner.Creature, this.DynamicVars.MaxHp.IntValue);
            await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, DynamicVars["Alcool"].IntValue, this.Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}