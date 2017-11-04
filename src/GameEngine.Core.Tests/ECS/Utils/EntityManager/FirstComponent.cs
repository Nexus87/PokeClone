using System;
using GameEngine.Core.ECS;

namespace GameEngine.Core.Tests.ECS.Utils.EntityManager
{
    public class FirstComponent : Component
    {
        public FirstComponent(Guid entityId) : base(entityId)
        {
        }
    }
}