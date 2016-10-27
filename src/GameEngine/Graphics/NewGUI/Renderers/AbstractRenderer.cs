using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.NewGUI.Renderers
{
    public abstract class AbstractRenderer : IRenderer
    {
        public abstract void Render(IArea area);

        protected void RenderText(ISpriteFont font, string text, Vector2 position, float textHeight)
        {
            var scale = textHeight / font.MeasureString(" ").Y;
            SpriteBatch.DrawString(font, text, position, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        protected void RenderText(ISpriteFont font, string text, Rectangle position, float textHeight)
        {
            RenderText(font, text, position.Location.ToVector2(), textHeight);
        }

        protected void RenderImage(ITexture2D texture, Rectangle position)
        {
            SpriteBatch.Draw(texture, position, Color.Black);
        }

        public ISpriteBatch SpriteBatch { get; set; }
    }
}