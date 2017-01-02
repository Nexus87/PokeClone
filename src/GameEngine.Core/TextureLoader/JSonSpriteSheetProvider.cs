using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.TextureLoader
{
    public class JsonSpriteSheetProvider
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

        private readonly string _mappingFileName;
        private Dictionary<string, Rectangle> _mapping;

        public JsonSpriteSheetProvider(string mappingFileName)
        {
            _mappingFileName = mappingFileName;
        }

        public Dictionary<string, Rectangle> GetMapping()
        {
            return _mapping;
        }

        public void Setup()
        {
            _mapping = ReadAreaMapping().ToDictionary(p => p.Key, p => AreaToRectangle(p.Value));
        }

        private static Rectangle AreaToRectangle(Area area)
        {
            return new Rectangle(area.X, area.Y, area.Width, area.Height);
        }

        private Dictionary<string, Area> ReadAreaMapping()
        {
            var deserializer = new DataContractJsonSerializer(typeof(Dictionary<string, Area>));
            return (Dictionary<string, Area>) deserializer.ReadObject(new FileStream(_mappingFileName, FileMode.Open, FileAccess.Read));

        }
    }
}