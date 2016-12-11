using GameEngine.Graphics;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI.Renderers
{
    public abstract class AbstractRenderer<T> : IRenderer<T> where T : IGraphicComponent
    {
        protected void RenderText(ISpriteBatch spriteBatch, ISpriteFont font, string text, Vector2 position, float textHeight)
        {
            var scale = textHeight / font.MeasureString(" ").Y;
            spriteBatch.DrawString(font, text, position, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        protected void RenderText(ISpriteBatch spriteBatch, ISpriteFont font, string text, Rectangle position, float textHeight)
        {
            RenderText(spriteBatch, font, text, position.Location.ToVector2(), textHeight);
        }

        protected void RenderImage(ISpriteBatch spriteBatch, ITexture2D texture, Rectangle position)
        {
            spriteBatch.Draw(texture, position, Color.Black);
        }

        public abstract void Render(ISpriteBatch spriteBatch, T component);
    }
}