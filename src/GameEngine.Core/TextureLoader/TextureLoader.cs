using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.GUI.Configuration;
using GameEngine.GUI.Graphics;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core.TextureLoader
{
    public class TextureLoader : ITextureLoader
    {
        private readonly ContentManager _contentManager;
        private readonly IGameTypeRegistry _registry;
        private readonly Dictionary<string, ITextureProvider> _providerMap = new Dictionary<string, ITextureProvider>();

        public TextureLoader(ContentManager contentManager, IGameTypeRegistry registry)
        {
            _contentManager = contentManager;
            _registry = registry;
        }

        public void AddFromConfiguration(TextureConfig textureConfig)
        {
            foreach (var textureConfigSpriteSheet in textureConfig.SpriteSheets)
            {
                AddTextureProvider(TextureProviderFactory.GetProviderFromConfiguration(textureConfigSpriteSheet, _contentManager));
            }
            foreach (var textureConfigTexture in textureConfig.Textures)
            {
                AddTextureProvider(TextureProviderFactory.GetProviderFromConfiguration(textureConfigTexture, _contentManager, _registry));
            }
        }

        public void AddTextureProvider(ITextureProvider provider)
        {
            provider.GetProvidedNames()
                .ToList()
                .ForEach(s => _providerMap[s] = provider);
        }

        public IImageBox LoadTexture(string textureName)
        {
            ITextureProvider provider;

            if(_providerMap.TryGetValue(textureName, out provider))
                return provider.GetTexture(textureName);

            throw new ArgumentException("texture name not found: " + textureName, textureName);
        }
    }
}
