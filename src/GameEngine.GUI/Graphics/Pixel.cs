using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    public class Pixel : AbstractGraphicComponent
    {
        private readonly TextureBox _textureBox;

        public Pixel(ITexture2D pixelTexture)
        {
            _textureBox = new TextureBox(pixelTexture);
        }

        public Color Color { get; set; }

        protected override void Update()
        {
            _textureBox.SetCoordinates(this);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _textureBox.Draw(time, batch);
        }

        public override void Setup()
        {
            _textureBox.Setup();
        }
    }
}
