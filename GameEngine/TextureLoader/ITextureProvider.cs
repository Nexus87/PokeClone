using System.Collections.Generic;

namespace GameEngine.TextureLoader
{
    public interface ITextureProvider
    {
        IEnumerable<string> GetProvidedNames();
        ITexture2D GetTexture(string name);
    }
}