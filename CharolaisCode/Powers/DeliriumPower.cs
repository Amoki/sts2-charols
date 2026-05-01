using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;


namespace Charolais.CharolaisCode.Powers;

public class DeliriumPower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool IsInstanced => true;
    // ToDo uninstanced and increment PintPower
    
    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        Flash();
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner, 1, this.Owner, null);
    }
}
