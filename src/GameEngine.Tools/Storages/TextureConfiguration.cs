using System.Collections.Generic;

namespace GameEngine.Tools.Storages
{
    public class TextureConfiguration
    {
        public List<SingleTextureConfiguration> SingleTextureConfigurations { get; set; } = new List<SingleTextureConfiguration>();
        public List<SpriteSheetConfiguration> SpriteSheetConfigurations { get; set; } = new List<SpriteSheetConfiguration>();
        public List<SingleTextureConfiguration> FontConfigurations { get; set; } = new List<SingleTextureConfiguration>();
    }
}