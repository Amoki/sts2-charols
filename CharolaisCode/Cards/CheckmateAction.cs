using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Charolais.CharolaisCode.Cards;


public static class CheckmateAction
{
    public const string Key = "Échec";
    public static async Task ExecuteCheckmate(PlayerChoiceContext context, CardPlay cardPlay, Creature? targetOverride = null)
    {
        var target = targetOverride ?? cardPlay.Target;
        if (target == null || !target.IsAlive) return;
        var amount = target.GetPowerAmount<ChestPower>();
        if (amount > 0)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(amount)
                .Unpowered()
                .FromCard(cardPlay.Card)
                .WithHitCount(1)
                .Targeting(target)
                .WithAttackerAnim("Cast", 1f)
                .WithAttackerFx(() => NMinionDiveBombVfx.Create(cardPlay.Card.Owner.Creature, cardPlay.Target))
                .Execute(context);
            
            await PowerCmd.Apply<ChestPower>(context, target, -amount, cardPlay.Card.Owner.Creature, cardPlay.Card);
        }
    }
}
