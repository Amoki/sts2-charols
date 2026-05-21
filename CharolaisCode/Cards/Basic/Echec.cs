using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Cards.Basic;

public class Echec() : CharolaisCard(1, CardType.Skill, CardRarity.Basic, TargetType.AnyEnemy)
{
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Power", 2m)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        (HoverTipFactory.FromPower<ChestPower>()),
        (HoverTipFactory.FromPower<VulnerablePower>())
    ];
    
    protected override bool ShouldGlowGoldInternal
    {
        get
        {
            var combatState = this.CombatState;
            return combatState != null && combatState.HittableEnemies.Any(e => e.HasPower<ChestPower>());
        }
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (this.IsUpgraded)
        {
            await PowerCmd.Apply<VulnerablePower>(choiceContext,
                cardPlay.Target ?? throw new InvalidOperationException(), this.DynamicVars["Power"].IntValue,
                this.Owner.Creature, this);
            await CheckmateAction.ExecuteCheckmate(choiceContext, cardPlay);
        }
        else
        {
            await PowerCmd.Apply<VulnerablePower>(choiceContext,
                cardPlay.Target ?? throw new InvalidOperationException(), this.DynamicVars["Power"].IntValue,
                this.Owner.Creature, this);
            await CheckAction.ExecuteCheck(choiceContext, cardPlay);
        }
            
    }

    protected override void OnUpgrade()
    {
        
    }
}
