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

        public SpriteSheetTexture(ITexture2D spriteSheet, Rectangle sourceRectangle)
        {
            if (spriteSheet == null) throw new ArgumentNullException("spriteSheet");

            this.sourceRectangle = sourceRectangle;
            this.spriteSheet = spriteSheet;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            //batch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color);
            batch.Draw(spriteSheet, destinationRectangle, null, Color);
            var scale = new Vector2(128/16, 128/16);
            batch.Draw(spriteSheet, Position, sourceRectangle, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
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