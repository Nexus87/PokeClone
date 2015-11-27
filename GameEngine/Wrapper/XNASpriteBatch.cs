using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Wrapper
{
    public class XNASpriteBatch : SpriteBatch, ISpriteBatch
    {
        public XNASpriteBatch(GraphicsDevice device) : base(device){ }
    }
}
