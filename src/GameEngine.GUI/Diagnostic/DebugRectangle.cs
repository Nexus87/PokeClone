using GameEngine.GUI.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Diagnostic
{
    public class DebugRectangle
    {
        private readonly ITexture2D _pixel;

        public DebugRectangle(ITexture2D pixel)
        {
            _pixel = pixel;
        }

        public void Draw(ISpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            var upperLine = new Rectangle(rectangle.Location, new Point(rectangle.Width, 1));
            var leftLine = new Rectangle(rectangle.Location, new Point(1, rectangle.Height));
            var lowerLine = new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, 1);
            var rightLine = new Rectangle(rectangle.Right - 1, rectangle.Top, 1, rectangle.Height);
            spriteBatch.Draw(_pixel, upperLine, color);
            spriteBatch.Draw(_pixel, leftLine, color);
            spriteBatch.Draw(_pixel, lowerLine, color);
            spriteBatch.Draw(_pixel, rightLine, color);
        }
    }
}