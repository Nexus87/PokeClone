using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class TextureBox : AbstractGraphicComponent
    {
        Vector2 textureScaling;

        string texture;
        Texture2D image;

        public TextureBox(String texture)
        {
            this.texture = texture;
        }

        public override void Setup(ContentManager content)
        {
            image = content.Load<Texture2D>(texture);
            textureScaling.X = 1.0f / image.Width;
            textureScaling.Y = 1.0f / image.Height;

            CalculateScale();
        }

        public override void Draw(GameTime time, SpriteBatch batch)
        {
            batch.Draw(image, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        protected override void CalculateScale()
        {
            scale.X = Width * textureScaling.X;
            scale.Y = Height * textureScaling.Y;
        }
    }
}
