using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;


namespace Charolais.CharolaisCode.Relics;

public class Tireuse() : CharolaisRelic
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
        await PowerCmd.Apply<AlcoolPower>(choiceContext, this.Owner.Creature, 1, this.Owner.Creature, null);
    }
}