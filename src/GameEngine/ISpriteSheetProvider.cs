using System.Collections.Generic;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public interface ISpriteSheetProvider
    {
        ITexture2D GetTexture();
        IDictionary<string, Rectangle> GetMapping();
        void Setup();
    }
}