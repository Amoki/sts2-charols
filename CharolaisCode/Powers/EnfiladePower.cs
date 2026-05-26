using System.Threading.Tasks;
using Charolais.CharolaisCode.Cards.Token;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Charolais.CharolaisCode.Powers;

public class EnfiladePower : CharolaisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
       
        List<CardModel> selection = (await CardSelectCmd.FromHand(choiceContext, this.Owner.Player!, new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, Amount), null, (AbstractModel) this)).ToList<CardModel>();
        foreach (CardModel original in selection)
        {
            CardPileAddResult? nullable = await CardCmd.TransformTo<Couptranquille>(original);
        }
        selection = (List<CardModel>) null!;
    }
}
