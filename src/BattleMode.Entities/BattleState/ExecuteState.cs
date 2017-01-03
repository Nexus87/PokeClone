using System.Collections.Generic;
using System.Linq;
using BattleMode.Entities.BattleState.Commands;
using GameEngine.TypeRegistry;

namespace BattleMode.Entities.BattleState
{
    [GameService(typeof(ExecuteState))]
    public class ExecuteState : AbstractState
    {
        private readonly ICommandScheduler scheduler;
        private readonly CommandExecuter executer;

        public ExecuteState(ICommandScheduler scheduler, CommandExecuter executer)
        {
            this.scheduler = scheduler;
            this.executer = executer;
        }

        public override BattleStates State
        {
            get { return BattleStates.Execute; }
        }

        public override void Init(BattleData data)
        {
            IsDone = false;
        }

        public override void Update(BattleData data)
        {
            executer.Data = data;

            var commands = ScheduleCommands(data);
            commands.ForEach(c => c.Execute(executer));

            data.ClearCommands();
            IsDone = true;
        }

        private List<ICommand> ScheduleCommands(BattleData data)
        {
            scheduler.ClearCommands();
            scheduler.AppendCommands(data.Commands);
            return scheduler.ScheduleCommands().ToList();
        }
    }
}