using System.Collections.Generic;
using GameEngine.Configuration;
using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.TextureLoader
{
    public class SpriteSheetTextureProvider : ITextureProvider
    {
        private readonly JsonSpriteSheetProvider _spriteSheetProvider;

        public SpriteSheetTextureProvider(SpriteSheet spriteSheet, ContentManager contentManager)
        {
            _spriteSheetProvider = new JsonSpriteSheetProvider(spriteSheet.FileName, spriteSheet.MapFile, contentManager);
        }
        public IEnumerable<string> GetProvidedNames()
        {
            if(_spriteSheetProvider.GetMapping() == null)
                _spriteSheetProvider.Setup();
            return _spriteSheetProvider.GetMapping().Keys;
        }

        public IImageBox GetTexture(string name)
        {
            if(_spriteSheetProvider.GetMapping() == null)
                _spriteSheetProvider.Setup();
            return new SpriteSheetTexture(_spriteSheetProvider.GetTexture(), _spriteSheetProvider.GetMapping()[name]);
        }
    }
}