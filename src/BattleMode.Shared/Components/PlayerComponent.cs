using System;
using GameEngine.Core.ECS;

namespace BattleMode.Shared.Components
{
    public class PlayerComponent : Component
    {
        public PlayerComponent(Guid entityId) : base(entityId)
        {
        }
    }
}