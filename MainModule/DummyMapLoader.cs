using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework.Content;

namespace MainModule
{
    [GameService(typeof(IMapLoader))]
    public class DummyMapLoader : IMapLoader
    {
        private SpriteSheetFactory factory;
        private Table<IGraphicComponent> table;

        public DummyMapLoader(ContentManager contentManager)
        {
            factory = new SpriteSheetFactory("Tileset", "Content/TilesetMap.txt", contentManager);
        }
        public void LoadMap()
        {
            factory.Setup();
            table = new Table<IGraphicComponent>();
            table[0, 0] = factory.CreateSpriteSheetTexture("Tile33");
            table[0, 1] = factory.CreateSpriteSheetTexture("Tile34");
            table[0, 2] = factory.CreateSpriteSheetTexture("Tile35");
            table[1, 0] = factory.CreateSpriteSheetTexture("Tile43");
            table[1, 1] = factory.CreateSpriteSheetTexture("Tile44");
            table[1, 2] = factory.CreateSpriteSheetTexture("Tile45");
            table[2, 0] = factory.CreateSpriteSheetTexture("Tile53");
            table[2, 1] = factory.CreateSpriteSheetTexture("Tile54");
            table[2, 2] = factory.CreateSpriteSheetTexture("Tile55");
        }

        public ITable<IGraphicComponent> GetFieldTextures()
        {
            return table;
        }
    }
}