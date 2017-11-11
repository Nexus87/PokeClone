using System;

namespace GameEngine.Core.ECS
{
    public abstract class Component
    {
        protected Component(Guid entityId)
        {
            EntityId = entityId;
        }

        public Guid EntityId { get; }
        public Guid Id { get; } = Guid.NewGuid();

    }
}