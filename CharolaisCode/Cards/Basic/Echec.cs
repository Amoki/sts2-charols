using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Cards.Basic;

public class Echec() : CharolaisCard(0, CardType.Skill, CardRarity.Basic, TargetType.AnyEnemy)
{
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<WeakPower>(1m),
        new PowerVar<ChestPower>(1m)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        (HoverTipFactory.FromPower<ChestPower>()),
        (HoverTipFactory.FromPower<WeakPower>())
    ];
    
    protected override bool ShouldGlowGoldInternal
    {
        get
        {
            ICombatState? combatState = this.CombatState;
            return combatState != null && combatState.HittableEnemies.Any<Creature>((Func<Creature, bool>) (e => e.HasPower<ChestPower>()));
        }
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<WeakPower>(choiceContext, cardPlay.Target ?? throw new InvalidOperationException(), this.DynamicVars.Weak.BaseValue, this.Owner.Creature, this);
        await CheckmateAction.ExecuteCheckmate(choiceContext, cardPlay);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Weak.UpgradeValueBy(1M);
    }
}
