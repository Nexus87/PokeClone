using GameEngine.Graphics;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;

namespace GameEngine.Core
{
    [GameService(typeof(SpriteProvider))]
    public class SpriteProvider
    {
        private readonly TextureProvider _textureProvider;

        public SpriteProvider(TextureProvider textureProvider)
        {
            _textureProvider = textureProvider;
        }


        public ITexture2D GetTexturesFront(int id)
        {
            return id == -1 ? null : _textureProvider.GetTexture(GameEngineModule.Key, "charmander-front");
        }

        public ITexture2D GetTextureBack(int id)
        {
            return id == -1 ? null : _textureProvider.GetTexture(GameEngineModule.Key, "charmander-back");
        }

        public ITexture2D GetIcon(int id)
        {
            return GetTexturesFront(id);
        }
    }
}
