using Charolais.CharolaisCode.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Charolais.CharolaisCode.Cards.Basic;

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

using System.Collections.Generic;
using System.Threading.Tasks;




public class Beer() : CharolaisCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    // protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    // [
    //     HoverTipFactory.FromPower<DexterityPower>(), HoverTipFactory.FromPower<ThornsPower>()
    // ];
    // 
    // public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Sly];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10, ValueProp.Move), new AlcoolVar(3)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_slash")
                    .Execute(choiceContext);
        await PowerCmd.Apply<AlcoolPower>(choiceContext, this.Owner.Creature, DynamicVars["Alcool"].IntValue, this.Owner.Creature, null);
        await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
        // await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        // await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this);
        // await PowerCmd.Apply<ThornsPower>(base.Owner.Creature, base.DynamicVars["ThornsPower"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(3m);
    }
}