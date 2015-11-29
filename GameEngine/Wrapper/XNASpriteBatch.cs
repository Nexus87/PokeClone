using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Wrapper
{
    public class XNASpriteBatch : SpriteBatch, ISpriteBatch
    {
        public XNASpriteBatch(GraphicsDevice device) : base(device){ }

        public void DrawString(ISpriteFont spriteFont, StringBuilder text, Vector2 position, Color color)
        {
            DrawString(spriteFont.SpriteFont, text, position, color);
        }

        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color)
        {
            DrawString(spriteFont.SpriteFont, text, position, color);
        }

        public void DrawString(ISpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            DrawString(spriteFont.SpriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
        }

        public void DrawString(ISpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            DrawString(spriteFont.SpriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
        }

        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            DrawString(spriteFont.SpriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
        }

        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            DrawString(spriteFont.SpriteFont, text, position, color, rotation, origin, scale, effects, layerDepth);
        }
    }
}
