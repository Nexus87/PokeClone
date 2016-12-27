using GameEngine.GUI.Graphics;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameType]
    public class PlayerPokemonDataView : PokemonDataView
    {
        public PlayerPokemonDataView(HpLine line, TextBox nameBox, TextBox levelBox, TextBox hpBox, HpText hpTextBox) :
            base(line, nameBox, levelBox, hpBox, hpTextBox)
        { }
    }
}