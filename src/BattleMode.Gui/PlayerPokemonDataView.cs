using GameEngine.Core;
using GameEngine.GUI.Components;
using GameEngine.GUI.Controlls;
using GameEngine.Pokemon.Gui;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameType]
    public class PlayerPokemonDataView : PokemonDataView
    {
        public PlayerPokemonDataView(GuiManager guiManager, HpLine line, Label nameBox, Label levelBox, Label hpBox, HpText hpTextBox) :
            base(line, nameBox, levelBox, hpBox, hpTextBox, guiManager)
        { }
    }
}