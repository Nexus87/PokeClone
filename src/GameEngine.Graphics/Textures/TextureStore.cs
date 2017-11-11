using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Graphics.Textures
{
    internal class TextureStore
    {
        private readonly IEnumerable<TextureItem> _textureConfigs;
        private readonly IEnumerable<FontItem> _fontConfig;
        private readonly IEnumerable<SpriteSheetItem> _spriteSheetConfigs;

        private readonly ContentManager _contentManager;
        private readonly Dictionary<string, ITexture2D> _textures = new Dictionary<string, ITexture2D>();
        private readonly Dictionary<string, ISpriteFont> _fonts = new Dictionary<string, ISpriteFont>();
        private bool _isInitialized;

        public TextureStore(TextureProviderConfiguration configuration, ContentManager contentManager)
        {
            _contentManager = contentManager;
            _textureConfigs = configuration.TextureConfigs;
            _fontConfig = configuration.FontConfigs;
            _spriteSheetConfigs = configuration.SpriteSheetConfigs;
        }

        public ITexture2D GetTexture(string textureIdentifier)
        {
            if (!_textures.TryGetValue(textureIdentifier, out var texture))
            {
                texture = CreateTexture(textureIdentifier);
            }

            return texture;
        }

        private ITexture2D CreateTexture(string textureIdentifier)
        {
            ITexture2D texture;

            var textureConfig = _textureConfigs.SingleOrDefault(x => x.TextureName == textureIdentifier);
            if(textureConfig != null)
            {
                texture =  new XnaTexture2D(textureConfig.Path, _contentManager);

            }
            else
            {
                var spriteSheetConfig = _spriteSheetConfigs.Single(x => x.Map.ContainsKey(textureIdentifier));
                var source = spriteSheetConfig.Map[textureIdentifier];
                texture = new XnaSpriteSheetTexture2D(source,new XnaTexture2D(spriteSheetConfig.Path, _contentManager));

            }
            _textures[textureIdentifier] = texture;

            if(_isInitialized)
                texture.LoadContent();

            return texture;
        }

        public void Init()
        {
            foreach (var texture in _textures.Values)
            {
                texture.LoadContent();
            }
            foreach (var font in _fonts.Values)
            {
                font.LoadContent();
            }

            _isInitialized = true;
        }

        public ISpriteFont GetFont(string fontIdentifier)
        {
            if (!_fonts.TryGetValue(fontIdentifier, out var font))
            {
                font = CreateFont(fontIdentifier);
            }

            return font;
        }

        private ISpriteFont CreateFont(string fontIdentifier)
        {
            var fontConfig = _fontConfig.Single(x => x.FontName == fontIdentifier);
            var font = new XnaSpriteFont(fontConfig.Path, _contentManager);
            _fonts[fontIdentifier] = font;

            if(_isInitialized)
                font.LoadContent();

            return font;
        }
    }
}