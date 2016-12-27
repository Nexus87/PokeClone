using Base;
using GameEngine;
using GameEngine.Core;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.GUI;
using GameEngine.GUI.Graphics.TableView;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameType]
    public class PokemonMenuWidget : AbstractMenuWidget<Pokemon>
    {
        public PokemonMenuWidget(PokemonModel model, PokemonTableRenderer renderer, TableSingleSelectionModel selection, Pixel pixel, ScreenConstants constants, IGameTypeRegistry registry) :
            base(new TableWidget<Pokemon>(null, null, model, selection, registry), new Dialog(pixel), registry)
        {
            pixel.Color = constants.BackgroundColor;
            TableWidget.TableCellFactory = renderer.GetComponent;
        }
    }
}
