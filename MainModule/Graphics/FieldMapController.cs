using System;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace MainModule.Graphics
{
    [GameService(typeof(IMapController))]
    public class FieldMapController : AbstractGraphicComponent, IMapController
    {
        private float screenCenterX;
        private float screenCenterY;
        private readonly ScreenConstants screenConstants;
        private readonly IMap map;

        public FieldMapController(Map map, ScreenConstants screenConstants) :
            this((IMap) map, screenConstants)
        {}

        internal FieldMapController(IMap map, ScreenConstants screenConstants)
        {
            this.map = map;
            this.screenConstants = screenConstants;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            map.Draw(time, batch);
        }

        public override void Setup()
        {
            CalculateScreenCenter();
            map.Setup();
        }

        private void CalculateScreenCenter()
        {
            var centerX = screenConstants.ScreenWidth / 2.0f;
            var centerY = screenConstants.ScreenHeight / 2.0f;

            screenCenterX = centerX - (map.TextureSize / 2.0f);
            screenCenterY = centerY - (map.TextureSize / 2.0f);
        }

        internal int CenteredFieldX { get; set; }
        internal int CenteredFieldY { get; set; }

        public void CenterField(int fieldX, int fieldY)
        {
            map.XPosition = screenCenterX - map.GetXPositionOfColumn(fieldX);
            map.YPosition = screenCenterY - map.GetYPositionOfRow(fieldY);

            CenteredFieldX = fieldX;
            CenteredFieldY = fieldY;
        }

        public void MoveMap(Direction moveDirection)
        {
            switch (moveDirection)
            {
                case Direction.Up:
                    CenterField(CenteredFieldX, CenteredFieldY - 1);
                    break;
                case Direction.Down:
                    CenterField(CenteredFieldX, CenteredFieldY + 1);
                    break;
                case Direction.Left:
                    CenterField(CenteredFieldX - 1, CenteredFieldY);
                    break;
                case Direction.Right:
                    CenterField(CenteredFieldX + 1, CenteredFieldY);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("moveDirection", moveDirection, null);
            }
        }
    }
}