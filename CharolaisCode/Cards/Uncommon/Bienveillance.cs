using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Bienveillance() : CharolaisCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new HealVar(2)
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.Heal(this.Owner.Creature, this.DynamicVars.Heal.BaseValue);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Heal.UpgradeValueBy(2);
    }
}