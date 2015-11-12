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
    public class Line : AbstractGraphicComponent
    {
        Texture2D pixel;
        Texture2D cups;

        float circleScale;
        Vector2 leftCup;
        Vector2 rightCup;
        Vector2 cupScale;

        Vector2 line;
        Vector2 lineScale;

        public Color Color { get; set; }


        void Init(GraphicsDevice device)
        {
            pixel = new Texture2D(device, 1, 1, false, SurfaceFormat.Color, 1);
            pixel.SetData(new[] { Color.White });
        }

        protected override void DrawComponent(GameTime time, SpriteBatch batch)
        {
            if (pixel == null)
                Init(batch.GraphicsDevice);

            batch.Draw(pixel, position: line, scale: lineScale, color: Color);

            batch.Draw(cups, position: leftCup, scale: cupScale, color: Color);
            batch.Draw(cups, position: rightCup, scale: cupScale, color: Color);
        }

        public override void Setup(ContentManager content)
        {
            cups = content.Load<Texture2D>("circle");
            circleScale = 1.0f / cups.Height;
        }

        protected override void Update()
        {
            lineScale.Y = Size.Y;
            lineScale.X = Size.X - Size.Y;

            line.X = Position.X + Size.Y / 2.0f;
            line.Y = Position.Y;

            leftCup = Position;
            rightCup.Y = Position.Y;
            rightCup.X = Position.X + Size.X - Size.Y;

            rightCup.X = rightCup.X.CompareTo(leftCup.X) > 0 ? rightCup.X : leftCup.X;
            cupScale.X = cupScale.Y = Size.Y * circleScale;
        }
    }
}
