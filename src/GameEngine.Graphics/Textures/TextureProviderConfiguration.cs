using System.Collections.Generic;

namespace GameEngine.Graphics.Textures
{
    internal class TextureProviderConfiguration
    {
        public object Key { get; }
        public IEnumerable<TextureItem> TextureConfigs { get; }
        public IEnumerable<FontItem> FontConfigs { get; }
        public IEnumerable<SpriteSheetItem> SpriteSheetConfigs { get; }

        public TextureProviderConfiguration(object key, IEnumerable<TextureItem> textureConfigs, IEnumerable<FontItem> fontConfigs, IEnumerable<SpriteSheetItem> spriteSheetConfigs)
        {
            Key = key;
            TextureConfigs = textureConfigs;
            FontConfigs = fontConfigs;
            SpriteSheetConfigs = spriteSheetConfigs;
        }

    }
}