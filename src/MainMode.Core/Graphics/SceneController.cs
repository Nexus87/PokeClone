using System;
using GameEngine.Globals;
using GameEngine.Graphics;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace MainMode.Core.Graphics
{
    [GameService(typeof(ISceneController))]
    public class SceneController : ISceneController
    {
        private float _screenCenterX;
        private float _screenCenterY;
        private readonly IMapLoader _mapLoader;
        private readonly ScreenConstants _screenConstants;
        private IMapGraphic _mapGraphic;

        public SceneController(IMapLoader mapLoader, ScreenConstants screenConstants)
        {
            _mapLoader = mapLoader;
            _screenConstants = screenConstants;
            Scene = new Scene();
        }

        protected void Update()
        {
            CalculateScreenCenter();
            var x = _screenCenterX - _mapGraphic.GetXPositionOfColumn(CenteredFieldX);
            var y = _screenCenterY - _mapGraphic.GetYPositionOfRow(CenteredFieldY);

            Scene.MoveSceneTo(new Vector2(x, y));
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
            CenteredFieldX = fieldX;
            CenteredFieldY = fieldY;

            Update();
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
            Scene.SetBackground(_mapGraphic.Texture2D);
        }

        public Scene Scene { get; }
    }
}