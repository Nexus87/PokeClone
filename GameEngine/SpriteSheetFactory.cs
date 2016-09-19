using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class SpriteSheetFactory
    {
        private readonly string textureName;
        private readonly string spriteSheetDictionary;
        private readonly ContentManager contentManager;
        private readonly Dictionary<string, Rectangle> rectangles = new Dictionary<string, Rectangle>();
        private XNATexture2D spriteSheet;

        public SpriteSheetFactory(string textureName, string spriteSheetDictionary, ContentManager contentManager)
        {
            this.textureName = textureName;
            this.spriteSheetDictionary = spriteSheetDictionary;
            this.contentManager = contentManager;
        }

        public void Setup()
        {
            spriteSheet = new XNATexture2D(contentManager.Load<Texture2D>(textureName));
            var tmpList = new List<string>();
            using (var stream = new StreamReader(spriteSheetDictionary))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                    tmpList.Add(line);
            }

            var splitted = tmpList.Select(
                s => new {key = s.Split(';')[0], row = int.Parse(s.Split(';')[1]), column = int.Parse(s.Split(';')[2])}
            ).ToList();

            var rows = splitted.Max(t => t.row) + 1;
            var columns = splitted.Max(t => t.column) + 1;
            var textureHeight = ((float) spriteSheet.Height) / rows;
            var textureWidth = ((float) spriteSheet.Width) / columns;

            foreach (var entry in splitted)
            {
                var sourceRectangle = new Rectangle((int) (entry.column * textureWidth), (int) (entry.row * textureHeight), (int) textureWidth, (int) textureHeight);
                rectangles[entry.key] = sourceRectangle;
            }
        }

        public SpriteSheetTexture CreateSpriteSheetTexture(string textureName)
        {
            return new SpriteSheetTexture(spriteSheet, rectangles[textureName]);
        }
    }
}