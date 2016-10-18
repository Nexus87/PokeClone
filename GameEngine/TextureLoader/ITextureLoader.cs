﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.TextureLoader
{
    public interface ITextureLoader
    {
        void AddTextureProvider(ITextureProvider provider);
        ITexture2D LoadTexture(string textureName);
    }
}
