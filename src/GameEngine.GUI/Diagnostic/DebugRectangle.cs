using GameEngine.Graphics;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Diagnostic
{
    public class DebugRectangle
    {
        private readonly ITexture2D _pixel;

        public DebugRectangle(TextureProvider textureProvider)
        {
            _pixel = textureProvider.Pixel;
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