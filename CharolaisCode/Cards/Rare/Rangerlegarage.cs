using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Charolais.CharolaisCode.Cards.Rare;

public class Rangerlegarage() : CharolaisCard(2, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (this.IsUpgraded)
        {
            await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
            await PowerCmd.Apply<RangerlegarageUpgradedPower>(choiceContext, this.Owner.Creature, 1, this.Owner.Creature, this);
        }
        else
        {
            await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
            await PowerCmd.Apply<RangerlegaragePower>(choiceContext, this.Owner.Creature, 1, this.Owner.Creature, this);
        }
    }
    
    protected override void OnUpgrade()
    {
        
    }
}