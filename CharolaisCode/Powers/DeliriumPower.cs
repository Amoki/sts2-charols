using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;


namespace Charolais.CharolaisCode.Powers;

public class DeliriumPower : CharolaisPower
{
    
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        if (player != this.Owner.Player)
            return;
        Flash();
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner, base.Amount, this.Owner, null);
    }
}
