using System.Collections.Generic;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    [GameType]
    public class SpriteSheetFactory
    {
        private readonly ISpriteSheetProvider _provider;
        private IDictionary<string, Rectangle> _rectangles;
        private ITexture2D _spriteSheet;

        public SpriteSheetFactory(ISpriteSheetProvider provider)
        {
            _provider = provider;
        }

        public void Setup()
        {
            _provider.Setup();
            _spriteSheet = _provider.GetTexture();
            _rectangles = _provider.GetMapping();
        }

        public SpriteSheetTexture CreateSpriteSheetTexture(string textureName)
        {
            return new SpriteSheetTexture(_spriteSheet, _rectangles[textureName]);
        }
    }
}