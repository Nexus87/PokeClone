using System;
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
        private IDictionary<string, Rectangle> rectangles;
        private ITexture2D spriteSheet;

        public SpriteSheetFactory(ISpriteSheetProvider provider)
        {
            this.provider = provider;
        }

        public void Setup()
        {
            provider.Setup();
            spriteSheet = provider.GetTexture();
            rectangles = provider.GetMapping();
        }

        public SpriteSheetTexture CreateSpriteSheetTexture(string textureName)
        {
            return new SpriteSheetTexture(spriteSheet, rectangles[textureName]);
        }
    }
}