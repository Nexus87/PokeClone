using GameEngine.Core;
using GameEngine.Core.TextureLoader;
using GameEngine.Graphics;
using GameEngine.GUI.Controlls;
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
        private readonly TextureProvider _textureProvider;
        private readonly IGameTypeRegistry _registry;

        public DummySpriteLoader(TextureProvider textureProvider, IGameTypeRegistry registry)
        {
            _textureProvider = textureProvider;
            _registry = registry;
        }
        public ICharacterSprite GetSprite(string spriteName)
        {
            var left = _registry.ResolveType<ImageBox>();
            var right = _registry.ResolveType<ImageBox>();
            var up = _registry.ResolveType<ImageBox>();
            var down = _registry.ResolveType<ImageBox>();
            left.Image = _textureProvider.GetTexture(MainModule.Key, spriteName + "Left");
            right.Image = _textureProvider.GetTexture(MainModule.Key, spriteName + "Left");
            up.Image = _textureProvider.GetTexture(MainModule.Key, spriteName + "Up");
            down.Image = _textureProvider.GetTexture(MainModule.Key, spriteName + "Down");

            return new CharacterSprite(left, right, up, down);
        }
    }
}