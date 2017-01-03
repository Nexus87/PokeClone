using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Textures
{
    public class TextureProvider
    {
        public ITexture2D Pixel => _pixel;

        internal void SetConfiguration(IEnumerable<TextureProviderConfiguration> configurations, ContentManager contentManager)
        {
            _subTextureProviders = configurations
                .ToDictionary(x => x.Key, x => new SubTextureProvider(x, contentManager));
        }

        public TextureProvider()
        {
            _pixel = new XnaTexture2D();
        }

        private Dictionary<object, SubTextureProvider> _subTextureProviders;
        private readonly XnaTexture2D _pixel;

        public ITexture2D GetTexture(object key, string textureIdentifier)
        {
            return _subTextureProviders[key].GetTexture(textureIdentifier);
        }

        public void Init(GraphicsDevice device)
        {
            _pixel.Texture = new Texture2D(device, 1, 1, false, SurfaceFormat.Color, 1);
            _pixel.SetData(new[] { Color.White });
            foreach (var subTextureProvider in _subTextureProviders.Values)
            {
                subTextureProvider.Init();
            }
        }

        public ISpriteFont GetFont(object key, string fontIdentifier)
        {
            return _subTextureProviders[key].GetFont(fontIdentifier);
        }
    }
}