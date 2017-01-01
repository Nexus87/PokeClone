using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Globals;
using Microsoft.Xna.Framework;
using OpenTK.Input;

namespace GameEngine.Graphics
{
    public class TextureConfigurationBuilder
    {
        private readonly Dictionary<object, IEnumerable<TextureItem>> _configs = new Dictionary<object, IEnumerable<TextureItem>>();
        private readonly Dictionary<object, IEnumerable<FontItem>> _fontConfigs = new Dictionary<object, IEnumerable<FontItem>>();

        public void AddTextureConfig(object key, IEnumerable<TextureItem> textureConfig)
        {
            AddToDictionary(_configs, key, textureConfig);
        }

        public void AddFont(object key, IEnumerable<FontItem> fontConfigs)
        {
            AddToDictionary(_fontConfigs, key, fontConfigs);
        }

        private static void AddToDictionary<T>(IDictionary<object, IEnumerable<T>> dictionary, object key, IEnumerable<T> config)
        {
            IEnumerable<T> currentConfig;
            if (dictionary.TryGetValue(key, out currentConfig))
            {
                dictionary[key] = currentConfig.Union(config);
            }
            else
            {
                dictionary[key] = config;
            }
        }

        public Dictionary<object, Tuple<IEnumerable<TextureItem>, IEnumerable<FontItem>>> BuildConfiguration()
        {
            var textureConfigs = _configs.Select(x => new { Key = x.Key, Value = Tuple.Create(x.Value, (IEnumerable<FontItem>) null)});
            var fontConfigs = _fontConfigs.Select(x => new { Key = x.Key, Value = Tuple.Create((IEnumerable<TextureItem>)null, x.Value)});

            var dictonary = textureConfigs.Union(fontConfigs)
                .GroupBy(x => x.Key)
                .ToDictionary(
                    x => x.Key,
                    x => Tuple.Create(
                        x.FirstOrDefault(y => y.Value.Item1 != null)?.Value.Item1,
                        x.FirstOrDefault(y => y.Value.Item2 != null)?.Value.Item2)
                );

            return dictonary;
        }
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
        public TextureItem(string path, TextureType type, Rectangle? source, string textureName, bool isPlatformSpecific)
        {
            Path = path;
            Type = type;
            Source = source;
            TextureName = textureName;
            IsPlatformSpecific = isPlatformSpecific;
        }

        public string Path { get; }
        public TextureType Type { get; }
        public Rectangle? Source { get; }
        public string TextureName { get; }
        public bool IsPlatformSpecific { get; }
    }

    public enum TextureType
    {
        SingleTexture,
        MultipleTextures
    }
}