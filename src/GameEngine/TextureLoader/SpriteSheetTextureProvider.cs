using System.Collections.Generic;
using GameEngine.Configuration;
using GameEngine.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.TextureLoader
{
    public class SpriteSheetTextureProvider : ITextureProvider
    {
        private readonly SpriteSheet spriteSheet;
        private readonly JsonSpriteSheetProvider spriteSheetProvider;

        public SpriteSheetTextureProvider(SpriteSheet spriteSheet, ContentManager contentManager)
        {
            this.spriteSheet = spriteSheet;
            spriteSheetProvider = new JsonSpriteSheetProvider(spriteSheet.FileName, spriteSheet.MapFile, contentManager);
        }
        public IEnumerable<string> GetProvidedNames()
        {
            if(spriteSheetProvider.GetMapping() == null)
                spriteSheetProvider.Setup();
            return spriteSheetProvider.GetMapping().Keys;
        }

        public IImageBox GetTexture(string name)
        {
            if(spriteSheetProvider.GetMapping() == null)
                spriteSheetProvider.Setup();
            return new SpriteSheetTexture(spriteSheetProvider.GetTexture(), spriteSheetProvider.GetMapping()[name]);
        }
    }
}