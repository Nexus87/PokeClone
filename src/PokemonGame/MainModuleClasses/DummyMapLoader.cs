using GameEngine;
using GameEngine.Core;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Utils;
using GameEngine.TypeRegistry;
using MainMode.Core;
using MainMode.Core.Graphics;

namespace PokemonGame.MainModuleClasses
{
    [GameService(typeof(IMapLoader))]
    public class DummyMapLoader : IMapLoader
    {
        private readonly SpriteSheetFactory _factory;
        private Table<IGraphicComponent> _table;

        public DummyMapLoader(SpriteSheetFactory factory)
        {
            _factory = factory;
        }
        public IMapGraphic LoadMap(Map map)
        {
            _factory.Setup();
            var tiles = map.Tiles;
            _table = new Table<IGraphicComponent>();

            for (var i = 0; i < tiles.Rows; i++)
            {
                for (var j = 0; j < tiles.Columns; j++)
                {
                    _table[i, j] = _factory.CreateSpriteSheetTexture(tiles[i, j].TextureName);
                }
            }
            return new MapGraphic(_table);
        }

        public ITable<IGraphicComponent> GetFieldTextures()
        {
            return _table;
        }
    }
}