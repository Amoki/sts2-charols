using BaseLib.Utils;
using Charolais.CharolaisCode.Character;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Rooms;

namespace Charolais.CharolaisCode.Relics;

[Pool(typeof(CharolaisRelicPool))]

public class Anneaumagique() : CharolaisRelic
{
    public override RelicRarity Rarity => RelicRarity.Common;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (!(room is CombatRoom))
            return;
        Flash();
        await PowerCmd.Apply<PintPower>((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), this.Owner.Creature, 2, this.Owner.Creature, null);
    }
}