using System.Collections.Generic;
using GameEngine.GUI.Graphics;

namespace GameEngine.Core.TextureLoader
{
    public interface ITextureProvider
    {
        IEnumerable<string> GetProvidedNames();
        IImageBox GetTexture(string name);
    }
}