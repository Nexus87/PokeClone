using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Configuration;
using GameEngine.Graphics;
using GameEngine.Graphics.General;

namespace GameEngine.TextureLoader
{
    public interface ITextureLoader
    {
        void AddFromConfiguration(TextureConfig textureConfig);
        void AddTextureProvider(ITextureProvider provider);
        IImageBox LoadTexture(string textureName);
    }
}
