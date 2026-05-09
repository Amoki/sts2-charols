using Charolais.CharolaisCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Charolais.CharolaisCode.Potions;

public class Potiondechec : CharolaisPotion
{
    public override PotionRarity Rarity => PotionRarity.Rare;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AllEnemies;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Power", 26)
    ];

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<ChestPower>()
    ];
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        PotionModel.AssertValidForTargetedPotion(target);
        if (CombatSide.Player != this.Owner.Creature.Side)
            return;
        
        foreach (Creature hittableEnemy in (IEnumerable<Creature>) this.Owner.Creature.CombatState!.HittableEnemies)
        {
            NCombatRoom? instance = NCombatRoom.Instance;
            if (instance != null)
                instance.CombatVfxContainer.AddChildSafely((Node) NSmokePuffVfx.Create(hittableEnemy, NSmokePuffVfx.SmokePuffColor.Purple)!);
        }
        await Cmd.CustomScaledWait(0.2f, 0.4f);
        foreach (Creature hittableEnemy in (IEnumerable<Creature>) this.Owner.Creature.CombatState.HittableEnemies)
        {
            await PowerCmd.Apply<ChestPower>(choiceContext, hittableEnemy, (Decimal) this.DynamicVars["Power"].IntValue, this.Owner.Creature, null);
        }
    }
}