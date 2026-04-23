using BaseLib.Abstracts;
using BaseLib.Utils;
using Charolais.CharolaisCode.Character;

namespace Charolais.CharolaisCode.Potions;

[Pool(typeof(CharolaisPotionPool))]
public abstract class CharolaisPotion : CustomPotionModel;