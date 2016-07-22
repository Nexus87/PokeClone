using Base;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.GraphicComponents
{
    public class PokemonMenuWidget : AbstractMenuWidget<Pokemon>
    {
        public PokemonMenuWidget(TableView<Pokemon> tableView) :
            base(new TableWidget<Pokemon>(null, null, tableView))
        {
        }
    }
}
