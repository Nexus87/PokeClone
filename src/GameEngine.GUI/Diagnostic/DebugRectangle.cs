using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Diagnostic
{
    internal class DebugRectangle
    {
        internal static DebugRectangle Rectangle;
        internal static void Enable(ITexture2D pixel)
        {
            Rectangle = new DebugRectangle(pixel);
        }
        private readonly ITexture2D _pixel;

        public DebugRectangle(ITexture2D pixel)
        {
            _pixel = pixel;
        }

        public void Draw(ISpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            var upperLine = new Rectangle(rectangle.Location, new Point(rectangle.Width, 5));
            var leftLine = new Rectangle(rectangle.Location, new Point(5, rectangle.Height));
            var lowerLine = new Rectangle(rectangle.Left, rectangle.Bottom - 5, rectangle.Width, 5);
            var rightLine = new Rectangle(rectangle.Right - 5, rectangle.Top, 5, rectangle.Height);
            spriteBatch.Draw(_pixel, upperLine, color);
            spriteBatch.Draw(_pixel, leftLine, color);
            spriteBatch.Draw(_pixel, lowerLine, color);
            spriteBatch.Draw(_pixel, rightLine, color);
        }
    }
}