using System.Collections.Generic;
using GameEngine.Graphics;
using GameEngine.Graphics.General;

namespace GameEngine.TextureLoader
{
    public interface ITextureProvider
    {
        IEnumerable<string> GetProvidedNames();
        IImageBox GetTexture(string name);
    }
}