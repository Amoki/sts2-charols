using System;
using BaseLib.Abstracts;
using Charolais.CharolaisCode.Cards.Ancient;
using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Basic;

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

using System.Collections.Generic;
using System.Threading.Tasks;




public class Beer() : CharolaisCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy), ITranscendenceCard
{
    
    public CardModel GetTranscendenceTransformedCard()
    {
        return ModelDb.Card<Futdebiere>();
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(10, ValueProp.Move),
        new DynamicVar("Power", 4)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PintPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_slash")
                    .Execute(choiceContext);
        await PowerCmd.Apply<PintPower>(choiceContext, this.Owner.Creature, DynamicVars["Power"].IntValue, this.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(2m);
        base.DynamicVars["Power"].UpgradeValueBy(2m);
    }
}