using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Renderers;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    public class Pixel : AbstractGraphicComponent
    {
        private readonly ImageBox _imageBox;

        public Pixel(ITexture2D pixelTexture, IImageBoxRenderer renderer)
        {
            _imageBox = new ImageBox(renderer){Image = pixelTexture};
        }

        public Color Color { get; set; }

        protected override void Update()
        {
            _imageBox.SetCoordinates(this);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _imageBox.Draw(time, batch);
        }

        public override void Setup()
        {
            _imageBox.Setup();
        }
    }
}
