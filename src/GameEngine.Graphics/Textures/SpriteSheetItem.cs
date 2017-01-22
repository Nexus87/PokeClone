using System.Collections.Generic;
using System.Linq;
using GameEngine.Tools.Storages;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.Textures
{
    public class SpriteSheetItem
    {
        public string Path { get; }
        public Dictionary<string, Rectangle> Map { get; }
        public bool IsPlatformSpecific { get; }

        public SpriteSheetItem(string path, Dictionary<string, Area> map, bool isPlatformSpecific)
        {
            Path = path;
            Map = map.ToDictionary(x => x.Key, x => ToRectangle(x.Value));
            IsPlatformSpecific = isPlatformSpecific;
        }

        private static Rectangle ToRectangle(Area area) => new Rectangle(area.X, area.Y, area.Width, area.Height);
    }
}