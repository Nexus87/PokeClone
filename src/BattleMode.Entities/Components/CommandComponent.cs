using System;
using BattleMode.Entities.BattleState.Commands;
using GameEngine.Core.ECS;

namespace BattleMode.Entities.Components
{
    public class CommandComponent : Component
    {
        public ICommand Command { get; set; }
        public CommandComponent(Guid entityId) : base(entityId)
        {
        }
    }
}