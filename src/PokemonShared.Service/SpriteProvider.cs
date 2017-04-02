using GameEngine.Graphics.Textures;

namespace PokemonShared.Service
{
    public class SpriteProvider
    {
        private readonly TextureProvider _textureProvider;
        private readonly object _resourceKey;

        public SpriteProvider(TextureProvider textureProvider, object resourceKey)
        {
            _textureProvider = textureProvider;
            _resourceKey = resourceKey;
        }


        public ITexture2D GetTexturesFront(int id)
        {
            return id == -1 ? null : _textureProvider.GetTexture(_resourceKey, "charmander-front");
        }

        public ITexture2D GetTextureBack(int id)
        {
            return id == -1 ? null : _textureProvider.GetTexture(_resourceKey, "charmander-back");
        }

        public ITexture2D GetIcon(int id)
        {
            return GetTexturesFront(id);
        }
    }
}
