using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Powers;

public class PicoloPower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        if (amount <= 0M || applier != this.Owner || !(power is PintPower))
            return;
        Flash();
        if (Owner.Player != null)
        {
            IEnumerable<CardModel> cardModels =
                await CardPileCmd.Draw(choiceContext, (Decimal)this.Amount, Owner.Player);
        }
    }
}
