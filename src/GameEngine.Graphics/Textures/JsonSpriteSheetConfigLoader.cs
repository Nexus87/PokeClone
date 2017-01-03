using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.Textures
{
    public static class JsonSpriteSheetConfigLoader
    {
        [DataContract]
        public class Area
        {
            [DataMember]
            public int X { get; set; }
            [DataMember]
            public int Y { get; set; }
            [DataMember]
            public int Width { get; set; }
            [DataMember]
            public int Height { get; set; }
        }

        private static Rectangle AreaToRectangle(Area area)
        {
            return new Rectangle(area.X, area.Y, area.Width, area.Height);
        }

        private static Dictionary<string, Area> ReadAreaMapping(string fileName)
        {
            var deserializer = new DataContractJsonSerializer(typeof(Dictionary<string, Area>));
            return (Dictionary<string, Area>) deserializer.ReadObject(new FileStream(fileName, FileMode.Open, FileAccess.Read));

        }

        public static Dictionary<string, Rectangle> Load(string fileName)
        {
             return ReadAreaMapping(@"Content\" + fileName)
                 .ToDictionary(p => p.Key, p => AreaToRectangle(p.Value));
        }
    }
}