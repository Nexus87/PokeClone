using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.Registry;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class AIPokemonDataView : PokemonDataView
    {
        public AIPokemonDataView(HpLine line, TextBox nameBox, TextBox levelBox, TextBox hpBox) :
            base(line, nameBox, levelBox, hpBox)
        { }
    }
}