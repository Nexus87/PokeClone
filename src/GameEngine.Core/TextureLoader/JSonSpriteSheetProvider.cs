using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using GameEngine.GUI.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core.TextureLoader
{
    public class JsonSpriteSheetProvider : ISpriteSheetProvider
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

        private readonly string filename;
        private readonly string mappingFileName;
        private readonly ContentManager content;
        private ITexture2D texture = null;
        private Dictionary<string, Rectangle> mapping;

        public JsonSpriteSheetProvider(string filename, string mappingFileName, ContentManager content)
        {
            this.filename = filename;
            this.mappingFileName = mappingFileName;
            this.content = content;
        }

        public ITexture2D GetTexture()
        {
            return texture;
        }

        public IDictionary<string, Rectangle> GetMapping()
        {
            return mapping;
        }

        public void Setup()
        {
            texture = new XnaTexture2D(content.Load<Texture2D>(filename));
            mapping = ReadAreaMapping().ToDictionary(p => p.Key, p => AreaToRectangle(p.Value));

        }

        private static Rectangle AreaToRectangle(Area area)
        {
            return new Rectangle(area.X, area.Y, area.Width, area.Height);
        }

        private Dictionary<string, Area> ReadAreaMapping()
        {
            var deserializer = new DataContractJsonSerializer(typeof(Dictionary<string, Area>));
            return (Dictionary<string, Area>) deserializer.ReadObject(new FileStream(mappingFileName, FileMode.Open, FileAccess.Read));

        }
    }
}