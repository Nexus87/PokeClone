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
        private readonly IMapLoader mapLoader;
        private readonly ScreenConstants screenConstants;
        private IMapGraphic mapGraphic;
        private bool mapChanged;

        public FieldMapController(IMapLoader mapLoader, ScreenConstants screenConstants)
        {
            this.mapLoader = mapLoader;
            this.screenConstants = screenConstants;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            mapGraphic.Draw(time, batch);
        }

        public override void Setup()
        {
        }

        protected override void Update()
        {
            base.Update();
            if(mapChanged)
            {
                mapGraphic.Setup();
                CalculateScreenCenter();
                mapChanged = false;
            }
            mapGraphic.XPosition = screenCenterX - mapGraphic.GetXPositionOfColumn(CenteredFieldX);
            mapGraphic.YPosition = screenCenterY - mapGraphic.GetYPositionOfRow(CenteredFieldY);

        }

        private void CalculateScreenCenter()
        {
            var centerX = screenConstants.ScreenWidth / 2.0f;
            var centerY = screenConstants.ScreenHeight / 2.0f;

            screenCenterX = centerX - (mapGraphic.TextureSize / 2.0f);
            screenCenterY = centerY - (mapGraphic.TextureSize / 2.0f);
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
                    throw new ArgumentOutOfRangeException("moveDirection", moveDirection, null);
            }
        }

        public void LoadMap(Map map)
        {
            mapGraphic = mapLoader.LoadMap(map);
            Invalidate();
            mapChanged = true;
        }
    }
}