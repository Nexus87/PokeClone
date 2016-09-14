using System;
using System.Linq;
using System.Runtime.InteropServices;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace MainModule
{
    public class FieldMap : AbstractGraphicComponent, IMap
    {
        private readonly float textureSize;
        private readonly ScreenConstants screenConstants;
        private readonly Container container = new Container();

        public FieldMap(ITable<IGraphicComponent> fieldTextures, float textureSize, ScreenConstants screenConstants)
        {
            FieldSize = new FieldSize(fieldTextures.Columns, fieldTextures.Rows);
            this.textureSize = textureSize;
            this.screenConstants = screenConstants;

            TotalHeight = fieldTextures.Rows * textureSize;
            TotalWidth = fieldTextures.Columns * textureSize;

            container.SetCoordinates(0, 0, TotalWidth, TotalHeight);
            fieldTextures.
                EnumerateAlongColumns().
                ToList().
                ForEach(c => container.AddComponent(c));

            container.Layout = new GridLayout(fieldTextures.Rows, fieldTextures.Columns);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            container.Draw(time, batch);
        }

        public override void Setup()
        {
            container.Setup();
        }


        public FieldSize FieldSize { get; private set; }
        public float TotalWidth { get; private set; }
        public float TotalHeight { get; private set; }

        public void CenterField(int fieldX, int fieldY)
        {
            if(fieldX >= FieldSize.Width || fieldX < 0)
                throw new ArgumentOutOfRangeException("fieldX must be between 0 and " + FieldSize.Width + " " +
                                                      "but was " + fieldX);
            if (fieldY >= FieldSize.Height || fieldY < 0)
                throw new ArgumentOutOfRangeException("fieldY must be between 0 and " + FieldSize.Height +
                                                      " but was " + fieldY);


            var centerX = screenConstants.ScreenWidth / 2.0f;
            var centerY = screenConstants.ScreenHeight / 2.0f;

            var xCenterComponent = centerX - (textureSize / 2.0f);
            var yCenterComponent = centerY - (textureSize / 2.0f);

            container.XPosition = xCenterComponent - fieldX * textureSize;
            container.YPosition = yCenterComponent - fieldY * textureSize;

        }
    }
}