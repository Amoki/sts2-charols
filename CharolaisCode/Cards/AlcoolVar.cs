using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards;

public class AlcoolVar : DynamicVar
{
    public const string Key = "Alcool";

    public AlcoolVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}