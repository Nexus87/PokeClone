using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.TextureLoader
{
    public class TextureLoader : ITextureLoader
    {
        private readonly Dictionary<string, ITextureProvider> providerMap = new Dictionary<string, ITextureProvider>();

        public void AddTextureProvider(ITextureProvider provider)
        {
            provider.GetProvidedNames()
                .ToList()
                .ForEach(s => providerMap[s] = provider);
        }

        public ITexture2D LoadTexture(string textureName)
        {
            ITextureProvider provider;

            if(providerMap.TryGetValue(textureName, out provider))
                return provider.GetTexture(textureName);

            throw new ArgumentException("texture name not found: " + textureName, textureName);
        }
    }
}
