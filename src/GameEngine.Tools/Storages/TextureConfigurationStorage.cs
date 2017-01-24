using System;

namespace GameEngine.Tools.Storages
{
    public static class TextureConfigurationStorage
    {
        private static readonly string Platform = Type.GetType("Mono.Runtime") == null ? "Windows" : "Linux";

        public static TextureConfiguration LoadTextureConfiguration(string file)
        {
            var config = LoadRawTextureConfiguration(file);
            config.FontConfigurations.ForEach(x => x.Path = ReplacePlatformString(x.Path));
            config.SingleTextureConfigurations.ForEach(x => x.Path = ReplacePlatformString(x.Path));
            config.SpriteSheetConfigurations.ForEach(x => x.TextureFile = ReplacePlatformString(x.TextureFile));

            return config;
        }

        private static string ReplacePlatformString(string path)
        {
            return path.Replace("$Platform", Platform);
        }

        public static TextureConfiguration LoadRawTextureConfiguration(string file)
        {
            return JsonNetStorage.LoadObject<TextureConfiguration>(file);
        }

        public static void StoreTextureConfiguration(string path, TextureConfiguration data, bool formated = false)
        {
            JsonNetStorage.StoreObject(path, data, formated);
        }
    }
}