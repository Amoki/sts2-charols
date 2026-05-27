using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Charolais.CharolaisCode.Cards.Token;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Ladefensecarokann() : CharolaisCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(14m, ValueProp.Move)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<Couptranquille>(this.IsUpgraded)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat((CardModel) this.CombatState!.CreateCard<Couptranquille>(this.Owner), PileType.Hand, this.Owner));
            if (this.IsUpgraded)
                CardCmd.Upgrade(this.CombatState!.CreateCard<Couptranquille>(this.Owner));
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat((CardModel) this.CombatState!.CreateCard<Couptranquille>(this.Owner), PileType.Hand, this.Owner));
            if (this.IsUpgraded)
                CardCmd.Upgrade(this.CombatState!.CreateCard<Couptranquille>(this.Owner));
    }
    
    protected override void OnUpgrade()
    {
        
    }
}