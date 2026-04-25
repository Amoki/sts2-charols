using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Embrasserfanny() : CharolaisCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<StrengthPower>(3)];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Embrasserfanny card = this;
        await PowerCmd.Apply<SetupStrikePower>(choiceContext, this.Owner.Creature, this.DynamicVars.Strength.BaseValue, this.Owner.Creature,this);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Strength.UpgradeValueBy(2);
    }
}