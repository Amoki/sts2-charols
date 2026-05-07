using System.Diagnostics;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Legambitduroi() : CharolaisCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DynamicVar("Power",12M)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<ChestPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target != null)
            await PowerCmd.Apply<ChestPower>(choiceContext, cardPlay.Target, this.DynamicVars["Power"].IntValue, this.Owner.Creature, this);


        Debug.Assert(cardPlay.Target != null, "cardPlay.Target != null");
        var amount = cardPlay.Target.GetPowerAmount<ChestPower>();

        await CheckmateAction.ExecuteCheckmate(choiceContext, cardPlay);
        
        if (cardPlay.Target != null)
            await PowerCmd.Apply<ChestPower>(choiceContext, cardPlay.Target, amount, this.Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars["Power"].UpgradeValueBy(6M);
    }
}