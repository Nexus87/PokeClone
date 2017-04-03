using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Textures
{
    public class TextureProvider
    {
        public ITexture2D Pixel => _pixel;

        internal void SetConfiguration(TextureProviderConfiguration configuration, ContentManager contentManager)
        {
            _subTextureProviders = new SubTextureProvider(configuration, contentManager);
        }

        public TextureProvider()
        {
            _pixel = new XnaTexture2D();
        }

        private SubTextureProvider _subTextureProviders;
        private readonly XnaTexture2D _pixel;

        public ITexture2D GetTexture(string textureIdentifier)
        {
            return _subTextureProviders.GetTexture(textureIdentifier);
        }

        public void Init(GraphicsDevice device)
        {
            _pixel.Texture = new Texture2D(device, 1, 1, false, SurfaceFormat.Color, 1);
            _pixel.SetData(new[] { Color.White });
            _subTextureProviders.Init();
        }

        public ISpriteFont GetFont(string fontIdentifier)
        {
            return _subTextureProviders.GetFont(fontIdentifier);
        }
    }
}