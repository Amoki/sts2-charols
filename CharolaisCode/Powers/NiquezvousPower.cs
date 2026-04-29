using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Powers;

public class NiquezvousPower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override bool IsInstanced => true;
    // ToDo uninstanced and increment heal and frail ?
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {

        Flash();
        await CreatureCmd.Heal(base.Owner, 2m);
        
        var players = base.CombatState?.Players;
        if (players == null) { return; }
        
        int index = new Random().Next(players.Count);
        Player randomPlayer = players[index];
        
        if (player.Creature != base.Owner || base.Owner?.Player?.PlayerCombatState == null) return;
        {
            await PowerCmd.Apply<FrailPower>(choiceContext, randomPlayer.Creature, 1, base.Owner, (CardModel?)null);
        }
    }
}
