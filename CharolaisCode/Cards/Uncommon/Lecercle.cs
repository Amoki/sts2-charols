using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Lecercle() : CharolaisCard(2,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Power", 1M)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>(),
        HoverTipFactory.FromPower<DexterityPower>(),
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int amount = this.DynamicVars["Power"].IntValue;
        var combatState = this.CombatState;
        if (combatState != null)
        {
            await PowerCmd.Apply<DexterityPower>(choiceContext, this.Owner.Creature,
                (Decimal)amount, this.Owner.Creature, (CardModel)this);
            await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner.Creature,
                (Decimal)amount, this.Owner.Creature, (CardModel)this);
            await PowerCmd.Apply<LecerclePower>(choiceContext, this.Owner.Creature, amount, this.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Innate);
    }
}
