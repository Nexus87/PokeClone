using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Graphics.Textures
{
    public class TextureConfigurationBuilder
    {
        private readonly Dictionary<object, TempConfig> _configurations = new Dictionary<object, TempConfig>();

        public const string ContentRoot = "Content";

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

        internal IEnumerable<TextureProviderConfiguration> BuildConfiguration()
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
}