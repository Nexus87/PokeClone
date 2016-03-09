using BattleLib.Components.BattleState.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public override void Init()
        {
        }

        public override IBattleState Update(BattleData data)
        {
            scheduler.ClearCommands();
            scheduler.AppendCommand(data.playerCommand);
            scheduler.AppendCommand(data.aiCommand);

            foreach (var command in scheduler.ScheduleCommands())
            {
                var args = new ExecutionEventArgs(command.Source, command);

                OnCommandStarted(this, args);
                command.Execute(executer, data);
                OnCommandFinished(this, args);
            }

            data.aiCommand = null;
            data.playerCommand = null;

            if (data.AIPkmn.HP == 0 || data.PlayerPkmn.HP == 0)
                return state.charState;

            state.actionState.Init(data.Clients);
            return state.actionState;
        }

        public override BattleStates State
        {
            get { return BattleStates.Execute; }
        }
    }
}
