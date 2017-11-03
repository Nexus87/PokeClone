using System;

namespace GameEngine.Core.ECS
{
    public class Entity
    {
        public Guid Id { get; }  = Guid.NewGuid();
    }
}