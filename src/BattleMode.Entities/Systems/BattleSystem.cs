using System.Collections.Generic;
using System.Linq;
using BattleMode.Entities.Actions;
using BattleMode.Entities.BattleState;
using BattleMode.Entities.BattleState.Commands;
using BattleMode.Entities.Components;
using GameEngine.Core.ECS;

namespace BattleMode.Entities.Systems
{
    public class BattleSystem
    {
        private readonly IMessageBus _messageBus;
        private readonly ICommandScheduler _scheduler;

        public BattleSystem(IMessageBus messageBus, ICommandScheduler scheduler)
        {
            _messageBus = messageBus;
            _scheduler = scheduler;
        }
        public void SetCommand(SetCommandAction action, IEntityManager entityManager)
        {
            var component = entityManager.GetComponentByTypeAndEntity<CommandComponent>(action.Entity).First();
            if (component.Command != null)
            {
                component.Command = action.Command;
            }

            if (entityManager.GetComponentsOfType<CommandComponent>().All(x => x.Command != null))
            {
                _messageBus.SendAction(new EndTurnAction());
            }
        }

        public void ExecuteNextCommand(ExecuteNextCommandAction action, IEntityManager entityManager)
        {
            var queue = entityManager.GetFirstComponentOfType<BattleStateComponent>().CommandQueues;
            if (queue.Count == 0)
            {
                _messageBus.SendAction(new StartNewTurnAction());
                return;
            }

            switch (queue.Dequeue())
            {
                case MoveCommand m:
                    break;
                case ItemCommand i:
                    break;
                case ChangeCommand c:
                    break;
            }

        }
        public void EndTurn(EndTurnAction action, IEntityManager entityManager)
        {
            var battleStateComponent = entityManager.GetFirstComponentOfType<BattleStateComponent>();
            battleStateComponent.CommandQueues = ScheduleCommands(entityManager.GetComponentsOfType<CommandComponent>().Select(x => x.Command));

            _messageBus.SendAction(new ExecuteNextCommandAction());
        }

        private Queue<ICommand> ScheduleCommands(IEnumerable<ICommand> commands)
        {
            _scheduler.ClearCommands();
            _scheduler.AppendCommands(commands);
            return new Queue<ICommand>(_scheduler.ScheduleCommands().ToList());
        }

    }
}