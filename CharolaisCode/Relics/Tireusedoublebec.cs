using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;


namespace Charolais.CharolaisCode.Relics;

public class Tireusedoublebec : CharolaisRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    
    public override async Task BeforeHandDraw(
        Player player,
        PlayerChoiceContext choiceContext,
        ICombatState combatState)
    {
        if (player != this.Owner)
            return;
        Flash();
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, 2, this.Owner.Creature, null);
    }
}