using System;
using System.Linq;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace MainModule
{
    [GameService(typeof(IMap))]
    public class FieldMap : AbstractGraphicComponent, IMap
    {
        private readonly float textureSize;
        private readonly Container container = new Container();

        private float screenCenterX;
        private float screenCenterY;
        private IMapLoader loader;
        private ScreenConstants screenConstants;

        public FieldMap(IMapLoader loader, ScreenConstants screenConstants)
            : this(loader, 128.0f, screenConstants)
        {
        }

        internal FieldMap(IMapLoader loader, float textureSize, ScreenConstants screenConstants)
        {
            this.textureSize = textureSize;
            this.loader = loader;
            this.screenConstants = screenConstants;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            container.Draw(time, batch);
        }

        public override void Setup()
        {
            loader.LoadMap();
            var fieldTextures = loader.GetFieldTextures();
            FieldSize = new FieldSize(fieldTextures.Columns, fieldTextures.Rows);

            TotalHeight = fieldTextures.Rows * textureSize;
            TotalWidth = fieldTextures.Columns * textureSize;

            container.SetCoordinates(0, 0, TotalWidth, TotalHeight);
            fieldTextures.
                EnumerateAlongRows().
                ToList().
                ForEach(c => container.AddComponent(c));

            container.Layout = new GridLayout(fieldTextures.Rows, fieldTextures.Columns);

            var centerX = screenConstants.ScreenWidth / 2.0f;
            var centerY = screenConstants.ScreenHeight / 2.0f;

            screenCenterX = centerX - (textureSize / 2.0f);
            screenCenterY = centerY - (textureSize / 2.0f);

            container.Setup();
        }


        public FieldSize FieldSize { get; private set; }
        public float TotalWidth { get; private set; }
        public float TotalHeight { get; private set; }
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
            container.XPosition = screenCenterX - fieldX * textureSize;
            container.YPosition = screenCenterY - fieldY * textureSize;

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