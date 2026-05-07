using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Common;

public class Enpassant() : CharolaisCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.AnyEnemy)
{

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new ChestVar(10)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<ChestPower>(choiceContext, cardPlay.Target ?? throw new InvalidOperationException(), DynamicVars["Chest"].IntValue, this.Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars["Chest"].UpgradeValueBy(4M);
    }
}