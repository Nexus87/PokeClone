using BattleMode.Shared;
using GameEngine.GUI.Graphics;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameType]
    public class AiPokemonDataView : PokemonDataView
    {
        public AiPokemonDataView(HpLine line, TextBox nameBox, TextBox levelBox, TextBox hpBox) :
            base(line, nameBox, levelBox, hpBox)
        { }
    }
}