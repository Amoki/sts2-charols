using BaseLib.Abstracts;
using BaseLib.Extensions;
using Charolais.CharolaisCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Logging;

namespace Charolais.CharolaisCode.Powers;

public abstract class CharolaisPower : CustomPowerModel
{
    //Loads from Charolais/images/powers/your_power.png
    public override string CustomPackedIconPath
    {
        get
        {
            Log.Warn("path to pint power image");
            Log.Warn($"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath());
            return $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}