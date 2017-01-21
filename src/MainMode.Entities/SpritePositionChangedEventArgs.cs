using System;
using MainMode.Graphic;
using Microsoft.Xna.Framework;

namespace MainMode.Entities
{
    public class SpritePositionChangedEventArgs : EventArgs
    {
        public SpritePositionChangedEventArgs(Point position, SpriteId spriteId)
        {
            Position = position;
            SpriteId = spriteId;
        }

        public Point Position { get; }
        public SpriteId SpriteId { get; }
    }
}