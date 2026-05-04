using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Embrasserfanny() : CharolaisCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{

    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<StrengthPower>(4)];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<StrengthPower>())];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Embrasserfanny card = this;
        await PowerCmd.Apply<EmbrasserfannyPower>(choiceContext, this.Owner.Creature, this.DynamicVars.Strength.BaseValue, this.Owner.Creature,this);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Strength.UpgradeValueBy(2);
    }
}