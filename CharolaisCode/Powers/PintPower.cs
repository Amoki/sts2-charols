using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Charolais.CharolaisCode.Powers;

public class PintPower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool AllowNegative => false;
    
    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != base.Owner.Side || base.Owner?.Player?.PlayerCombatState == null) return;
        
        Flash();
        await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null);
        
        var alcoolPower = base.Owner.GetPowerAmount<PintPower>();
        
        switch (alcoolPower)
        {
            case >= 12:
                Flash();
                await CreatureCmd.Damage(choiceContext, base.Owner, 3m, ValueProp.Unblockable | ValueProp.Unpowered, base.Owner);
                base.SetAmount(0);
                break;
            case >= 8:
                Flash();
                await CreatureCmd.Damage(choiceContext, base.Owner, 2m, ValueProp.Unblockable | ValueProp.Unpowered, base.Owner);
                break;
            case >= 4:
                Flash();
                await CreatureCmd.Damage(choiceContext, base.Owner, 1m, ValueProp.Unblockable | ValueProp.Unpowered, base.Owner);
                break;
        }
    }
}