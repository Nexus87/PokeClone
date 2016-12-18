using Base;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.TableView;
using GameEngine.Registry;
using GameEngine.Utils;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class PokemonTableRenderer
    {
        private readonly IGameTypeRegistry _registry;
        private readonly Table<SelectableContainer<PokemonMenuLine>> _components = new Table<SelectableContainer<PokemonMenuLine>>();

        public PokemonTableRenderer(IGameTypeRegistry registry)
        {
            _registry = registry;
        }

        public IGraphicComponent GetComponent(int row, int column, Pokemon data)
        {
            var component = _components[row, column];
            if (component == null)
            {
                component = _registry.ResolveType<SelectableContainer<PokemonMenuLine>>();
                component.Content = _registry.ResolveType<PokemonMenuLine>();
                _components[row, column] = component;
                component.Setup();
            }

            component.Content.SetPokemon(data);

            return component;
        }
    }
}
