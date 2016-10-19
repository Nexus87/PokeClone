using System;

namespace GameEngine.Graphics
{
    public class GraphicComponentPositionChangedEventArgs : EventArgs
    {
        public float XPosition { get; private set; }
        public float YPosition { get; private set; }

        public GraphicComponentPositionChangedEventArgs(float xPosition, float yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }
    }
}