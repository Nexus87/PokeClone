using System.Collections.Generic;
using GameEngine.Graphics;

namespace GameEngine
{
    public interface ISpriteSheetProvider
    {
        ITexture2D GetTexture();
        IDictionary<string, TableIndex> GetMapping();
    }
}