using System.IO;
using System.Linq;
using GameEngine.Entities;
using GameEngine.Graphics.Textures;
using GameEngine.Tools.Storages;
using GameEngine.TypeRegistry;

namespace GameEngine.Core.ModuleManager
{
    public abstract class Module : IModule
    {
        private readonly string _moduleFolderRoot;
        public string ModuleName { get; }

        public abstract void RegisterTypes(IGameTypeRegistry registry);

        protected void ReadConfiguration(
            TextureConfigurationBuilder builder,
            string textureFolder = "Textures",
            string configName = "TextureConfig.json",
            object resourceKey = null
        )
        {
            if (resourceKey == null)
                resourceKey = ResourceKey;

            var fullPath = TextureConfigurationBuilder.ContentRoot + "\\" + _moduleFolderRoot + "\\" + textureFolder + "\\";
            if (!File.Exists(fullPath + configName))
                return;

            var configuration = TextureConfigurationStorage.LoadTextureConfiguration(fullPath + configName);
            configuration.FontConfigurations.ForEach(x => x.Path = fullPath + x.Path);
            configuration.SingleTextureConfigurations.ForEach(x => x.Path = fullPath + x.Path);
            configuration.SpriteSheetConfigurations.ForEach(x =>
            {
                x.MappingFile = fullPath + x.MappingFile;
                x.TextureFile = fullPath + x.TextureFile;
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

        public virtual void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            ReadConfiguration(builder);
        }

        public virtual void AddBuilderAndRenderer()
        {
        }

        public abstract void Start(IGameComponentManager manager, IInputHandlerManager inputHandlerManager,
            IGameTypeRegistry registry);

        public abstract void Stop(IGameComponentManager engine, IInputHandlerManager inputHandlerManager);
        private object ResourceKey { get; }

        protected Module(object resourceKey, string moduleName, string moduleFolderRoot)
        {
            _moduleFolderRoot = moduleFolderRoot;
            ModuleName = moduleName;
            ResourceKey = resourceKey;
        }
    }
}