﻿using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Basic
{
    /// <summary>
    /// Draws a line with rounded cups
    /// </summary>
    /// <remarks>
    /// This component draws a mono color line with a half circle at each ends.
    /// The half circles can only be guaranteed, while the width
    /// of the component is large enough, this means Width >= Height.
    /// If the width is under this limit, the cups will still be round
    /// but no half circles any more.
    /// </remarks>
    public class Line : AbstractGraphicComponent
    {
        public Line(PokeEngine game) : base(game) 
        {
            Color = Color.Black;
        }

        private float circleScale;
        private Texture2D cups;
        private Vector2 cupScale;
        private Vector2 leftCup;
        private Vector2 line;
        private Vector2 lineScale;
        private Texture2D pixel;
        private Vector2 rightCup;

        /// <summary>
        /// Color of the line.
        /// </summary>
        /// <remarks>
        /// It is black by default.
        /// </remarks>
        public Color Color { get; set; }

        /// <summary>
        /// Setup this component
        /// </summary>
        /// <param name="content">Content manager</param>
        /// <see cref="IGraphicComponent.Setup"/>
        public override void Setup(ContentManager content)
        {
            cups = content.Load<Texture2D>("circle");
            circleScale = 1.0f / cups.Height;
        }

        /// <summary>
        /// Draws the line
        /// </summary>
        /// <param name="time">Game time</param>
        /// <param name="batch">Sprite batch</param>
        /// <see cref="AbstractGraphicComponent.DrawComponent"/>
        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            if (pixel == null)
                Init(batch.GraphicsDevice);

            batch.Draw(pixel, position: line, scale: lineScale, color: Color);

            batch.Draw(cups, position: leftCup, scale: cupScale, color: Color);
            batch.Draw(cups, position: rightCup, scale: cupScale, color: Color);
        }

        /// <summary>
        /// Updates the component
        /// </summary>
        /// <see cref="AbstractGraphicComponent.Update"/>
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

        /// <summary>
        /// This calculates the coordinates if Width >= Height
        /// </summary>
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

        /// <summary>
        /// This is called if there is not enough Width for a full circle with radius Height
        /// </summary>
        private void LineWithFlatCups()
        {
            line =  leftCup = rightCup = Position;
            lineScale.X = lineScale.Y = 0;
            cupScale.X = Width * circleScale;
            cupScale.Y = Height * circleScale;
        }

        /// <summary>
        /// Loads a 1x1 size texture
        /// </summary>
        /// <remarks>
        /// This function call on the first execution of Draw
        /// </remarks>
        /// <param name="device"></param>
        private void Init(GraphicsDevice device)
        {
            pixel = new Texture2D(device, 1, 1, false, SurfaceFormat.Color, 1);
            pixel.SetData(new[] { Color.White });

        }
    }
}
