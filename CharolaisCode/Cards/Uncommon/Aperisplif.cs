using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Aperisplif() : CharolaisCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new BlockVar(13m, ValueProp.Move),
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromKeyword(CardKeyword.Retain))];

    private static LocString RetainSelectionPrompt => new LocString("card_selection", "TO_RETAIN");
    
    // ToDo fix le texte de sélection de carte
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        CardSelectorPrefs prefs = new CardSelectorPrefs(RetainSelectionPrompt, 1);
        var selectedCards = await CardSelectCmd.FromHand(
            choiceContext, 
            this.Owner, 
            prefs, 
            c => !c.Keywords.Contains(CardKeyword.Retain), 
            this
        );
        CardModel? card = selectedCards?.FirstOrDefault();
        if (card == null) return;
        CardCmd.ApplyKeyword(card, CardKeyword.Retain);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(3M);
    }
}