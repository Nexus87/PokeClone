using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.GUI.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics
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

    internal class SubTextureProvider
    {
        private static string PlatformString { get; } = Type.GetType("Mono.Runtime") != null ? @"Linux\" : @"Windows\";

        private readonly IEnumerable<TextureItem> _configs;
        private readonly IEnumerable<FontItem> _fontConfig;

        private readonly ContentManager _contentManager;
        private readonly Dictionary<string, ITexture2D> _textures = new Dictionary<string, ITexture2D>();
        private readonly Dictionary<string, ISpriteFont> _fonts = new Dictionary<string, ISpriteFont>();
        private bool _isInitialized;

        public SubTextureProvider(TextureProviderConfiguration configuration, ContentManager contentManager)
        {
            _contentManager = contentManager;
            _configs = configuration.TextureConfigs;
            _fontConfig = configuration.FontConfigs;
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
            var textureConfig = _configs.Single(x => x.TextureName == textureIdentifier);
            var pathPrefix = textureConfig.IsPlatformSpecific ? PlatformString : "";
            var texture = new XnaTexture2D(pathPrefix + textureConfig.Path, _contentManager);
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