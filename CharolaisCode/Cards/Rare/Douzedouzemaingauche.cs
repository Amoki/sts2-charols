using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Douzedouzemaingauche() : CharolaisCard(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => [PetanqueTag.Petanque];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Power",1)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
        HoverTipFactory.FromPower<StrengthPower>(),
        HoverTipFactory.FromPower<DexterityPower>()
        
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var playerstrength = base.Owner.Creature.GetPowerAmount<StrengthPower>();
        var playerdexterity = base.Owner.Creature.GetPowerAmount<DexterityPower>();
        if (IsUpgraded)
        {
            CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
            CardModel? card = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this)).FirstOrDefault();
            if (card == null)
                return;
            await CardCmd.Exhaust(choiceContext, card);
        }
        
        else
        {
            var pile = PileType.Hand.GetPile(Owner);
            var card = Owner.RunState.Rng.CombatCardSelection.NextItem(pile.Cards);
            if (card == null)
                return;
            await CardCmd.Exhaust(choiceContext, card);
        }
        
        await PowerCmd.Apply<DexterityPower>(choiceContext, this.Owner.Creature, -playerdexterity, this.Owner.Creature, this);
        await PowerCmd.Apply<DexterityPower>(choiceContext, this.Owner.Creature, playerstrength, this.Owner.Creature, this);
        await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner.Creature, -playerstrength, this.Owner.Creature, this);
        await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner.Creature, playerdexterity, this.Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        
    }
}