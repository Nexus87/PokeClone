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
    class Line
    {
        Texture2D pixel;
        Texture2D cups;

        Vector2 circleOffset;
        float angle;
        float circleScale;
        Vector2 scale;
        Vector2 start;
        Vector2 end;

        public Color Color { get; set; }
        public float Scale
        {
            get { return scale.Y; }
            set 
            { 
                scale.Y = value;
                RecalculateCircles();
            }
        }

        public Vector2 End
        {
            get { return end; }
            set
            {
                end = value;
                Recalculate();
            }
        }

        public Vector2 Start
        {
            get { return start; }
            set 
            {
                start = value;
                Recalculate();
            }
        }

        void Recalculate()
        {
            float x = end.X - start.X;
            float y = end.Y - start.Y;

            angle = (float)Math.Atan2(y, x);
            scale.X = (float) Math.Sqrt(x * x + y * y);

            RecalculateCircles();
        }

        private void RecalculateCircles()
        {
            if (cups == null)
                return;

            circleScale = 1.0f / ((float)cups.Height) * Scale;
            circleOffset.X = -Scale / 2.0f;
        }

        void Init(GraphicsDevice device)
        {
            pixel = new Texture2D(device, 1, 1, false, SurfaceFormat.Color, 1);
            pixel.SetData(new[] { Color.White });
        }

        public void Draw(SpriteBatch batch)
        {
            if (pixel == null)
                Init(batch.GraphicsDevice);

            batch.Draw(pixel, start, null, Color, angle, Vector2.Zero, scale, SpriteEffects.None, 0);
            batch.Draw(cups, start + circleOffset, null, Color, 0, Vector2.Zero, circleScale, SpriteEffects.None, 0);
            batch.Draw(cups, end + circleOffset, null, Color, 0, Vector2.Zero, circleScale, SpriteEffects.None, 0);
        }

        public void Setup(ContentManager content)
        {
            cups = content.Load<Texture2D>("circle");
            RecalculateCircles();
        }
    }
}
