using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Common;

public class Bloc() : CharolaisCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new DynamicVar[2]
            {
                (DynamicVar) new BlockVar(6m, ValueProp.Move),
                (DynamicVar) new CardsVar(2)
            };
        }
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, this.DynamicVars.Cards.BaseValue, this.Owner);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(3M);
    }
}