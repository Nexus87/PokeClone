using BattleLib.Components.GraphicComponents;
using GameEngine.Graphics;
using GameEngine.Registry;

namespace BattleLib.GraphicComponents
{
    [GameType]
    public class PlayerPokemonDataView : PokemonDataView
    {
        public PlayerPokemonDataView(HPLine line, TextBox nameBox, TextBox levelBox, TextBox hpBox, HPText hpTextBox) :
            base(line, nameBox, levelBox, hpBox, hpTextBox)
        { }
    }
}