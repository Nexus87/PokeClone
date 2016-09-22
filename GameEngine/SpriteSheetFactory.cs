using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    [GameType]
    public class SpriteSheetFactory
    {
        private readonly ISpriteSheetProvider provider;
        private readonly Dictionary<string, Rectangle> rectangles = new Dictionary<string, Rectangle>();
        private ITexture2D spriteSheet;

        public SpriteSheetFactory(ISpriteSheetProvider provider)
        {
            this.provider = provider;
        }

        public void Setup()
        {
            spriteSheet = provider.GetTexture();
            var splitted = provider.GetMapping();

            var rows = splitted.Values.Max(t => t.Row) + 1;
            var columns = splitted.Values.Max(t => t.Column) + 1;

            var textureHeight = ((float) spriteSheet.Height) / rows;
            var textureWidth = ((float) spriteSheet.Width) / columns;

            foreach (var entry in splitted)
            {
                var sourceRectangle = new Rectangle((int) (entry.Value.Column * textureWidth), (int) (entry.Value.Row * textureHeight), (int) textureWidth, (int) textureHeight);
                rectangles[entry.Key] = sourceRectangle;
            }
        }

        public SpriteSheetTexture CreateSpriteSheetTexture(string textureName)
        {
            return new SpriteSheetTexture(spriteSheet, rectangles[textureName]);
        }
    }
}