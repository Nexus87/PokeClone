using Base;
using GameEngine;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.GUI;
using GameEngine.GUI.Graphics.TableView;
using GameEngine.Registry;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class PokemonMenuWidget : AbstractMenuWidget<Pokemon>
    {
        public PokemonMenuWidget(PokemonModel model, PokemonTableRenderer renderer, TableSingleSelectionModel selection, Pixel pixel, ScreenConstants constants) :
            base(new TableWidget<Pokemon>(null, null, model, renderer, selection), new Dialog(pixel))
        {
            pixel.Color = constants.BackgroundColor;
        }
    }
}
