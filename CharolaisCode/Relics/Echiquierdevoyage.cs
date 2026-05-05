using BaseLib.Utils;
using Charolais.CharolaisCode.Character;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Relics;

[Pool(typeof(CharolaisRelicPool))]
public class Echiquierdevoyage() : CharolaisRelic
{
  public override RelicRarity Rarity => RelicRarity.Uncommon;
  
  public override Task BeforeCombatStart()
  {
    this.Status = RelicStatus.Active;
    return Task.CompletedTask;
  }
  
  protected override IEnumerable<IHoverTip> ExtraHoverTips =>
  [
    HoverTipFactory.FromPower<ChestPower>()
  ];
  
  public override Decimal ModifyPowerAmountGiven(
    PowerModel power,
    Creature giver,
    Decimal amount,
    Creature? target,
    CardModel? cardSource)
  {
    if (this.Status == RelicStatus.Active && 
        power is ChestPower && 
        giver == this.Owner.Creature && 
        target != null && target.Side != this.Owner.Creature.Side)
    {
      this.Status = RelicStatus.Normal; 
      this.Flash();
        
      return amount + 5M;
    }
    return amount;
  }
}