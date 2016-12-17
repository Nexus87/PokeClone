﻿using System.Collections.Generic;
using GameEngine.GUI.Graphics;

namespace GameEngine.TextureLoader
{
    public interface ITextureProvider
    {
        IEnumerable<string> GetProvidedNames();
        IImageBox GetTexture(string name);
    }
}