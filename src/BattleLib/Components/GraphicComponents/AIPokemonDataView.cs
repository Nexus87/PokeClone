using GameEngine.GUI.Graphics;
using GameEngine.Registry;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class AiPokemonDataView : PokemonDataView
    {
        public AiPokemonDataView(HpLine line, TextBox nameBox, TextBox levelBox, TextBox hpBox) :
            base(line, nameBox, levelBox, hpBox)
        { }
    }
}