using BaseLib.Utils;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Token;

[Pool(typeof(TokenCardPool))]
public class Couptranquille() : CharolaisCard(0, CardType.Skill, CardRarity.Token, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ChestVar(6M),
        new CardsVar(1)
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain,
        CardKeyword.Exhaust
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<ChestPower>(choiceContext, cardPlay.Target ?? throw new InvalidOperationException(), DynamicVars["Chest"].IntValue, this.Owner.Creature, this);
        await CardPileCmd.Draw(choiceContext, this.DynamicVars.Cards.BaseValue, this.Owner);
    }
    
    public override async Task AfterSideTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Player && this.Pile!.Type == PileType.Hand)
        {
            if (this.IsUpgraded)
            {
                this.DynamicVars["Chest"].UpgradeValueBy(3M);
            }
            else
            {
                this.DynamicVars["Chest"].UpgradeValueBy(2M);
            }
        }
    }

    
    protected override void OnUpgrade()
    {
        this.DynamicVars["Chest"].UpgradeValueBy(4M);
    }
}