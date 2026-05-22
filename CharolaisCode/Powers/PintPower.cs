using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Powers;

public class PintPower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool AllowNegative => false;

    public bool SkipReset = false;

    private int _blockRemainder= 0;
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player == base.Owner.Player)
        {
            Flash();
            var block = decimal.Divide(Amount + _blockRemainder, 2);
            _blockRemainder= (Amount + _blockRemainder) % 2;
            await CreatureCmd.GainBlock(base.Owner, block, ValueProp.Unpowered, null);
        }
    }
    
    public override async Task BeforePowerAmountChanged(
        PowerModel power,
        decimal amount,
        Creature target,
        Creature? applier,
        CardModel? cardSource)
    {
        if (power != this)
            return;
        var block = decimal.Divide(amount + _blockRemainder, 2);
        _blockRemainder= ((int)amount + _blockRemainder) % 2;
        await CreatureCmd.GainBlock(base.Owner, block, ValueProp.Unpowered, null);
    }
    
    public override async Task BeforeSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
        if (side != base.Owner.Side || base.Owner?.Player?.PlayerCombatState == null) return;
        
        var alcoolPower = base.Owner.GetPowerAmount<PintPower>();

        var maxHpToRemove = base.Owner.CurrentHp - 1;
        
        switch (alcoolPower)
        {
            case >= 18:
                Flash();
                await CreatureCmd.Damage(choiceContext, base.Owner, Math.Min(maxHpToRemove, 3), ValueProp.Unblockable | ValueProp.Unpowered, base.Owner);
                if (!SkipReset)
                {
                    base.SetAmount(0);
                }
                else
                {
                    SkipReset = false;
                }
                break;
            case >= 12:
                Flash();
                await CreatureCmd.Damage(choiceContext, base.Owner, Math.Min(maxHpToRemove, 2), ValueProp.Unblockable | ValueProp.Unpowered, base.Owner);
                break;
            case >= 6:
                Flash();
                await CreatureCmd.Damage(choiceContext, base.Owner, Math.Min(maxHpToRemove, 1), ValueProp.Unblockable | ValueProp.Unpowered, base.Owner);
                break;
        }
    }
}