#nullable enable
using BaseLib.Abstracts;
using Charolais.CharolaisCode.Cards.Uncommon;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Charolais.CharolaisCode.Powers;

public class LancerdecochonnetPower : TemporaryDexterityPower, ICustomModel
{
    public override AbstractModel OriginModel => (AbstractModel) ModelDb.Card<Lancerdecochonnet>();
}
