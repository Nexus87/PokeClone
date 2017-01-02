using System;
using GameEngine.Core;
using GameEngine.GUI;
using GameEngine.GUI.General;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace MainMode.Core.Graphics
{
    [GameService(typeof(IMapController))]
    public class FieldMapController : AbstractGraphicComponent, IMapController
    {
        private float _screenCenterX;
        private float _screenCenterY;
        private readonly IMapLoader _mapLoader;
        private readonly ScreenConstants _screenConstants;
        private IMapGraphic _mapGraphic;
        private bool _mapChanged;

        public FieldMapController(IMapLoader mapLoader, ScreenConstants screenConstants)
        {
            _mapLoader = mapLoader;
            _screenConstants = screenConstants;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _mapGraphic.Draw(time, batch);
        }

        protected override void Update()
        {
            base.Update();
            if(_mapChanged)
            {
                _mapGraphic.Setup();
                CalculateScreenCenter();
                _mapChanged = false;
            }
            var x = _screenCenterX - _mapGraphic.GetXPositionOfColumn(CenteredFieldX);
            var y = _screenCenterY - _mapGraphic.GetYPositionOfRow(CenteredFieldY);

            _mapGraphic.Area = new Rectangle((int) x, (int) y, _mapGraphic.Area.Width, _mapGraphic.Area.Height);
        }

        private void CalculateScreenCenter()
        {
            var centerX = _screenConstants.ScreenWidth / 2.0f;
            var centerY = _screenConstants.ScreenHeight / 2.0f;

            _screenCenterX = centerX - (_mapGraphic.TextureSize / 2.0f);
            _screenCenterY = centerY - (_mapGraphic.TextureSize / 2.0f);
        }

        internal int CenteredFieldX { get; private set; }
        internal int CenteredFieldY { get; private set; }

        public void CenterField(int fieldX, int fieldY)
        {
            Invalidate();
            CenteredFieldX = fieldX;
            CenteredFieldY = fieldY;
        }

        public void MoveMap(Direction moveDirection)
        {
            switch (moveDirection)
            {
                case Direction.Up:
                    CenterField(CenteredFieldX, CenteredFieldY + 1);
                    break;
                case Direction.Down:
                    CenterField(CenteredFieldX, CenteredFieldY - 1);
                    break;
                case Direction.Left:
                    CenterField(CenteredFieldX + 1, CenteredFieldY);
                    break;
                case Direction.Right:
                    CenterField(CenteredFieldX - 1, CenteredFieldY);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
            }
        }

        public void LoadMap(Map map)
        {
            _mapGraphic = _mapLoader.LoadMap(map);
            Invalidate();
            _mapChanged = true;
        }
    }
}