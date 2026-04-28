using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Bitesignal() : CharolaisCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [(DynamicVar) new BlockVar(3m, ValueProp.Move)];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<BitesignalPower>(choiceContext, this.Owner.Creature, this.DynamicVars.Block.BaseValue, this.Owner.Creature,this);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(1);
        this.AddKeyword(CardKeyword.Innate);
    }
}
