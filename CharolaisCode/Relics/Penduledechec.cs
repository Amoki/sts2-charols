using BaseLib.Utils;
using Charolais.CharolaisCode.Character;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Relics;

[Pool(typeof(CharolaisRelicPool))]
public class Penduledechec() : CharolaisRelic
{
  public override RelicRarity Rarity => RelicRarity.Rare;

  protected override IEnumerable<IHoverTip> ExtraHoverTips =>
  [
    HoverTipFactory.FromPower<ChestPower>()
  ];

  protected override IEnumerable<DynamicVar> CanonicalVars =>
  [
    new DynamicVar("Power",2M)
  ];
  
  public override Decimal ModifyPowerAmountGiven(
    PowerModel power,
    Creature giver,
    Decimal amount,
    Creature? target,
    CardModel? cardSource)
  {
    return !(power is ChestPower) || giver != this.Owner.Creature ? amount : amount + (Decimal) this.DynamicVars["Power"].IntValue;
  }

  public override Task AfterModifyingPowerAmountGiven(PowerModel power)
  {
    this.Flash();
    return Task.CompletedTask;
  }
}
