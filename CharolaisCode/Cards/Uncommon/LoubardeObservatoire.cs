using Charolais.CharolaisCode.Cards;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Uncommon;


public class LoubardeObservatoire() : CharolaisCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllAllies)
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new("Turns", 3m), new CardsVar(2)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var appliedTurn  = new Random().Next(1,  base.DynamicVars["Turns"].IntValue + 1);
        
        IEnumerable<Creature> enumerable = from c in base.CombatState?.GetTeammatesOf(base.Owner.Creature)
            where c != null && c.IsAlive && c.IsPlayer
            select c;
        
        foreach (Creature item in enumerable)
        {
            await PowerCmd.Apply<LoubardeObservatoirePower>(choiceContext, item.Player!.Creature, appliedTurn, this.Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
        base.DynamicVars["Turns"].UpgradeValueBy(-1m);
    }
}