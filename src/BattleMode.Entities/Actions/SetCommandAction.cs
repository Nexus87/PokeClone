using BattleMode.Entities.BattleState.Commands;
using GameEngine.Core.ECS;

namespace BattleMode.Entities.Actions
{
    public class SetCommandAction
    {
        public readonly ICommand Command;
        public readonly Entity Entity;

        public SetCommandAction(ICommand command, Entity entity)
        {
            Command = command;
            Entity = entity;
        }

    }
}