using GameEngine.Registry;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    [GameService(typeof(ScreenConstants))]
    public class ScreenConstants
    {
        private const float screenHeight = 1080;
        private const float screenWidth = 1920;
        private static readonly Color backgroundColor = new Color(248, 248, 248, 0);

        public float ScreenHeight { get { return screenHeight; } }
        public float ScreenWidth { get { return screenWidth; } }
        public Color BackgroundColor { get { return backgroundColor; } }
    }
}
