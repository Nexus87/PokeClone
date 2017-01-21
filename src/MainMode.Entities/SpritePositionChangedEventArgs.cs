using System;
using MainMode.Graphic;

namespace MainMode.Entities
{
    public class SpritePositionChangedEventArgs : EventArgs
    {
        public SpritePositionChangedEventArgs(int newX, int newY, SpriteId spriteId)
        {
            NewX = newX;
            NewY = newY;
            SpriteId = spriteId;
        }

        public int NewX { get; }
        public int NewY { get; }
        public SpriteId SpriteId { get; }
    }
}