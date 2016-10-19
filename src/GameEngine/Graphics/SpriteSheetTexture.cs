using System;
using GameEngine.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics
{
    [GameType]
    public class SpriteSheetTexture : AbstractGraphicComponent
    {
        private readonly ITexture2D spriteSheet;
        private readonly Rectangle sourceRectangle;
        private Rectangle destinationRectangle = new Rectangle();
        public SpriteEffects SpriteEffects { get; set; }

        public SpriteSheetTexture(ITexture2D spriteSheet, Rectangle sourceRectangle)
        {
            if (spriteSheet == null) throw new ArgumentNullException("spriteSheet");

            SpriteEffects = SpriteEffects.None;
            this.sourceRectangle = sourceRectangle;
            this.spriteSheet = spriteSheet;
            Color = Color.White;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            batch.Draw(spriteSheet, destinationRectangle:destinationRectangle, sourceRectangle: sourceRectangle, color: Color, effects: SpriteEffects);
        }

        protected override void Update()
        {
            base.Update();
            destinationRectangle.Size = Size.ToPoint();
            destinationRectangle.Location = Position.ToPoint();
        }

        public override void Setup()
        {
            spriteSheet.LoadContent();
        }
    }
}