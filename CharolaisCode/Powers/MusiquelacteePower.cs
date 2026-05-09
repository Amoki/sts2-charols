using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Powers;

public class MusiquelacteePower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    private class Data
    {
        public int CardsPlayedThisTurn;
    }
    protected override object InitInternalData() => new Data();
    

    public override Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        ICombatState combatState)
    {
        if (side == this.Owner.Side)
        {
            this.GetInternalData<Data>().CardsPlayedThisTurn = 0;
        }
        return Task.CompletedTask;
    }
    
    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature == this.Owner && !cardPlay.IsAutoPlay && cardPlay.IsLastInSeries && cardPlay.Card.Keywords.Contains(CardKeyword.Exhaust))
        {
            ++this.GetInternalData<Data>().CardsPlayedThisTurn;
        }
        return Task.CompletedTask;
    }

    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        if (card.Owner.Creature != base.Owner)
        {
            return playCount;
        }

        var hasExhaust = card.Keywords.Contains(CardKeyword.Exhaust);
        if (!hasExhaust)
        {
            return playCount;
        }

        if (this.GetInternalData<Data>().CardsPlayedThisTurn < this.Amount)
        {
            return playCount + 2;
        } 
        return playCount;
    }

    public override bool TryModifyEnergyCostInCombatLate(
        CardModel card,
        decimal originalCost,
        out decimal modifiedCost)
    {
        modifiedCost = originalCost;
        
        if (card.Owner.Creature != base.Owner)
        {
            return false;
        }
        
        if (!card.Keywords.Contains(CardKeyword.Exhaust)) return false;

        if (this.GetInternalData<Data>().CardsPlayedThisTurn >= this.Amount)
        {
            return false;
        }

        modifiedCost = 0M;
        return true;

    }
    
    public override Task AfterModifyingCardPlayCount(CardModel card)
    {
        if (card.Keywords.Contains(CardKeyword.Exhaust))
        {
            this.Flash();
        }
        return base.AfterModifyingCardPlayCount(card);
    }
    
}