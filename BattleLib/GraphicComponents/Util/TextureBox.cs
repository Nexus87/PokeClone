using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponents.Util
{
    class TextureBox
    {
        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }

        public float Width { get; set; }
        public float Height { get; set; }

        Vector2 position;
        Vector2 scaling;
        Vector2 textureScaling;

        string texture;
        Texture2D image;

        public TextureBox(String texture)
        {
            this.texture = texture;
        }

        public void Setup(ContentManager content)
        {
            image = content.Load<Texture2D>(texture);
            textureScaling.X = 1.0f / image.Width;
            textureScaling.Y = 1.0f / image.Height;

            RecalculateScaling();
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(image, position, null, Color.White, 0, Vector2.Zero, scaling, SpriteEffects.None, 0);
        }

        void RecalculateScaling()
        {
            scaling.X = Width * textureScaling.X;
            scaling.Y = Height * textureScaling.Y;
        }
    }
}
