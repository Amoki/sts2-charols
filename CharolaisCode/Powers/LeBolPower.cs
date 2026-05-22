using MegaCrit.Sts2.Core.Entities.Powers;

namespace Charolais.CharolaisCode.Powers;


public class LeBolPower: CharolaisPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
}