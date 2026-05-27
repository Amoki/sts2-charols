using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        new DynamicVar("Power", 28)
    ];

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<ChestPower>()
    ];
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        Creature player = this.Owner.Creature;
        IReadOnlyList<Creature>? targets = player.CombatState!.HittableEnemies;
        foreach (Creature target1 in (IEnumerable<Creature>) targets)
        {
            NCombatRoom? instance = NCombatRoom.Instance;
            if (instance != null)
                instance.CombatVfxContainer.AddChildSafely((Node) NSmokePuffVfx.Create(target1, NSmokePuffVfx.SmokePuffColor.Purple)!);
        }
        await Cmd.CustomScaledWait(0.2f, 0.4f);
        IEnumerable<ChestPower> powerResults = await PowerCmd.Apply<ChestPower>(choiceContext, (IEnumerable<Creature>) targets, (Decimal) this.DynamicVars["Power"].IntValue, player, null);
        player = (Creature) null!;
        targets = (IReadOnlyList<Creature>) null!;
    }
}