using BaseLib.Utils;
using Charolais.CharolaisCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace Charolais.CharolaisCode.Relics;

[Pool(typeof(CharolaisRelicPool))]

public class Pileouface() : CharolaisRelic
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>(),
        HoverTipFactory.FromPower<DexterityPower>()
    ];
    
    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        
        if (base.Owner.RunState.Rng.CombatTargets.NextBool())
        {
            await PowerCmd.Apply<StrengthPower>((PlayerChoiceContext)new ThrowingPlayerChoiceContext(),
                this.Owner.Creature, 1, this.Owner.Creature, null);
        }
        else
        {
            await PowerCmd.Apply<DexterityPower>((PlayerChoiceContext)new ThrowingPlayerChoiceContext(),
                this.Owner.Creature, 1, this.Owner.Creature, null);
        }
    }
}