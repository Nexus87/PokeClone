using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEngine.Tools.Storages;

namespace GameEngine.Graphics.Textures
{
    public class TextureConfigurationBuilder
    {
        private readonly string _contentRoot;

        public TextureConfigurationBuilder(string contentRoot)
        {
            _contentRoot = contentRoot;
        }

        private readonly TempConfig _configurations = new TempConfig();


        private class TempConfig
        {
            public readonly List<TextureItem> TextureConfigs = new List<TextureItem>();
            public readonly List<FontItem> FontConfigs = new List<FontItem>();
            public readonly List<SpriteSheetItem> SpriteSheetConfigs = new List<SpriteSheetItem>();
        }

        public void ReadConfigFile(string configFile)
        {
            var path = Path.GetDirectoryName(configFile) ?? "";
            var configuration = TextureConfigurationStorage.LoadTextureConfiguration(Path.Combine(_contentRoot, configFile));
            configuration.FontConfigurations.ForEach(x => x.Path = Path.Combine(path, x.Path));
            configuration.SingleTextureConfigurations.ForEach(x => x.Path = Path.Combine( path, x.Path));
            configuration.SpriteSheetConfigurations.ForEach(x =>
            {
                x.MappingFile = Path.Combine(_contentRoot, path, x.MappingFile);
                x.TextureFile = Path.Combine(_contentRoot, path, x.TextureFile);
            });

            AddFont( configuration
                .FontConfigurations
                .Select(x => new FontItem(x.Path, x.Name))
            );

            AddSpriteSheet(configuration
                .SpriteSheetConfigurations
                .Select(x => new SpriteSheetItem(x.TextureFile, JsonSpriteSheetConfigStorage.Load(x.MappingFile)))
            );

            AddTextureConfig(configuration
                .SingleTextureConfigurations
                .Select(x => new TextureItem(x.Path, x.Name))
            );
        }

        public void AddTextureConfig(IEnumerable<TextureItem> textureConfig)
        {
            _configurations.TextureConfigs.AddRange(textureConfig);
        }

        public void AddFont(IEnumerable<FontItem> fontConfigs)
        {
            _configurations.FontConfigs.AddRange(fontConfigs);
        }

        public void AddSpriteSheet(IEnumerable<SpriteSheetItem> spriteSheetConfig)
        {
            _configurations.SpriteSheetConfigs.AddRange(spriteSheetConfig);
        }

        internal TextureProviderConfiguration BuildConfiguration()
        {
            return new TextureProviderConfiguration(
                        _configurations.TextureConfigs,
                        _configurations.FontConfigs,
                        _configurations.SpriteSheetConfigs);
        }
    }
}