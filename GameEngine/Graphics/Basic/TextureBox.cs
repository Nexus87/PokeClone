using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Basic
{
    public class TextureBox : AbstractGraphicComponent
    {
        private ITexture2D image;

        public ITexture2D Image { get { return image; } set { image = value; Invalidate(); } }
        private Vector2 scale;

        public TextureBox(IPokeEngine game) : base(game)
        {
        }

        public TextureBox(ITexture2D image, IPokeEngine game)
            : base(game)
        {
            this.image = image;
        }

        public override void Setup()
        {
            if (image == null)
                return;

            image.LoadContent();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            if (image != null)
                batch.Draw(image, Position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        protected override void Update()
        {
            if (image == null)
                return;
            
            var imageWidthScale = 1.0f / image.Width;
            var imageHeightScale = 1.0f / image.Height;

            scale.X = Width * imageWidthScale;
            scale.Y = Height * imageHeightScale;
        }
    }
}