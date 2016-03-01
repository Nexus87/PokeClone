using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Wrapper
{
    /// <summary>
    /// XNA implementation of the ISpriteBatch interface
    /// </summary>
    /// <remarks>
    /// This class is a wrapper around XNAs SpriteBatch class, with the only difference that
    /// it uses ISpriteFont instead of a SpriteFont
    /// </remarks>
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
    }
}
