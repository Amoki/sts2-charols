using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Cards.Common;

public class Proxi() : CharolaisCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(1),
        new DynamicVar("Power",2M)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<VigorPower>())];
    
    private static LocString GetSelectionPrompt => new LocString("card_selection", "TO_GET");
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<VigorPower>(choiceContext, this.Owner.Creature, this.DynamicVars["Power"].IntValue, this.Owner.Creature, this);
        int selectCount = Math.Min(this.DynamicVars.Cards.IntValue, CardPile.MaxCardsInHand - PileType.Hand.GetPile(this.Owner).Cards.Count);
        if (selectCount <= 0)
            return;
        await CardPileCmd.Add(await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Discard.GetPile(this.Owner).Cards, this.Owner, new CardSelectorPrefs(GetSelectionPrompt, selectCount)), PileType.Hand);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars["Power"].UpgradeValueBy(2M);
    }
}