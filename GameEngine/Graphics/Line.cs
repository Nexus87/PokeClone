using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics
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
        public Line(ITexture2D pixel, ITexture2D cupTexture)
        {
            cupTexture.CheckNull("cupTexture");

            this.pixel = pixel;
            this.cupTexture = cupTexture;
            Color = Color.Black;
        }

        private float circleTextureScale;
        private ITexture2D cupTexture;
        private Vector2 cupScale;
        private Vector2 leftCup;
        private Vector2 line;
        private Vector2 lineScale;
        private ITexture2D pixel;
        private Vector2 rightCup;

        /// <summary>
        /// Setup this component
        /// </summary>
        /// <see cref="IGraphicComponent.Setup"/>
        public override void Setup()
        {
            cupTexture.LoadContent();
            circleTextureScale = 1.0f / cupTexture.Height;
        }

        /// <summary>
        /// Draws the line
        /// </summary>
        /// <param name="time">Game time</param>
        /// <param name="batch">Sprite batch</param>
        /// <see cref="AbstractGraphicComponent.DrawComponent"/>
        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            batch.Draw(pixel, position: line, scale: lineScale, color: Color);

            batch.Draw(cupTexture, position: leftCup, scale: cupScale, color: Color);
            batch.Draw(cupTexture, position: rightCup, scale: cupScale, color: Color);
        }

        /// <summary>
        /// Updates the component
        /// </summary>
        /// <see cref="AbstractGraphicComponent.Update"/>
        protected override void Update()
        {
            if(Width.AlmostEqual(0))
            {
                lineScale.X = 0;
                cupScale.X = 0;
                return;
            }
            else if (Width.CompareTo(Height) < 0)
                SetComponentsFlatCups();
            else
                SetComponentsWithCups();
                
        }

        /// <summary>
        /// This calculates the coordinates if Width >= Height
        /// </summary>
        private void SetComponentsWithCups()
        {
            SetLineCoordinates();
            SetCupsCoordinates();
        }

        private void SetCupsCoordinates()
        {
            cupScale.X = cupScale.Y = Height * circleTextureScale;

            leftCup = Position;

            rightCup.Y = Position.Y;
            rightCup.X = Position.X + Width - Height;

            rightCup.X = rightCup.X.CompareTo(leftCup.X) > 0 ? rightCup.X : leftCup.X;
        }

        private void SetLineCoordinates()
        {
            // Width >= Height && Width != 0
            float cupRadius = 0.5f * Height;

            lineScale.Y = Height;
            lineScale.X = Width - 2 * cupRadius; // >= 0

            line.X = Position.X + cupRadius;
            line.Y = Position.Y;
        }

        /// <summary>
        /// This is called if there is not enough Width for a full circle with radius Height
        /// </summary>
        private void SetComponentsFlatCups()
        {
            line =  leftCup = rightCup = Position;

            lineScale = Vector2.Zero;

            cupScale.X = Width * circleTextureScale;
            cupScale.Y = Height * circleTextureScale;
        }
    }
}
