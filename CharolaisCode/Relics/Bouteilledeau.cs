using BaseLib.Utils;
using Charolais.CharolaisCode.Character;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Relics;

[Pool(typeof(CharolaisRelicPool))]

public class Bouteilledeau() : CharolaisRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Power",2M),
        new EnergyVar(2)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    public override async Task AfterSideTurnStart(CombatSide side, ICombatState combatState)
    {
        if (side != this.Owner.Creature.Side) return;
        
        if (combatState.RoundNumber == 2 || combatState.RoundNumber == 4)
        {
            int currentPint = this.Owner.Creature.GetPowerAmount<PintPower>();
            int powerToReduce = (int)this.DynamicVars["Power"].IntValue;
            int amountToRemove = Math.Min(currentPint, powerToReduce);
            if (amountToRemove > 0)
            {
                    await PowerCmd.Apply<PintPower>(null, this.Owner.Creature, Decimal.Negate(amountToRemove),
                        this.Owner.Creature, null);
            }
            await PlayerCmd.GainEnergy(this.DynamicVars.Energy.BaseValue, this.Owner);
        }
    }
}