using Microsoft.Xna.Framework;

namespace GameEngine.Globals
{
    public class ScreenConstants
    {
        public ScreenConstants() :
            this(DefaultScreenHeight, DefaultScreenWidth, DefaultBackgroundColor)
        {
        }

        public ScreenConstants(float screenHeight, float screenWidth, Color backgroundColor)
        {
            ScreenHeight = screenHeight;
            ScreenWidth = screenWidth;
            BackgroundColor = backgroundColor;
        }

        private const float DefaultScreenHeight = 1080;
        private const float DefaultScreenWidth = 1920;
        private static readonly Color DefaultBackgroundColor = new Color(248, 248, 248, 0);

        public float ScreenHeight { get; private set; }
        public float ScreenWidth { get; private set; }
        public Color BackgroundColor { get; private set; }
    }
}
