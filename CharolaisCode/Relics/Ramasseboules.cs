using BaseLib.Utils;
using Charolais.CharolaisCode.Cards;
using Charolais.CharolaisCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Charolais.CharolaisCode.Relics;

[Pool(typeof(CharolaisRelicPool))]

public class Ramasseboules : CharolaisRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != this.Owner || this.Owner.Creature.CombatState?.RoundNumber != 1)
            return;
        this.Flash();
        
        var fullPool = this.Owner.Character.CardPool.GetUnlockedCards(this.Owner.UnlockState, this.Owner.RunState.CardMultiplayerConstraint);
        var petanqueCards = fullPool.Where(c => c.Tags.Contains(PetanqueTag.Petanque)).ToList();
        if (petanqueCards.Count == 0) return;
        
        var card = CardFactory.GetDistinctForCombat(
            this.Owner, 
            petanqueCards, 
            1, 
            this.Owner.RunState.Rng.CombatCardGeneration
        ).First();
        
        card.SetToFreeThisTurn();
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, player);
    }
}