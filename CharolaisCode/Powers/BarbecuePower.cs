using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Charolais.CharolaisCode.Powers;

public class BarbecuePower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        if (player != this.Owner.Player)
        {
            return amount;
        }
        return amount + this.Amount;
    }
}