using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Charolais.CharolaisCode.Cards;


public static class CheckmateAction
{
    public const string Key = "Échec";
    public static async Task ExecuteCheckmate(PlayerChoiceContext context, CardPlay cardPlay, Creature? targetOverride = null)
    {
        var target = targetOverride ?? cardPlay.Target;
        if (target == null || !target.IsAlive) return;
        int amount = target.GetPowerAmount<ChestPower>();
        if (amount > 0)
        {
            await DamageCmd.Attack(amount)
                .FromCard(cardPlay.Card)
                .WithHitCount(1)
                .Targeting(target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(context);
            
            await PowerCmd.Apply<ChestPower>(context, target, -amount, cardPlay.Card.Owner.Creature, cardPlay.Card);
        }
    }
}
