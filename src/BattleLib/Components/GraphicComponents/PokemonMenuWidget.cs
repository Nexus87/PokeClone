using Base;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
