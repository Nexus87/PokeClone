using BattleLib.Components.BattleState.Commands;
using GameEngine.Registry;
using System.Collections.Generic;
using System.Linq;

namespace BattleLib.Components.BattleState
{
    [GameType]
    public class ExecuteState : AbstractState
    {
        private ICommandScheduler scheduler;
        private CommandExecuter executer;

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

            List<ICommand> commands = ScheduleCommands(data);
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