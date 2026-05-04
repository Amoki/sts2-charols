using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Ladefensesicilienne() : CharolaisCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new BlockVar(7m, ValueProp.Move),
        new DynamicVar("Power", 7M)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<ChestPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        int amount = this.DynamicVars["Power"].IntValue;
        var randomEnemy = CombatState?.Enemies.Where(e => e.IsAlive).OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
        if (randomEnemy != null)
            await PowerCmd.Apply<ChestPower>(choiceContext, randomEnemy, amount, cardPlay.Card.Owner.Creature, cardPlay.Card);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(3M);
        this.DynamicVars["Power"].UpgradeValueBy(3M);
    }
}