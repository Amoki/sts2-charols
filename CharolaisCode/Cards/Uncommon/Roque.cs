using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

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
        Roque cardSource = this;
        await CreatureCmd.TriggerAnim(cardSource.Owner.Creature, "Cast", cardSource.Owner.Character.CastAnimDelay);
        VfxCmd.PlayOnCreatureCenter(cardSource.Owner.Creature, "vfx/vfx_flying_slash");
        foreach (Creature enemy in (IEnumerable<Creature>) cardSource.CombatState?.HittableEnemies)
        {
            await PowerCmd.Apply<ChestPower>(choiceContext, enemy, DynamicVars["Chest"].IntValue, cardSource.Owner.Creature, (CardModel) this);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars["Chest"].UpgradeValueBy(4M);
    }
}