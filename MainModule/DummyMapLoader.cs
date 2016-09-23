using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using GameEngine.Utils;

namespace MainModule
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
        public void LoadMap()
        {
            factory.Setup();
            table = new Table<IGraphicComponent>();
            table[0, 0] = factory.CreateSpriteSheetTexture("Tile0303");
            table[0, 1] = factory.CreateSpriteSheetTexture("Tile0304");
            table[0, 2] = factory.CreateSpriteSheetTexture("Tile0305");
            table[1, 0] = factory.CreateSpriteSheetTexture("Tile0403");
            table[1, 1] = factory.CreateSpriteSheetTexture("Tile0404");
            table[1, 2] = factory.CreateSpriteSheetTexture("Tile0405");
            table[2, 0] = factory.CreateSpriteSheetTexture("Tile0503");
            table[2, 1] = factory.CreateSpriteSheetTexture("Tile0504");
            table[2, 2] = factory.CreateSpriteSheetTexture("Tile0505");
        }

        public ITable<IGraphicComponent> GetFieldTextures()
        {
            return table;
        }
    }
}