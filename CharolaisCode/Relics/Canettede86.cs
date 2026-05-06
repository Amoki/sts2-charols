using BaseLib.Utils;
using Charolais.CharolaisCode.Character;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace Charolais.CharolaisCode.Relics;

[Pool(typeof(CharolaisRelicPool))]

public class Canettede86() : CharolaisRelic
{
    private const string _turnsKey = "Turns";
    private bool _isActivating;
    private int _turnsSeen;

    public override string FlashSfx => "event:/sfx/ui/relic_activate_draw";
    
    public override RelicRarity Rarity => RelicRarity.Shop;

    public override bool ShowCounter => true;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    public override int DisplayAmount
    {
        get => !this.IsActivating ? this.TurnsSeen : this.DynamicVars["Turns"].IntValue;
    }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        (DynamicVar)new DynamicVar("Power",1),
        new DynamicVar("Turns", 3M)
    ];

    private bool IsActivating
    {
        get => this._isActivating;
        set
        {
            this.AssertMutable();
            this._isActivating = value;
            this.InvokeDisplayAmountChanged();
        }
    }

    [SavedProperty]
    public int TurnsSeen
    {
        get => this._turnsSeen;
        set
        {
            this.AssertMutable();
            this._turnsSeen = value;
            this.InvokeDisplayAmountChanged();
        }
    }
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != this.Owner)
            return;
        this.TurnsSeen = (this.TurnsSeen + 1) % this.DynamicVars["Turns"].IntValue;
        this.Status = this.TurnsSeen == this.DynamicVars["Turns"].IntValue - 1 ? RelicStatus.Active : RelicStatus.Normal;
        if (this.TurnsSeen != 0)
            return;
        TaskHelper.RunSafely(this.DoActivateVisuals());
        int powerAmount = this.Owner.Creature.GetPowerAmount<PintPower>();
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, decimal.Multiply(2, powerAmount), this.Owner.Creature, null);
    }
    
    private async Task DoActivateVisuals()
    {
        this.IsActivating = true;
        this.Flash();
        await Cmd.Wait(1f);
        this.IsActivating = false;
    }

    public override Task AfterCombatEnd(CombatRoom _)
    {
        this.Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }
}