using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Powers;

public class LecerclePower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<PintPower>(1)
    ];

    public override async Task AfterPlayerTurnStartLate(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        int powerAmount = this.Owner.GetPowerAmount<PintPower>();
        if (powerAmount >= 0)
        {
            Flash();
            await PowerCmd.Apply<PintPower>(choiceContext, this.Owner, Decimal.Negate(powerAmount), this.Owner, null);
        }
    }
}