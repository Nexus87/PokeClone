﻿namespace GameEngine.Graphics
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