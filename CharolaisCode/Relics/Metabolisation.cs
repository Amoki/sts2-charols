using Charolais.CharolaisCode.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Relics;


public class Metabolisation() : CharolaisRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new StarsVar(1)];
   
    public override async Task AfterStarsGained(int amount, Player gainer)
    {
        if (this.Owner.PlayerCombatState is { Stars: >= 4 } && gainer == base.Owner.Creature.Player)
        {
            PlayerCmd.EndTurn(gainer, false, null);
        }
    }
}