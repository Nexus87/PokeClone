using System.Collections.Generic;
using BattleMode.Components.BattleState.Commands;

namespace BattleMode.Components.BattleState
{
    public interface ICommandScheduler
	{
        void AppendCommand(ICommand command);
        void AppendCommands(IEnumerable<ICommand> commands);
        void ClearCommands();

        IEnumerable<ICommand> ScheduleCommands();
	}
}

