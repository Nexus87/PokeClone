using System;
using System.IO;
using GameEngine.Tools.Configuration;
using Newtonsoft.Json;

namespace GameEngine.Tools.Storages
{
    public static class ConfigurationStorage
    {
        private static readonly string Platform = Type.GetType ("Mono.Runtime") == null ? "Windows" : "Linux";

        public static ModuleConfiguration LoadConfiguration(string path)
        {
            var configuration = LoadConfigurationRaw(path);
            foreach (var singleTextureConfiguration in configuration.TextureConfiguration.SingleTextureConfigurations)
            {
                singleTextureConfiguration.Path = SetPlatformPath(singleTextureConfiguration.Path);
            }
            foreach (var spriteSheetConfiguration in configuration.TextureConfiguration.SpriteSheetConfigurations)
            {
                spriteSheetConfiguration.MappingFile = SetPlatformPath(spriteSheetConfiguration.MappingFile);
                spriteSheetConfiguration.TextureFile = SetPlatformPath(spriteSheetConfiguration.TextureFile);
            }

            return configuration;
        }

        public static ModuleConfiguration LoadConfigurationRaw(string path)
        {
            return JsonNetStorage.LoadObject<ModuleConfiguration>(path);
        }

        public static void StoreConfiguration(string path, ModuleConfiguration configuration, bool formated = false)
        {
            JsonNetStorage.StoreObject(path, configuration, formated);
        }

        private static string SetPlatformPath(string path)
        {
            return path.Replace("$Platform", Platform);
        }
    }
}