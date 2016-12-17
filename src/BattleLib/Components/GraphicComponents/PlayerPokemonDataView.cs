using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.Registry;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class PlayerPokemonDataView : PokemonDataView
    {
        public PlayerPokemonDataView(HpLine line, TextBox nameBox, TextBox levelBox, TextBox hpBox, HPText hpTextBox) :
            base(line, nameBox, levelBox, hpBox, hpTextBox)
        { }
    }
}