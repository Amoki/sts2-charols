using Godot;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;


namespace Charolais.CharolaisCode.Powers;

public class ChestPower : CharolaisPower
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool AllowNegative => false;
    
    public override Color AmountLabelColor => PowerModel._normalAmountLabelColor;
    
    
    
}