using System;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Powers;

public class MetredeshootersPower : CharolaisPower
{
    private const int _baseCardsLeft = 5;
    private const string _cardsLeftKey = "CardsLeft";
    
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override int DisplayAmount => this.DynamicVars["CardsLeft"].IntValue;
    
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("CardsLeft", 5M)
    ];
    
    protected override object InitInternalData() => (object) new MetredeshootersPower.Data();
    
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        MetredeshootersPower metredeshootersPower = this;
        MetredeshootersPower.Data data;
        if (cardPlay.Card.Owner != metredeshootersPower.Owner.Player)
        {
            data = null!;
        }
        else
        {
            data = metredeshootersPower.GetInternalData<MetredeshootersPower.Data>();
            if (data.AlreadyApplied)
            {
                --metredeshootersPower.DynamicVars["CardsLeft"].BaseValue;
                metredeshootersPower.InvokeDisplayAmountChanged();
                if (metredeshootersPower.DynamicVars["CardsLeft"].IntValue <= 0)
                {
                    await Cmd.Wait(0.5f);
                    await PowerCmd.Apply<PintPower>(choiceContext, cardPlay.Card.Owner.Creature, 8, cardPlay.Card.Owner.Creature, null);
                    metredeshootersPower.DynamicVars["CardsLeft"].BaseValue = 5M;
                    metredeshootersPower.InvokeDisplayAmountChanged();
                }
            }
            data.AlreadyApplied = true;
            data = null!;
        }
    }
    
    public override Task AfterSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
        if (!participants.Contains<Creature>(this.Owner))
            return Task.CompletedTask;
        this.DynamicVars["CardsLeft"].BaseValue = 5M;
        this.InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }

    private class Data
    {
        public bool AlreadyApplied;
    }
}
