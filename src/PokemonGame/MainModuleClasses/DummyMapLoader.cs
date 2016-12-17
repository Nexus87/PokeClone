using GameEngine;
using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.Registry;
using GameEngine.Utils;
using MainModule;
using MainModule.Graphics;

namespace PokemonGame.MainModuleClasses
{
    [GameService(typeof(IMapLoader))]
    public class DummyMapLoader : IMapLoader
    {
        private readonly SpriteSheetFactory factory;
        private Table<IGraphicComponent> table;

        public DummyMapLoader(SpriteSheetFactory factory)
        {
            this.factory = factory;
        }
        public IMapGraphic LoadMap(Map map)
        {
            factory.Setup();
            var tiles = map.Tiles;
            table = new Table<IGraphicComponent>();

            for (var i = 0; i < tiles.Rows; i++)
            {
                for (var j = 0; j < tiles.Columns; j++)
                {
                    table[i, j] = factory.CreateSpriteSheetTexture(tiles[i, j].TextureName);
                }
            }
            return new MapGraphic(table);
        }

        public ITable<IGraphicComponent> GetFieldTextures()
        {
            return table;
        }
    }
}