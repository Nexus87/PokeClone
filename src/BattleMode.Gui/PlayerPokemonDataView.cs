using BattleMode.Shared;
using GameEngine.GUI.Components;
using GameEngine.GUI.Controlls;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameType]
    public class PlayerPokemonDataView : PokemonDataView
    {
        public PlayerPokemonDataView(HpLine line, Label nameBox, Label levelBox, Label hpBox, HpText hpTextBox) :
            base(line, nameBox, levelBox, hpBox, hpTextBox)
        { }
    }
}