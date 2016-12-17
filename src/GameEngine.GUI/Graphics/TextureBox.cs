using GameEngine.GUI.Graphics.General;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    [GameType]
    public class TextureBox : AbstractGraphicComponent, IImageBox
    {
        private ITexture2D image;

        public ITexture2D Image { get { return image; } set { image = value; Invalidate(); } }
        private Vector2 scale;


        public TextureBox(ITexture2D image = null)
        {
            this.image = image;
        }

        public override void Setup()
        {
            image?.LoadContent();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            if (image != null)
                batch.Draw(texture: image, destinationRectangle: Area, color: Color.White);
        }

        protected override void Update()
        {
            if (image == null)
                return;
            
            var imageWidthScale = 1.0f / image.Width;
            var imageHeightScale = 1.0f / image.Height;

            scale.X = Area.Width * imageWidthScale;
            scale.Y = Area.Height * imageHeightScale;
        }
    }
}