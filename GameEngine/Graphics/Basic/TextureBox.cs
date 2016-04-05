using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Basic
{
    public class TextureBox : AbstractGraphicComponent
    {
        Vector2 textureScaling;
        ITexture2D image;

        public ITexture2D Image { get { return image; } set { image = value; Invalidate(); } }
        Vector2 scale;

        public TextureBox(PokeEngine game) : base(game) { }

        public TextureBox(ITexture2D texture, PokeEngine game)
            : base(game)
        {
            this.image = texture;
        }

        public override void Setup()
        {
            if (image == null)
                return;

            image.LoadContent();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            if(image != null)
                batch.Draw(image, Position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        protected override void Update()
        {
            if (image == null)
                return;

            textureScaling.X = 1.0f / image.Width;
            textureScaling.Y = 1.0f / image.Height;

            scale.X = Width * textureScaling.X;
            scale.Y = Height * textureScaling.Y;
        }
    }
}
