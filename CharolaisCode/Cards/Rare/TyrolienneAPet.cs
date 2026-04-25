using Charolais.CharolaisCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Cards.Rare;


public class TyrolienneAPet() : CharolaisCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyAlly)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(cardPlay.Target.Player, "cardPlay.Target.Player");

        var cardsToGive = (
            await CardSelectCmd.FromHandForDiscard(
                choiceContext,
                base.Owner,
                new CardSelectorPrefs(new LocString("card_selection", "TO_TRADE"), base.DynamicVars.Cards.IntValue), 
                null,
                this
            )
        ).ToList();
        
        if (cardsToGive.Count == 0) { return; }

        var cardsToReceive = (
            await CardSelectCmd.FromHand(
                new BlockingPlayerChoiceContext(),
                cardPlay.Target.Player,
                new CardSelectorPrefs(new LocString("card_selection", "TO_TRADE"), base.DynamicVars.Cards.IntValue),
                null
                , this
            )
        ).ToList();
        
        foreach (var cardToGive in cardsToGive)
        {
            var newCard = cardToGive.CreateClone();
            // newCard.Owner = cardPlay.Target.Player;
            await CardPileCmd.AddGeneratedCardToCombat(cardToGive, PileType.Hand, cardPlay.Target.Player);
        }

        foreach (var cardToReceive in cardsToReceive)
        {
            var newCard = cardToReceive.CreateClone();
            await CardPileCmd.AddGeneratedCardToCombat(cardToReceive, PileType.Hand, base.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1);
    }
}