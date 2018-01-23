using System.Collections.Generic;
using System.Linq;
using BattleMode.Entities.Actions;
using BattleMode.Entities.BattleState;
using BattleMode.Entities.BattleState.Commands;
using BattleMode.Entities.Components;
using BattleMode.Shared;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS;

namespace BattleMode.Entities.Systems
{
    public class BattleSystem
    {
        private readonly IMessageBus _messageBus;
        private readonly ICommandScheduler _scheduler;
        private readonly IMoveEffectCalculator _calculator;

        public BattleSystem(IMessageBus messageBus, ICommandScheduler scheduler, IMoveEffectCalculator calculator)
        {
            _messageBus = messageBus;
            _scheduler = scheduler;
            _calculator = calculator;
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

        public void ExecuteNextCommand(IEntityManager entityManager)
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
                    _messageBus.SendAction(new UseMoveAction(m.Move, m.Source, m.Target));
                    break;
                case ItemCommand i:
                    _messageBus.SendAction(new UseItemAction(i.Item, i.Target));
                    break;
                case ChangeCommand c:
                    _messageBus.SendAction(new UsePokemonChange(c.Pokemon, c.Target));
                    break;
            }

        }

        public void EndTurn(IEntityManager entityManager)
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

        public void UseMove(UseMoveAction action, IEntityManager entityManager)
        {
            var source = entityManager.GetComponentByTypeAndEntity<PokemonComponent>(action.Source).First().Pokemon;
            var move = action.Move;
            var target = entityManager.GetComponentByTypeAndEntity<PokemonComponent>(action.Target).First().Pokemon;

            _calculator.Init(source, move, target);
            if (!_calculator.IsHit)
            {
                _messageBus.SendAction(new MoveMissedAction());
                return;
            }

            var responseAction = new DoDamageAction(_calculator.Damage, GetEffect(_calculator.TypeModifier),
                action.Target, _calculator.IsCritical);

            _messageBus.SendAction(responseAction);
        }

        private static MoveEfficiency GetEffect(float typeModifier)
        {
            if (typeModifier.CompareTo(0) == 0)
                return MoveEfficiency.NoEffect;

            var result = typeModifier.CompareTo(1.0f);

            if (result > 0)
                return MoveEfficiency.VeryEffective;
            if (result < 0)
                return MoveEfficiency.NotEffective;

            return MoveEfficiency.Normal;
        }
    }
}