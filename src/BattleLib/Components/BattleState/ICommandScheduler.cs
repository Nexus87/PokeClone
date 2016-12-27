using System.Collections.Generic;
using BattleMode.Core.Components.BattleState.Commands;

namespace BattleMode.Core.Components.BattleState
{
    public interface ICommandScheduler
	{
        void AppendCommand(ICommand command);
        void AppendCommands(IEnumerable<ICommand> commands);
        void ClearCommands();

        IEnumerable<ICommand> ScheduleCommands();
	}
}

