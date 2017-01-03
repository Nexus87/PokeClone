using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.Textures
{
    public class SpriteSheetItem
    {
        public string Path { get; }
        public Dictionary<string, Rectangle> Map { get; }
        public bool IsPlatformSpecific { get; }

        public SpriteSheetItem(string path, Dictionary<string, Rectangle> map, bool isPlatformSpecific)
        {
            Path = path;
            Map = map;
            IsPlatformSpecific = isPlatformSpecific;
        }
    }
}