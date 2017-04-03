using System.Collections.Generic;

namespace GameEngine.Graphics.Textures
{
    internal class TextureProviderConfiguration
    {
        public IEnumerable<TextureItem> TextureConfigs { get; }
        public IEnumerable<FontItem> FontConfigs { get; }
        public IEnumerable<SpriteSheetItem> SpriteSheetConfigs { get; }

        public TextureProviderConfiguration(IEnumerable<TextureItem> textureConfigs, IEnumerable<FontItem> fontConfigs, IEnumerable<SpriteSheetItem> spriteSheetConfigs)
        {
            TextureConfigs = textureConfigs;
            FontConfigs = fontConfigs;
            SpriteSheetConfigs = spriteSheetConfigs;
        }

    }
}