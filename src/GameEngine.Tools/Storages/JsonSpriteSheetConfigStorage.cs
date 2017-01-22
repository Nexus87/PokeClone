using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace GameEngine.Tools.Storages
{
    public static class JsonSpriteSheetConfigStorage
    {
        private static Rectangle AreaToRectangle(Area area)
        {
            return new Rectangle(area.X, area.Y, area.Width, area.Height);
        }


        public static Dictionary<string, Area> Load(string fileName)
        {
            return JsonNetStorage.LoadObject<Dictionary<string, Area>>(fileName);
        }
    }
}