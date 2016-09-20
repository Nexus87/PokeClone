using GameEngine.Registry;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class Pixel : ForwardingGraphicComponent<TextureBox>
    {
        public Pixel(ITexture2D pixelTexture) :
            base(new TextureBox(pixelTexture))
        {
        }

        protected override void Update()
        {
        }
    }
}
