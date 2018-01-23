using System;
using System.Collections.Generic;
using BattleMode.Entities.BattleState.Commands;
using GameEngine.Core.ECS;

namespace BattleMode.Entities.Components
{
    public class BattleStateComponent : Component
    {
        public Queue<ICommand> CommandQueues { get; set; }
        public BattleStateComponent(Guid entityId) : base(entityId)
        {
        }
    }
}