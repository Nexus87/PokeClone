using System;
using System.Collections.Generic;
using GameEngine.Graphics.Textures;
using MainMode.Globals;
using Microsoft.Xna.Framework.Graphics;

namespace MainMode.Graphic
{
    public class SpriteEntityTextures
    {
        public Dictionary<Direction, Tuple<ITexture2D, SpriteEffects>> StandingTextures { get; set; }
        public Dictionary<Direction, Tuple<ITexture2D, SpriteEffects>> MovingTextures { get; set; }
    }
}