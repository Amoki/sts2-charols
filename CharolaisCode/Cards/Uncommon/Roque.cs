using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Roque() : CharolaisCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AllEnemies)
{

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new ChestVar(8)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        VfxCmd.PlayOnCreatureCenter(this.Owner.Creature, "vfx/vfx_flying_slash");
        var combatStateHittableEnemies = this.CombatState?.HittableEnemies;
        if (combatStateHittableEnemies == null)
            return;
        foreach (var enemy in combatStateHittableEnemies)
        {
            await PowerCmd.Apply<ChestPower>(choiceContext, enemy, DynamicVars["Chest"].IntValue, this.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars["Chest"].UpgradeValueBy(4M);
    }
}