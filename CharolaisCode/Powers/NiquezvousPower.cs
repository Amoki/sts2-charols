using System;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Powers;

public class NiquezvousPower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != base.Owner.Player)
            return;
        
        Flash();
        await CreatureCmd.Heal(base.Owner, decimal.Multiply(base.Amount, 3));
        var players = base.CombatState?.Players;
        if (players == null) { return; }
        var randomPlayer = base.CombatState?.RunState.Rng.CombatTargets.NextItem(players) ?? throw new InvalidOperationException();
        
        if (player.Creature != base.Owner || base.Owner?.Player?.PlayerCombatState == null) return;
        {
            await PowerCmd.Apply<FrailPower>(choiceContext, randomPlayer.Creature, base.Amount, base.Owner, null);
        }
    }
}
