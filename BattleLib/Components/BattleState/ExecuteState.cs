using BattleLib.Components.BattleState.Commands;
using System;

namespace BattleLib.Components.BattleState
{
    public class ExecuteState : AbstractState
    {
        ICommandScheduler scheduler;
        CommandExecuter executer;

        public ExecuteState(ICommandScheduler scheduler, CommandExecuter executer)
        {
            this.scheduler = scheduler;
            this.executer = executer;
        }

        public override void Init(BattleData data)
        {
            IsDone = false;
        }
        public override void Update(BattleData data)
        {
            scheduler.ClearCommands();
            scheduler.AppendCommands(data.Commands);

            foreach (var command in scheduler.ScheduleCommands())
                command.Execute(executer, data);

            data.ClearCommands();
            IsDone = true;
        }

        public override BattleStates State
        {
            get { return BattleStates.Execute; }
        }
    }
}
