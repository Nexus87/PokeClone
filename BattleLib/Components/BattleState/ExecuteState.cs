using BattleLib.Components.BattleState.Commands;
using System;

namespace BattleLib.Components.BattleState
{
    public class ExecutionEventArgs : EventArgs
    {
        public ClientIdentifier Source { get; private set; }
        public ICommand Command { get; private set; }

        public ExecutionEventArgs(ClientIdentifier source, ICommand command)
        {
            Command = command;
            Source = source;
        }
    }

    public class ExecuteState : AbstractState
    {
        public EventHandler<ExecutionEventArgs> OnCommandStarted = delegate { };
        public EventHandler<ExecutionEventArgs> OnCommandFinished = delegate { };

        ICommandScheduler scheduler;
        CommandExecuter executer;

        BattleStateComponent state;

        public ExecuteState(BattleStateComponent state, ICommandScheduler scheduler, CommandExecuter executer)
        {
            this.scheduler = scheduler;
            this.executer = executer;
            this.state = state;
        }

        public override IBattleState Update(BattleData data)
        {
            scheduler.ClearCommands();
            scheduler.AppendCommands(data.Commands);

            foreach (var command in scheduler.ScheduleCommands())
            {
                var args = new ExecutionEventArgs(command.Source, command);

                OnCommandStarted(this, args);
                command.Execute(executer, data);
                OnCommandFinished(this, args);
            }

            data.ClearCommands();

            foreach (var id in data.Clients)
            {
                if(data.GetPokemon(id).HP == 0)
                    return state.CharacterSetState;
            }
                
            return state.ActionState;
        }

        public override BattleStates State
        {
            get { return BattleStates.Execute; }
        }
    }
}
