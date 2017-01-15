using GameEngine.Graphics.Textures;
using GameEngine.GUI.Controlls;
using GameEngine.TypeRegistry;
using MainMode.Core;
using MainMode.Core.Graphics;

namespace PokemonGame.MainModuleClasses
{
    [GameService(typeof(ISpriteLoader))]
    public class DummySpriteLoader : ISpriteLoader
    {
        private readonly TextureProvider _textureProvider;
        private readonly IGameTypeRegistry _registry;

        public DummySpriteLoader(TextureProvider textureProvider, IGameTypeRegistry registry)
        {
            _textureProvider = textureProvider;
            _registry = registry;
        }
        public AbstractCharacterSprite GetSprite(string spriteName)
        {
            var left = _textureProvider.GetTexture(MainModule.Key, spriteName + "Left");
            var right = _textureProvider.GetTexture(MainModule.Key, spriteName + "Left");
            var up = _textureProvider.GetTexture(MainModule.Key, spriteName + "Back");
            var down = _textureProvider.GetTexture(MainModule.Key, spriteName + "Front");

            return new CharacterSprite(left, right, up, down);
        }
    }
}