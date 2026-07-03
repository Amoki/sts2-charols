using System.Collections.Generic;
using System.Threading.Tasks;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Charolais.CharolaisCode.Cards;


public static class CheckmateAction
{
    public const string Key = "Échec et Mat";
    public static async Task ExecuteCheckmate(PlayerChoiceContext context, CardPlay cardPlay, Creature? targetOverride = null)
    {
        var target = targetOverride ?? cardPlay.Target;
        if (target == null || !target.IsAlive) return;
        var amount = target.GetPowerAmount<ChestPower>();
        if (amount > 0)
        {
            await DamageCmd.Attack(amount)
                .Unpowered()
                .FromCard(cardPlay.Card, cardPlay)
                .WithHitCount(1)
                .Targeting(target)
                .WithAttackerAnim("Cast", 1f)
                .WithAttackerFx(() => NMinionDiveBombVfx.Create(cardPlay.Card.Owner.Creature, target))
                .Execute(context);
        }
    }
    
    public static async Task ExecuteCheckmate(PlayerChoiceContext context, CardPlay cardPlay, IReadOnlyList<Creature> creatures)
    {
        foreach (var creature in creatures)
        {
            if (!creature.IsAlive) continue;
            var amount = creature.GetPowerAmount<ChestPower>();
            if (amount > 0)
            {
                await DamageCmd.Attack(amount)
                    .Unpowered()
                    .FromCard(cardPlay.Card, cardPlay)
                    .WithHitCount(1)
                    .Targeting(creature)
                    .WithAttackerAnim("Cast", 1f)
                    .WithAttackerFx(() => NMinionDiveBombVfx.Create(cardPlay.Card.Owner.Creature, creature))
                    .Execute(context);
            }
        }
    }
}
