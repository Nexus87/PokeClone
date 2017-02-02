using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI.Renderers
{
    public abstract class AbstractRenderer<T> where T : IGuiComponent
    {
        protected static void RenderText(ISpriteBatch spriteBatch, ISpriteFont font, string text, Vector2 position, float textHeight)
        {
            var scale = textHeight / font.MeasureString(" ").Y;
            spriteBatch.DrawString(font, text, position, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        protected static void RenderText(ISpriteBatch spriteBatch, ISpriteFont font, string text, Rectangle position, float textHeight)
        {
            RenderText(spriteBatch, font, text, position.Location.ToVector2(), textHeight);
        }

        protected static void RenderImage(ISpriteBatch spriteBatch, ITexture2D texture, Rectangle position)
        {
            RenderImage(spriteBatch, texture, position, Color.White);
        }

        protected static void RenderImage(ISpriteBatch spriteBatch, ITexture2D texture, Rectangle position, Color color)
        {
            spriteBatch.Draw(texture: texture, destinationRectangle: position, color: color);
        }

        public void Render(ISpriteBatch spriteBatch, T component)
        {
            RenderComponent(spriteBatch, component);
#if DEBUG
            DebugRectangle.Rectangle?.Draw(spriteBatch, component.Area, Color.Black);
#endif
        }

        protected abstract void RenderComponent(ISpriteBatch spriteBatch, T component);
    }
}