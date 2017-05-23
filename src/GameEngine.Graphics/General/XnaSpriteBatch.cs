using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.General
{
    /// <summary>
    /// XNA implementation of the ISpriteBatch interface
    /// </summary>
    /// <remarks>
    /// This class is a wrapper around XNAs SpriteBatch class, with the only difference that
    /// it uses ISpriteFont instead of a SpriteFont
    /// </remarks>
    public sealed class XnaSpriteBatch : SpriteBatch, ISpriteBatch
    {
        public XnaSpriteBatch(GraphicsDevice device) : base(device){ }

        public void Draw(ITexture2D texture, Vector2 location, Vector2 scaling, Color color,
            SpriteEffects spriteEffects = SpriteEffects.None)
        {
            var destination = new Rectangle(location.ToPoint(), scaling.ToPoint());
            Draw(texture, destination, color, spriteEffects);
        }

        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(SpriteFont, string, Vector2, Color, float, Vector2, float, SpriteEffects, float)"/>
        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            DrawString(spriteFont.SpriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
        }

        public void Draw(RenderTarget2D texture, Rectangle destinationRectangle)
        {
            base.Draw(texture, destinationRectangle: destinationRectangle);
        }

        public void Draw(ITexture2D texture, Rectangle destinationRectangle, Color color, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            Draw(texture.Texture, destinationRectangle, sourceRectangle: texture.Bounds, color: color);
        }
    }
}
