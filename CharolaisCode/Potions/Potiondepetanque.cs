using Charolais.CharolaisCode.Cards;
using Charolais.CharolaisCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;


namespace Charolais.CharolaisCode.Potions;

public class Potiondepetanque : CharolaisPotion
{
    public override PotionRarity Rarity => PotionRarity.Rare;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Power", 1)
    ];

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>(),
        HoverTipFactory.FromPower<DexterityPower>()
    ];
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        await PowerCmd.Apply<StrengthPower>(choiceContext, target!, this.DynamicVars["Power"].IntValue, this.Owner.Creature, null);
        await PowerCmd.Apply<DexterityPower>(choiceContext, target!, this.DynamicVars["Power"].IntValue, this.Owner.Creature, null);

        var player = target!.Player;
        
        var cardPool = ModelDb.CardPool<CharolaisCardPool>()
            .GetUnlockedCards(player!.UnlockState, player!.RunState.CardMultiplayerConstraint);
        
        var filteredCards = cardPool.Where(c => 
            c.Tags.Contains(PetanqueTag.Petanque)
        );
        
        var cardsToGenerate = CardFactory.GetDistinctForCombat(
            player, 
            filteredCards, 
            1, 
            player.RunState.Rng.CombatCardGeneration
        ).ToList();
        
        foreach (var cardModel in cardsToGenerate)
        {
            cardModel.SetToFreeThisTurn();
        }
        
        await CardPileCmd.AddGeneratedCardsToCombat(cardsToGenerate, PileType.Hand, player);
        
    }
}