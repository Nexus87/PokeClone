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

        public override IBattleState Update(BattleData data)
        {
            scheduler.ClearCommands();
            scheduler.AppendCommands(data.Commands);

            foreach (var command in scheduler.ScheduleCommands())
            {
                command.Execute(executer, data);
            }

            data.ClearCommands();

            foreach (var id in data.Clients)
            {
                if(data.GetPokemon(id).HP == 0)
                    return BattleState.CharacterSetState;
            }
                
            return BattleState.ActionState;
        }

        public override BattleStates State
        {
            get { return BattleStates.Execute; }
        }
    }
}
