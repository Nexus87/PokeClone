using System.IO;
using System.Linq;
using GameEngine.Graphics.Textures;
using GameEngine.Tools.Storages;

namespace GameEngine.Core.ModuleManager
{
    public abstract class ContentModule : IContentModule
    {
        private readonly string _contentRoot;

        protected ContentModule(string contentRoot)
        {
            _contentRoot = contentRoot;
        }

        protected void ReadConfiguration(
            TextureConfigurationBuilder builder,
            object resourceKey,
            string configFile
        )
        {
            var path = Path.GetDirectoryName(configFile) ?? "";
            var configuration = TextureConfigurationStorage.LoadTextureConfiguration(Path.Combine(_contentRoot, configFile));
            configuration.FontConfigurations.ForEach(x => x.Path = Path.Combine(_contentRoot, path, x.Path));
            configuration.SingleTextureConfigurations.ForEach(x => x.Path = Path.Combine(_contentRoot,  path, x.Path));
            configuration.SpriteSheetConfigurations.ForEach(x =>
            {
                x.MappingFile = Path.Combine(_contentRoot, path, x.MappingFile);
                x.TextureFile = Path.Combine(_contentRoot, path, x.TextureFile);
            });

            builder.AddFont(resourceKey, configuration
                .FontConfigurations
                .Select(x => new FontItem(x.Path, x.Name))
            );

            builder.AddSpriteSheet(resourceKey, configuration
                .SpriteSheetConfigurations
                .Select(x => new SpriteSheetItem(x.TextureFile, JsonSpriteSheetConfigStorage.Load(x.MappingFile)))
            );

            builder.AddTextureConfig(resourceKey, configuration
                .SingleTextureConfigurations
                .Select(x => new TextureItem(x.Path, x.Name))
            );
        }

        public abstract void AddTextureConfigurations(TextureConfigurationBuilder builder);
        public abstract void AddBuilderAndRenderer();
    }
}
