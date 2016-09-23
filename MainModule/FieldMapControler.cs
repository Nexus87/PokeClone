using System;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace MainModule
{
    [GameService(typeof(IMapControler))]
    public class FieldMapControler : AbstractGraphicComponent, IMapControler
    {
        private float screenCenterX;
        private float screenCenterY;
        private readonly ScreenConstants screenConstants;
        private readonly IMap map;

        public FieldMapControler(Map map, ScreenConstants screenConstants) :
            this((IMap) map, screenConstants)
        {}

        internal FieldMapControler(IMap map, ScreenConstants screenConstants)
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

        public FieldSize FieldSize { get { return map.FieldSize; } }
        internal int CenteredFieldX { get; set; }
        internal int CenteredFieldY { get; set; }

        public void CenterField(int fieldX, int fieldY)
        {
            if (fieldX >= FieldSize.Width || fieldX < 0)
                throw new ArgumentOutOfRangeException("fieldX must be between 0 and " + FieldSize.Width + " " +
                                                      "but was " + fieldX);
            if (fieldY >= FieldSize.Height || fieldY < 0)
                throw new ArgumentOutOfRangeException("fieldY must be between 0 and " + FieldSize.Height +
                                                      " but was " + fieldY);
            DoCenterField(fieldX, fieldY);
        }

        private void DoCenterField(int fieldX, int fieldY)
        {
            map.XPosition = screenCenterX - map.GetXPositionOfColumn(fieldX);
            map.YPosition = screenCenterY - map.GetYPositionOfRow(fieldY);

            CenteredFieldX = fieldX;
            CenteredFieldY = fieldY;
        }

        private void CenterFieldIfPossible(int fieldX, int fieldY)
        {
            if (fieldY < 0)
                fieldY = 0;
            else if (fieldY >= FieldSize.Height)
                fieldY = FieldSize.Height - 1;

            if (fieldX < 0)
                fieldX = 0;
            else if (fieldX >= FieldSize.Width)
                fieldX = FieldSize.Width - 1;

            DoCenterField(fieldX, fieldY);
        }

        public void MoveMap(Direction moveDirection)
        {
            switch (moveDirection)
            {
                case Direction.Up:
                    CenterFieldIfPossible(CenteredFieldX, CenteredFieldY - 1);
                    break;
                case Direction.Down:
                    CenterFieldIfPossible(CenteredFieldX, CenteredFieldY + 1);
                    break;
                case Direction.Left:
                    CenterFieldIfPossible(CenteredFieldX - 1, CenteredFieldY);
                    break;
                case Direction.Right:
                    CenterFieldIfPossible(CenteredFieldX + 1, CenteredFieldY);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("moveDirection", moveDirection, null);
            }
        }
    }
}