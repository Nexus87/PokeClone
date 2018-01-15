using System.Collections.Generic;
using System.Linq;
using BattleMode.Entities.BattleState.Commands;
using BattleMode.Shared;
using GameEngine.TypeRegistry;

namespace BattleMode.Entities.BattleState.States
{
    [GameService(typeof(ExecuteState))]
    public class ExecuteState : AbstractState
    {
        private readonly ICommandScheduler _scheduler;
        private readonly CommandExecuter _executer;

        public ExecuteState(ICommandScheduler scheduler, CommandExecuter executer)
        {
            _scheduler = scheduler;
            _executer = executer;
        }

        public override BattleStates State => BattleStates.Execute;

        public override void Init(BattleData data)
        {
            IsDone = false;
        }

        public override void Update(BattleData data)
        {
            _executer.Data = data;

            var commands = ScheduleCommands(data);
            commands.ForEach(c => c.Execute(_executer));

            data.ClearCommands();
            IsDone = true;
        }

        private List<ICommand> ScheduleCommands(BattleData data)
        {
            _scheduler.ClearCommands();
            _scheduler.AppendCommands(data.Commands);
            return _scheduler.ScheduleCommands().ToList();
        }
    }
}