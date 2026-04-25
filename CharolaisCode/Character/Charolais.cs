using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Charolais.CharolaisCode.Cards.Basic;
using Charolais.CharolaisCode.Cards.Common;
using Charolais.CharolaisCode.Extensions;
using Charolais.CharolaisCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Charolais.CharolaisCode.Character;


public class Charolais : PlaceholderCharacterModel
{
    public const string CharacterId = "Charolais";
    
    public override float AttackAnimDelay => 0.15f;

    public override float CastAnimDelay => 0.25f;

    public static readonly Color Color = new("BABB5C");
    public override Color EnergyLabelOutlineColor => new Color("F26A38FF");
    public override int BaseOrbSlotCount => 3;
    public override Color DialogueColor => new Color("13446B");
    public override VfxColor SpeechBubbleColor => VfxColor.Gold;
    public override Color MapDrawingColor => new Color("FABB5C");
    public override Color RemoteTargetingLineColor => new Color("CD94EDFF");
    public override Color RemoteTargetingLineOutline => new Color("CD94EDFF");
    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Masculine;
    public override int StartingHp => 69;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeCharolais>(),
        ModelDb.Card<StrikeCharolais>(),
        ModelDb.Card<StrikeCharolais>(),
        ModelDb.Card<StrikeCharolais>(),
        ModelDb.Card<StrikeCharolais>(),
        ModelDb.Card<DefendCharolais>(),
        ModelDb.Card<DefendCharolais>(),
        ModelDb.Card<DefendCharolais>(),
        ModelDb.Card<DefendCharolais>(),
        ModelDb.Card<DefendCharolais>(),
        ModelDb.Card<Beer>(),
        ModelDb.Card<Burp>(),
        
    ];
    
    
    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<Tireuse>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<CharolaisCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<CharolaisRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<CharolaisPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}
