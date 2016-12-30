using BattleMode.Shared;
using GameEngine.GUI.Components;
using GameEngine.GUI.Controlls;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameType]
    public class AiPokemonDataView : PokemonDataView
    {
        public AiPokemonDataView(HpLine line, Label nameBox, Label levelBox, Label hpBox) :
            base(line, nameBox, levelBox, hpBox)
        { }
    }
}