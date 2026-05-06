using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;


namespace Charolais.CharolaisCode.Powers;


public sealed class LoubardeObservatoirePower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool IsInstanced => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(2)];
    
    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
    {
        if (player != base.Owner.Player)
            return;
        if (base.Amount > 1)
        {
            await PowerCmd.Decrement(this);
            return;
        }
        Flash();
        await Cmd.Wait(0.2f);
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, player);
        await PowerCmd.Remove(this);
    }
}
