using System.Collections.Generic;
using GameEngine.GUI.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public interface ISpriteSheetProvider
    {
        ITexture2D GetTexture();
        IDictionary<string, Rectangle> GetMapping();
        void Setup();
    }
}