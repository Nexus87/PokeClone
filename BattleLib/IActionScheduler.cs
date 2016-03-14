
using BattleLib.Components.BattleState.Commands;
using System.Collections.Generic;

namespace BattleLib
{
    public interface ICommandScheduler
	{
        void AppendCommand(ICommand command);
        void AppendCommands(IEnumerable<ICommand> commands);
        void ClearCommands();

        IEnumerable<ICommand> ScheduleCommands();
	}
}

