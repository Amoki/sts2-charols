using System.Collections.Generic;
using System.Threading.Tasks;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Rare;


public class Enfilade() : CharolaisCard(1, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(2)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (this.IsUpgraded)
        {
            await CardPileCmd.Draw(choiceContext, this.DynamicVars.Cards.BaseValue, this.Owner);
            await PowerCmd.Apply<EnfiladePower>(choiceContext, this.Owner.Creature, 1, this.Owner.Creature, this);
        }
        else
        {
            await PowerCmd.Apply<EnfiladePower>(choiceContext, this.Owner.Creature, 1, this.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        
    }
}