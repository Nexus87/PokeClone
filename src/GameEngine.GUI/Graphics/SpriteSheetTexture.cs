using System;
using GameEngine.GUI.Graphics.General;
using GameEngine.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI.Graphics
{
    [GameType]
    public class SpriteSheetTexture : AbstractGraphicComponent, IImageBox
    {
        private readonly ITexture2D _spriteSheet;
        private readonly Rectangle _sourceRectangle;
        private Rectangle _destinationRectangle;
        public SpriteEffects SpriteEffects { get; set; }

        public SpriteSheetTexture(ITexture2D spriteSheet, Rectangle sourceRectangle)
        {
            if (spriteSheet == null) throw new ArgumentNullException(nameof(spriteSheet));

            SpriteEffects = SpriteEffects.None;
            this._sourceRectangle = sourceRectangle;
            this._spriteSheet = spriteSheet;
            Color = Color.White;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            batch.Draw(_spriteSheet, destinationRectangle:_destinationRectangle, sourceRectangle: _sourceRectangle, color: Color, effects: SpriteEffects);
        }

        protected override void Update()
        {
            base.Update();
            _destinationRectangle.Size = Area.Size;
            _destinationRectangle.Location = Area.Location;
        }

        public override void Setup()
        {
            _spriteSheet.LoadContent();
        }
    }
}