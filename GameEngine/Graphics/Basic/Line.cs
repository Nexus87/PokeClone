using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Basic
{
    public class Line : AbstractGraphicComponent
    {
        public Line(PokeEngine game) : base(game) { }

        private float circleScale;
        private Texture2D cups;
        private Vector2 cupScale;
        private Vector2 leftCup;
        private Vector2 line;
        private Vector2 lineScale;
        private Texture2D pixel;
        private Vector2 rightCup;
        public Color Color { get; set; }

        public override void Setup(ContentManager content)
        {
            cups = content.Load<Texture2D>("circle");
            circleScale = 1.0f / cups.Height;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            if (pixel == null)
                Init(batch.GraphicsDevice);

            batch.Draw(pixel, position: line, scale: lineScale, color: Color);

            batch.Draw(cups, position: leftCup, scale: cupScale, color: Color);
            batch.Draw(cups, position: rightCup, scale: cupScale, color: Color);
        }

        protected override void Update()
        {
            if(Width == 0)
            {
                lineScale.X = 0;
                cupScale.X = 0;
                return;
            }
            else if (Width.CompareTo(Height) < 0)
                LineWithFlatCups();
            else
                LineWithCups();
                
        }

        private void LineWithCups()
        {
            // Width >= Height && Width != 0
            lineScale.Y = Height;
            lineScale.X = Width - Height; // >= 0

            line.X = Position.X + Height / 2.0f;
            line.Y = Position.Y;

            leftCup = Position;
            rightCup.Y = Position.Y;
            rightCup.X = Position.X + Width - Height;

            rightCup.X = rightCup.X.CompareTo(leftCup.X) > 0 ? rightCup.X : leftCup.X;
            cupScale.X = cupScale.Y = Height * circleScale;
        }

        private void LineWithFlatCups()
        {
            line =  leftCup = rightCup = Position;
            lineScale.X = lineScale.Y = 0;
            cupScale.X = Width * circleScale;
            cupScale.Y = Height * circleScale;
        }

        private void Init(GraphicsDevice device)
        {
            pixel = new Texture2D(device, 1, 1, false, SurfaceFormat.Color, 1);
            pixel.SetData(new[] { Color.White });
        }
    }
}
