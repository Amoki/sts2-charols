using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Picolo() : CharolaisCard(1, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<PicoloPower>(choiceContext, this.Owner.Creature, this.DynamicVars.Cards.BaseValue, this.Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Retain);
    }
}