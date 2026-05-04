using Charolais.CharolaisCode.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Powers;

public class TerraindepetanquePower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
   protected override object InitInternalData() => (object) new TerraindepetanquePower.Data();

  public override Task BeforePowerAmountChanged(
    PowerModel power,
    Decimal amount,
    Creature target,
    Creature? applier,
    CardModel? cardSource)
  {
    if (power != this)
      return Task.CompletedTask;
    this.HideTemporaryZeroCostVisual();
    return Task.CompletedTask;
  }

  public override Task BeforeApplied(
    Creature target,
    Decimal amount,
    Creature? applier,
    CardModel? cardSource)
  {
    this.HideTemporaryZeroCostVisual();
    return Task.CompletedTask;
  }

  public override bool TryModifyEnergyCostInCombatLate(
    CardModel card,
    Decimal originalCost,
    out Decimal modifiedCost)
  {
    modifiedCost = originalCost;
    if (this.ShouldSkip(card))
      return false;
    modifiedCost = 0M;
    return true;
  }

  public override bool TryModifyStarCost(
    CardModel card,
    Decimal originalCost,
    out Decimal modifiedCost)
  {
    modifiedCost = originalCost;
    if (this.ShouldSkip(card))
      return false;
    modifiedCost = 0M;
    return true;
  }

  public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
  {
    if (cardPlay.Card.Owner.Creature == this.Owner && cardPlay != null && !cardPlay.IsAutoPlay && cardPlay.IsLastInSeries)
      ++this.GetInternalData<TerraindepetanquePower.Data>().CardsPlayedThisTurn;
    return Task.CompletedTask;
  }

  public override Task BeforeSideTurnStart(
    PlayerChoiceContext choiceContext,
    CombatSide side,
    ICombatState combatState)
  {
    if (side == this.Owner.Side)
      this.GetInternalData<TerraindepetanquePower.Data>().CardsPlayedThisTurn = 0;
    return Task.CompletedTask;
  }

  private bool ShouldSkip(CardModel card)
  {
    if (card.Owner.Creature != this.Owner)
      return true;
    
    PileType? type = card.Pile?.Type;
    bool inHandOrPlay = type == PileType.Hand || type == PileType.Play;
    if (!inHandOrPlay)
      return true;
    
    bool isAttack = card.Type == CardType.Attack;
    bool isPetanque = card.Tags.Contains(PetanqueTag.Petanque);

    if (!isAttack || !isPetanque)
      return true;
    
    return this.GetInternalData<TerraindepetanquePower.Data>().CardsPlayedThisTurn >= this.Amount;
  }

  private void HideTemporaryZeroCostVisual()
  {
    this.GetInternalData<TerraindepetanquePower.Data>().CardsPlayedThisTurn = 999999999;
  }

  private class Data
  {
    public int CardsPlayedThisTurn;
  }
}