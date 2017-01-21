﻿using System;
using MainMode.Globals;
using MainMode.Graphic;

namespace MainMode.Entities
{
    public class SpriteTurnedEventArgs : EventArgs
    {
        public SpriteTurnedEventArgs(SpriteId spriteId, Direction direction)
        {
            SpriteId = spriteId;
            Direction = direction;
        }

        public SpriteId SpriteId { get; }
        public Direction Direction { get; }
    }
}