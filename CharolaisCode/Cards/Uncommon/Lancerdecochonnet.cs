using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Lancerdecochonnet() : CharolaisCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DexterityPower>(4)];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Lancerdecochonnet card = this;
        await PowerCmd.Apply<LancerdecochonnetPower>(choiceContext, this.Owner.Creature, this.DynamicVars.Dexterity.BaseValue, this.Owner.Creature,this);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Dexterity.UpgradeValueBy(2);
    }
}