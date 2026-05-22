using System.Linq;
using System.Threading.Tasks;
using Charolais.CharolaisCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Powers;

public class RangerlegaragePower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task BeforeHandDraw(
        Player player,
        PlayerChoiceContext choiceContext,
        ICombatState combatState)
    {
        if (player != this.Owner.Player)
            return;
        
        var cardPool = ModelDb.CardPool<CharolaisCardPool>()
            .GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint);
        
        var filteredCards = cardPool.Where(c => 
            c.Type == CardType.Skill && 
            c.Rarity == CardRarity.Uncommon
        );
        
        var cardsToGenerate = CardFactory.GetDistinctForCombat(
            player, 
            filteredCards, 
            this.Amount, 
            player.RunState.Rng.CombatCardGeneration
        ).ToList();
        
        await CardPileCmd.AddGeneratedCardsToCombat(cardsToGenerate, PileType.Hand, this.Owner.Player);
        this.Flash();
   }
}