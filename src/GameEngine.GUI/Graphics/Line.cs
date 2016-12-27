using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Utils;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
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
            pixel.CheckNull(nameof(pixel));
            cupTexture.CheckNull(nameof(cupTexture));

            _pixel = pixel;
            _cupTexture = cupTexture;
            Color = Color.Black;
        }

        private float _circleTextureScale;
        private readonly ITexture2D _cupTexture;
        private Vector2 _cupScale;
        private Vector2 _leftCup;
        private Vector2 _line;
        private Vector2 _lineScale;
        private readonly ITexture2D _pixel;
        private Vector2 _rightCup;

        /// <summary>
        /// Setup this component
        /// </summary>
        /// <see cref="IGraphicComponent.Setup"/>
        public override void Setup()
        {
            _circleTextureScale = 1.0f / _cupTexture.Height;
        }

        /// <summary>
        /// Draws the line
        /// </summary>
        /// <param name="time">Game time</param>
        /// <param name="batch">Sprite batch</param>
        /// <see cref="AbstractGraphicComponent.DrawComponent"/>
        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            batch.Draw(_pixel, position: _line, scale: _lineScale, color: Color);

            batch.Draw(_cupTexture, position: _leftCup, scale: _cupScale, color: Color);
            batch.Draw(_cupTexture, position: _rightCup, scale: _cupScale, color: Color);
        }

        /// <summary>
        /// Updates the component
        /// </summary>
        /// <see cref="AbstractGraphicComponent.Update"/>
        protected override void Update()
        {
            if(Area.Width == 0)
            {
                _lineScale.X = 0;
                _cupScale.X = 0;
            }
            else if (Area.Width.CompareTo(Area.Height) < 0)
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
            _cupScale.X = _cupScale.Y = Area.Height * _circleTextureScale;

            _leftCup = Area.Location.ToVector2();

            _rightCup.Y = Area.Y;
            _rightCup.X = Area.X + Area.Width - Area.Height;

            _rightCup.X = _rightCup.X.CompareTo(_leftCup.X) > 0 ? _rightCup.X : _leftCup.X;
        }

        private void SetLineCoordinates()
        {
            // Width >= Height && Width != 0
            var cupRadius = 0.5f * Area.Height;

            _lineScale.Y = Area.Height;
            _lineScale.X = Area.Width - 2 * cupRadius; // >= 0

            _line.X = Area.X + cupRadius;
            _line.Y = Area.Y;
        }

        /// <summary>
        /// This is called if there is not enough Width for a full circle with radius Height
        /// </summary>
        private void SetComponentsFlatCups()
        {
            _line =  _leftCup = _rightCup = Area.Location.ToVector2();

            _lineScale = Vector2.Zero;

            _cupScale.X = Area.Width * _circleTextureScale;
            _cupScale.Y = Area.Height * _circleTextureScale;
        }
    }
}
