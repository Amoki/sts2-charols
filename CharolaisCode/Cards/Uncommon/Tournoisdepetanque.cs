using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Uncommon;


public class Tournoisdepetanque() : CharolaisCard(2,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Power", 1M)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var amount = this.DynamicVars["Power"].IntValue;
        var combatState = this.CombatState;
        if (combatState != null)
        {
            await PowerCmd.Apply<TournoisdepetanquePower>(choiceContext, this.Owner.Creature, amount, this.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars["Power"].UpgradeValueBy(1M);
    }
}
