using Base;
using GameEngine.Graphics;
using GameEngine.Registry;
using GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Graphics.TableView;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class PokemonTableRenderer : ITableRenderer<Pokemon>
    {
        private IGameTypeRegistry registry;
        private Table<SelectableContainer<PokemonMenuLine>> components = new Table<SelectableContainer<PokemonMenuLine>>();

        public PokemonTableRenderer(IGameTypeRegistry registry)
        {
            this.registry = registry;
        }

        public ISelectableGraphicComponent GetComponent(int row, int column, Pokemon data, bool isSelected)
        {
            var component = components[row, column];
            if (component == null)
            {
                component = registry.ResolveType<SelectableContainer<PokemonMenuLine>>();
                component.Content = registry.ResolveType<PokemonMenuLine>();
                components[row, column] = component;
                component.Setup();
            }

            component.Content.SetPokemon(data);

            if (isSelected)
                component.Select();
            else
                component.Unselect();

            return component;
        }
    }
}
