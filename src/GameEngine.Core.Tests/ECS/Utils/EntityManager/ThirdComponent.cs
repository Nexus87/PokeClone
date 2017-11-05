using System;
using GameEngine.Core.ECS;

namespace GameEngine.Core.Tests.ECS.Utils.EntityManager
{
    public class ThirdComponent : Component
    {
        public ThirdComponent(Guid entityId) : base(entityId)
        {
        }
    }
}