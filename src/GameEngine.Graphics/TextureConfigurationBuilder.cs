using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics
{
    public class TextureConfigurationBuilder
    {
        private readonly Dictionary<object, TempConfig> _configurations = new Dictionary<object, TempConfig>();

        private class TempConfig
        {
            public readonly List<TextureItem> TextureConfigs = new List<TextureItem>();
            public readonly List<FontItem> FontConfigs = new List<FontItem>();
            public readonly List<SpriteSheetItem> SpriteSheetConfigs = new List<SpriteSheetItem>();
        }
        public void AddTextureConfig(object key, IEnumerable<TextureItem> textureConfig)
        {
            var tempConfig = GetTempConfig(key);
            tempConfig.TextureConfigs.AddRange(textureConfig);
        }

        private TempConfig GetTempConfig(object key)
        {
            TempConfig config;
            if (!_configurations.TryGetValue(key, out config))
                _configurations[key] = config = new TempConfig();

            return config;
        }

        public void AddFont(object key, IEnumerable<FontItem> fontConfigs)
        {
            var tempConfig = GetTempConfig(key);
            tempConfig.FontConfigs.AddRange(fontConfigs);
        }

        public void AddSpriteSheet(object key, IEnumerable<SpriteSheetItem> spriteSheetConfig)
        {
            var tempConfig = GetTempConfig(key);
            tempConfig.SpriteSheetConfigs.AddRange(spriteSheetConfig);
        }

        public IEnumerable<TextureProviderConfiguration> BuildConfiguration()
        {
            return _configurations
                .Select(x =>
                    new TextureProviderConfiguration(
                        x.Key,
                        x.Value.TextureConfigs,
                        x.Value.FontConfigs,
                        x.Value.SpriteSheetConfigs)
                );
        }
    }

    public class TextureProviderConfiguration
    {
        public TextureProviderConfiguration(object key, IEnumerable<TextureItem> textureConfigs, IEnumerable<FontItem> fontConfigs, IEnumerable<SpriteSheetItem> spriteSheetConfigs)
        {
            Key = key;
            TextureConfigs = textureConfigs;
            FontConfigs = fontConfigs;
            SpriteSheetConfigs = spriteSheetConfigs;
        }

        public object Key { get; }
        public IEnumerable<TextureItem> TextureConfigs { get; }
        public IEnumerable<FontItem> FontConfigs { get; }
        public IEnumerable<SpriteSheetItem> SpriteSheetConfigs { get; }
    }

    public class FontItem
    {
        public string Path { get; }
        public string FontName { get; }
        public bool IsPlatfromSpecific { get; }

        public FontItem(string path, string fontName, bool isPlatfromSpecific)
        {
            Path = path;
            FontName = fontName;
            IsPlatfromSpecific = isPlatfromSpecific;
        }
    }

    public class TextureItem
    {
        public TextureItem(string path, string textureName, bool isPlatformSpecific)
        {
            Path = path;
            TextureName = textureName;
            IsPlatformSpecific = isPlatformSpecific;
        }

        public string Path { get; }
        public string TextureName { get; }
        public bool IsPlatformSpecific { get; }
    }

    public class SpriteSheetItem
    {
        public string Path { get; }
        public Dictionary<string, Rectangle> Map { get; }
        public bool IsPlatformSpecific { get; }

        public SpriteSheetItem(string path, Dictionary<string, Rectangle> map, bool isPlatformSpecific)
        {
            Path = path;
            Map = map;
            IsPlatformSpecific = isPlatformSpecific;
        }
    }
    public enum TextureType
    {
        SingleTexture,
        MultipleTextures
    }
}