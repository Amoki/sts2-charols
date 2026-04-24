using Charolais.CharolaisCode.Cards;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Common;



public class Machine() : CharolaisCard(1,
    CardType.Attack, CardRarity.Basic,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(15, ValueProp.Move)];
    
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        
        IReadOnlyList<Player> players = base.CombatState?.Players;
        var alcoolPower = base.Owner.Creature.GetPowerAmount<AlcoolPower>();
        if (players != null && alcoolPower > 0)
        {
            Random random = new Random();
            int index = random.Next(players.Count);
            Player randomPlayer = players[index];
            await Cmd.Wait(0.5f);
            await DamageCmd.Attack(alcoolPower).FromCard(this).Targeting(randomPlayer.Creature)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        }
        
        
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(3m);
    }
}