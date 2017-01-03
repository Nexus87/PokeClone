using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Graphics.Textures
{
    internal class SubTextureProvider
    {
        private static string PlatformString { get; } = Type.GetType("Mono.Runtime") != null ? @"Linux\" : @"Windows\";

        private readonly IEnumerable<TextureItem> _configs;
        private readonly IEnumerable<FontItem> _fontConfig;
        private readonly IEnumerable<SpriteSheetItem> _spriteSheetConfigs;

        private readonly ContentManager _contentManager;
        private readonly Dictionary<string, ITexture2D> _textures = new Dictionary<string, ITexture2D>();
        private readonly Dictionary<string, ISpriteFont> _fonts = new Dictionary<string, ISpriteFont>();
        private bool _isInitialized;

        public SubTextureProvider(TextureProviderConfiguration configuration, ContentManager contentManager)
        {
            _contentManager = contentManager;
            _configs = configuration.TextureConfigs;
            _fontConfig = configuration.FontConfigs;
            _spriteSheetConfigs = configuration.SpriteSheetConfigs;
        }

        public ITexture2D GetTexture(string textureIdentifier)
        {
            ITexture2D texture;
            if (!_textures.TryGetValue(textureIdentifier, out texture))
            {
                texture = CreateTexture(textureIdentifier);
            }

            return texture;
        }

        private ITexture2D CreateTexture(string textureIdentifier)
        {
            ITexture2D texture;

            var textureConfig = _configs.SingleOrDefault(x => x.TextureName == textureIdentifier);
            if(textureConfig != null)
            {
                var pathPrefix = textureConfig.IsPlatformSpecific ? PlatformString : "";
                texture =  new XnaTexture2D(pathPrefix + textureConfig.Path, _contentManager);

            }
            else
            {
                var spriteSheetConfig = _spriteSheetConfigs.Single(x => x.Map.ContainsKey(textureIdentifier));
                var source = spriteSheetConfig.Map[textureIdentifier];
                var pathPrefix = spriteSheetConfig.IsPlatformSpecific ? PlatformString : "";
                texture = new XnaSpriteSheetTexture2D(source,new XnaTexture2D(pathPrefix + spriteSheetConfig.Path, _contentManager));

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
            ISpriteFont font;
            if (!_fonts.TryGetValue(fontIdentifier, out font))
            {
                font = CreateFont(fontIdentifier);
            }

            return font;
        }

        private ISpriteFont CreateFont(string fontIdentifier)
        {
            var fontConfig = _fontConfig.Single(x => x.FontName == fontIdentifier);
            var pathPrefix = fontConfig.IsPlatfromSpecific ? PlatformString : "";
            var font = new XnaSpriteFont(pathPrefix + fontConfig.Path, _contentManager);
            _fonts[fontIdentifier] = font;

            if(_isInitialized)
                font.LoadContent();

            return font;
        }
    }
}