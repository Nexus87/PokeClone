using System;
using GameEngine.Core.ECS;

namespace GameEngine.Core.Tests.ECS.Utils.EntityManager
{
    public class SecondComponent : Component
    {
        public SecondComponent(Guid entityId) : base(entityId)
        {
        }
    }
}