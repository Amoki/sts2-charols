using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Powers;

public class MusiquelacteePower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        bool hasExhaust = card.Keywords.Contains(CardKeyword.Exhaust);
        if (!hasExhaust)
        {
            return playCount;
        }
        int count = CombatManager.Instance.History.CardPlaysStarted.Count(e => 
            e.Actor == this.Owner && 
            e.CardPlay.Card.Keywords.Contains(CardKeyword.Exhaust) && 
            e.HappenedThisTurn(this.CombatState)
        );
        
        if (count == 0)
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
        
        if (card.Keywords.Contains(CardKeyword.Exhaust))
        {
            int count = CombatManager.Instance.History.CardPlaysStarted.Count(e => 
                e.Actor == this.Owner && 
                e.CardPlay.Card.Keywords.Contains(CardKeyword.Exhaust) && 
                e.HappenedThisTurn(this.CombatState)
            );
            
            if (count == 0)
            {
                modifiedCost = 0M;
                return true;
            }
        }

        return false;
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