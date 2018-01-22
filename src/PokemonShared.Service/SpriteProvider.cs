using GameEngine.Graphics.Textures;

namespace PokemonShared.Service
{
    public class SpriteProvider
    {
        private readonly ITextureProvider _textureProvider;

        public SpriteProvider(ITextureProvider textureProvider)
        {
            _textureProvider = textureProvider;
        }


        public ITexture2D GetTexturesFront(int id)
        {
            return id == -1 ? null : _textureProvider.GetTexture("charmander-front");
        }

        public ITexture2D GetTextureBack(int id)
        {
            return id == -1 ? null : _textureProvider.GetTexture("charmander-back");
        }

        public ITexture2D GetIcon(int id)
        {
            return GetTexturesFront(id);
        }
    }
}
