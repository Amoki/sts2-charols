using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards.Uncommon;

public class Lebol() : CharolaisCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Power", 4),
        new CardsVar(1)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<PintPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<LeBolPower>(choiceContext, this.Owner.Creature, 1, this.Owner.Creature, this);
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, DynamicVars["Power"].IntValue, this.Owner.Creature, this);
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars["Power"].UpgradeValueBy(3);
    }
}