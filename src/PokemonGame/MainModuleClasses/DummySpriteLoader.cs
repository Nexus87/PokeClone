using GameEngine.Core;
using GameEngine.Core.TextureLoader;
using GameEngine.TypeRegistry;
using MainMode.Core;
using MainMode.Core.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PokemonGame.MainModuleClasses
{
    [GameService(typeof(ISpriteLoader))]
    public class DummySpriteLoader : ISpriteLoader
    {
        private readonly SpriteSheetFactory factory;

        public DummySpriteLoader(ContentManager contentManager)
        {
            var provider = new JsonSpriteSheetProvider("Characters Overworld", "Content/CharactersMapping.json", contentManager);
            factory = new SpriteSheetFactory(provider);
        }
        public ICharacterSprite GetSprite(string spriteName)
        {
            var left = factory.CreateSpriteSheetTexture(spriteName + "Left");
            var right = factory.CreateSpriteSheetTexture(spriteName + "Left");
            var up = factory.CreateSpriteSheetTexture(spriteName + "Up");
            var down = factory.CreateSpriteSheetTexture(spriteName + "Down");

            right.SpriteEffects = SpriteEffects.FlipHorizontally;
            return new CharacterSprite(left, right, up, down);
        }

        public void Setup()
        {
            factory.Setup();
        }
    }
}