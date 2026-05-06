using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Charolais.CharolaisCode.Powers;

public class BarbecuePower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override bool IsInstanced => true;
    // ToDo uninstanced and increment burns ?
    
    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        if (player != this.Owner.Player)
        {
            return amount;
        }
        return amount + this.Amount;
    }
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != base.Owner || base.Owner?.Player?.PlayerCombatState == null) 
            return;
        Flash();
        var ownerPlayer = base.Owner.Player;
        if (ownerPlayer != null)
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(this.CombatState.CreateCard<Burn>(this.Owner.Player), PileType.Discard, this.Owner.Player));
    }
}