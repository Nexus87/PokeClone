using GameEngine.Globals;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.TypeRegistry;
using MainMode.Core;
using MainMode.Core.Graphics;

namespace PokemonGame.MainModuleClasses
{
    [GameService(typeof(IMapLoader))]
    public class DummyMapLoader : IMapLoader
    {
        private readonly TextureProvider _textureProvider;
        private readonly IGameTypeRegistry _registry;
        private Table<IGuiComponent> _table;

        public DummyMapLoader(TextureProvider textureProvider, IGameTypeRegistry registry)
        {
            _textureProvider = textureProvider;
            _registry = registry;
        }
        public IMapGraphic LoadMap(Map map)
        {
            var tiles = map.Tiles;
            _table = new Table<IGuiComponent>();

            for (var i = 0; i < tiles.Rows; i++)
            {
                for (var j = 0; j < tiles.Columns; j++)
                {
                    var image = _registry.ResolveType<ImageBox>();
                    image.Image = _textureProvider.GetTexture(MainModule.Key, tiles[i, j].TextureName);
                    _table[i, j] = image;
                }
            }
            return new MapGraphic(_table);
        }

        public ITable<IGuiComponent> GetFieldTextures()
        {
            return _table;
        }
    }
}