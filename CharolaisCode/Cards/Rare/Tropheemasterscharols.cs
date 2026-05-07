using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;


namespace Charolais.CharolaisCode.Cards.Rare;

public class Tropheemasterscharols() : CharolaisCard(3, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Power", 2M),
        new CardsVar(1)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>(),
        HoverTipFactory.FromPower<DexterityPower>(),
    ];
    
    private static LocString SettofreeSelectionPrompt => new LocString("card_selection", "TO_SETTOFREE");

    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var amount = this.DynamicVars["Power"].IntValue;
        var combatState = this.CombatState;
        if (combatState != null)
        {
            await PowerCmd.Apply<DexterityPower>(choiceContext, this.Owner.Creature, amount, this.Owner.Creature, (CardModel)this);
            await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner.Creature, amount, this.Owner.Creature, (CardModel)this);
        }
        
        var prefs = new CardSelectorPrefs(SettofreeSelectionPrompt, 1);
        (await CardSelectCmd.FromHand(choiceContext, this.Owner, prefs, (c => c.CostsEnergyOrStars(false) || c.CostsEnergyOrStars(true)), this)).FirstOrDefault()?.SetToFreeThisCombat();
    }


    protected override void OnUpgrade()
    {
        this.DynamicVars["Power"].UpgradeValueBy(1M);
    }
}