using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Charolais.CharolaisCode.Cards.Token;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

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
        if (this.IsUpgraded)
        {
            await PowerCmd.Apply<ChestPower>(choiceContext, cardPlay.Target ?? throw new InvalidOperationException(), DynamicVars["Chest"].IntValue, this.Owner.Creature, this);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat((CardModel) this.CombatState!.CreateCard<Couptranquille>(this.Owner), PileType.Hand, this.Owner));
        }
        else
        {
            await PowerCmd.Apply<ChestPower>(choiceContext, cardPlay.Target ?? throw new InvalidOperationException(), DynamicVars["Chest"].IntValue, this.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        
    }
}