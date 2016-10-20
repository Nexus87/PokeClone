using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Configuration;
using GameEngine.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.TextureLoader
{
    public class TextureLoader : ITextureLoader
    {
        private readonly ContentManager contentManager;
        private readonly Dictionary<string, ITextureProvider> providerMap = new Dictionary<string, ITextureProvider>();

        public TextureLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public void AddFromConfiguration(TextureConfig textureConfig)
        {
            foreach (var textureConfigSpriteSheet in textureConfig.SpriteSheets)
            {
                AddTextureProvider(TextureProviderFactory.GetProviderFromConfiguration(textureConfigSpriteSheet, contentManager));
            }
            foreach (var textureConfigTexture in textureConfig.Textures)
            {
                AddTextureProvider(TextureProviderFactory.GetProviderFromConfiguration(textureConfigTexture, contentManager));
            }
        }

        public void AddTextureProvider(ITextureProvider provider)
        {
            provider.GetProvidedNames()
                .ToList()
                .ForEach(s => providerMap[s] = provider);
        }

        public IImageBox LoadTexture(string textureName)
        {
            ITextureProvider provider;

            if(providerMap.TryGetValue(textureName, out provider))
                return provider.GetTexture(textureName);

            throw new ArgumentException("texture name not found: " + textureName, textureName);
        }
    }
}
