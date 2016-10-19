using System.Collections.Generic;
using GameEngine.Graphics.General;

namespace GameEngine.TextureLoader
{
    public interface ITextureProvider
    {
        IEnumerable<string> GetProvidedNames();
        ITexture2D GetTexture(string name);
    }
}