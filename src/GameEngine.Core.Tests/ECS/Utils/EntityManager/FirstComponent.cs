using System;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Components;

namespace GameEngine.Core.Tests.ECS.Utils.EntityManager
{
    public class FirstComponent : Component
    {
        public FirstComponent(Guid entityId) : base(entityId)
        {
        }
    }
}