using System.Diagnostics;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Echecetmat() : CharolaisCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(10M, ValueProp.Move),
        new DynamicVar("Power",1M)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<ChestPower>(),
        HoverTipFactory.FromPower<ShrinkPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await DamageCmd.Attack(this.DynamicVars.Damage.IntValue)
            .FromCard(this).TargetingAllOpponents(CombatState ?? throw new InvalidOperationException())
            .WithHitFx("vfx/vfx_giant_horizontal_slash")
            .Execute(choiceContext);
        
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        VfxCmd.PlayOnCreatureCenter(this.Owner.Creature, "vfx/vfx_flying_slash");
        
        var enemies = CombatState?.Enemies;
        Debug.Assert(enemies != null, nameof(enemies) + " != null");
        foreach (var enemy in enemies)
        {
            if (!enemy.IsAlive || enemy == this.Owner.Creature) continue;
            await CheckmateAction.ExecuteCheckmate(choiceContext, cardPlay, enemy);
            await PowerCmd.Apply<ShrinkPower>(choiceContext, enemy, this.DynamicVars["Power"].IntValue, this.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.RemoveKeyword(CardKeyword.Exhaust);
        this.DynamicVars.Damage.UpgradeValueBy(3M);
    }
}