using System;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Components
{
    public class PositionComponent : Component
    {
        public Rectangle Destination { get; set; }

        public PositionComponent(Guid entityId) : base(entityId)
        {
        }
    }
}