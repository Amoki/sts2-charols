using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace Charolais.CharolaisCode.Cards.Rare;


public class RangementFinal() : CharolaisCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(2)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var currentRoom = base.CombatState!.RunState.CurrentRoom;
        if (currentRoom is CombatRoom combatRoom)
        {
            var cards = base.Owner.UnlockState.CharacterCardPools.Where((p) => p != base.Owner.Character.CardPool).ToList().StableShuffle(base.Owner.RunState.Rng.Niche).Take(3);
            var options = new CardCreationOptions(cards, CardCreationSource.Other, CardRarityOddsType.RegularEncounter).WithFlags(CardCreationFlags.IsCardReward);
            combatRoom.AddExtraReward(base.Owner, new CardReward(options, base.DynamicVars.Cards.IntValue, base.Owner));
            await PowerCmd.Apply<RangementFinalPower>(choiceContext, base.Owner.Creature, 1m, base.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Cards.UpgradeValueBy(1M);
    }
}