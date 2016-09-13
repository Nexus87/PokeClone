using GameEngine;
using GameEngine.Graphics;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace MainModule
{
    public class FieldMap : AbstractGraphicComponent, IMap
    {
        private float textureSize;

        public FieldMap(Table<IGraphicComponent> fieldTextures, float textureSize)
        {
            FieldSize = new FieldSize(fieldTextures.Columns, fieldTextures.Rows);
            this.textureSize = textureSize;
            TotalHeight = fieldTextures.Rows * textureSize;
            TotalWidth = fieldTextures.Columns * textureSize;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            throw new System.NotImplementedException();
        }

        public override void Setup()
        {
        }

        public FieldSize FieldSize { get; private set; }
        public float TotalWidth { get; private set; }
        public float TotalHeight { get; private set; }
    }
}