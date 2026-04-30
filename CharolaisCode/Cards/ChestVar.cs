using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Charolais.CharolaisCode.Cards;

public class ChestVar : DynamicVar
{
    public const string Key = "Chest";

    public ChestVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}