using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charolais.CharolaisCode.Cards.Token;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Ladefensesicilienne() : CharolaisCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(7m, ValueProp.Move)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<ChestPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 1);
        CardModel? original = (await CardSelectCmd.FromHand(choiceContext, this.Owner, prefs, null, (AbstractModel) this)).FirstOrDefault<CardModel>();
        if (original == null)
            return;
        CardModel card = (CardModel) this.CombatState!.CreateCard<Couptranquille>(this.Owner);
        if (this.IsUpgraded)
            CardCmd.Upgrade(card);
        CardPileAddResult? nullable = await CardCmd.Transform(original, card);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(4M);
    }
}