using BaseLib.Abstracts;
using Charolais.CharolaisCode.Extensions;
using Godot;

namespace Charolais.CharolaisCode.Character;

public class CharolaisRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Charolais.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}