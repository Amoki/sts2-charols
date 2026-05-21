using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Lebol() : CharolaisCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Power", 4),
        new CardsVar(1)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<PintPower>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var pintPower = this.Owner.Creature.GetPower<PintPower>();
        if (pintPower != null)
        {
            pintPower.SkipReset = true;
        }
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, DynamicVars["Power"].IntValue, this.Owner.Creature, this);
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        int selectCount = Math.Min(this.DynamicVars.Cards.IntValue, CardPile.MaxCardsInHand - PileType.Hand.GetPile(this.Owner).Cards.Count);
        if (selectCount <= 0)
            return;
        await CardPileCmd.Add(await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Discard.GetPile(this.Owner).Cards, this.Owner, new CardSelectorPrefs(this.SelectionScreenPrompt, selectCount)), PileType.Hand);
    }
    
    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Retain);
    }
}