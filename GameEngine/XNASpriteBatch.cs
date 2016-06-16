using GameEngine.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace GameEngine
{
    /// <summary>
    /// XNA implementation of the ISpriteBatch interface
    /// </summary>
    /// <remarks>
    /// This class is a wrapper around XNAs SpriteBatch class, with the only difference that
    /// it uses ISpriteFont instead of a SpriteFont
    /// </remarks>
    [GameComponentAttribute(RegisterType=typeof(ISpriteBatch))]
    sealed class XNASpriteBatch : SpriteBatch, ISpriteBatch
    {
        public XNASpriteBatch(GraphicsDevice device) : base(device){ }

        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(SpriteFont, StringBuilder, Vector2, Color)"/>
        public void DrawString(ISpriteFont spriteFont, StringBuilder text, Vector2 position, Color color)
        {
            DrawString(spriteFont.SpriteFont, text, position, color);
        }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(SpriteFont, string, Vector2, Color)"/>
        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color)
        {
            DrawString(spriteFont.SpriteFont, text, position, color);
        }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(SpriteFont, StringBuilder, Vector2, Color, float, Vector2, float, SpriteEffects, float)"/>
        public void DrawString(ISpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            DrawString(spriteFont.SpriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
        }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(SpriteFont, StringBuilder, Vector2, Color, float, Vector2, Vector2, SpriteEffects, float)"/>
        public void DrawString(ISpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            DrawString(spriteFont.SpriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
        }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(SpriteFont, string, Vector2, Color, float, Vector2, Vector2, SpriteEffects, float)"/>
        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            DrawString(spriteFont.SpriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
        }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(SpriteFont, string, Vector2, Color, float, Vector2, float, SpriteEffects, float)"/>
        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            DrawString(spriteFont.SpriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
        }


        public void Draw(ITexture2D texture, Rectangle destinationRectangle, Color color)
        {
            Draw(texture.Texture, destinationRectangle, color);
        }

        public void Draw(ITexture2D texture, Vector2 position, Color color)
        {
            Draw(texture.Texture, position, color);
        }

        public void Draw(ITexture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            Draw(texture.Texture, destinationRectangle, sourceRectangle, color);
        }

        public void Draw(ITexture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            Draw(texture.Texture, position, sourceRectangle, color);
        }

        public void Draw(ITexture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            Draw(texture.Texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
        }

        public void Draw(ITexture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            Draw(texture.Texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        public void Draw(ITexture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            Draw(texture.Texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        public void Draw(ITexture2D texture, Vector2? position = null, Rectangle? destinationRectangle = null, Rectangle? sourceRectangle = null, Vector2? origin = null, float rotation = 0f, Vector2? scale = null, Color? color = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0f)
        {
            Draw(texture.Texture, position, destinationRectangle, sourceRectangle, origin, rotation, scale, color, effects, layerDepth);
        }
    }
}
