
using BattleLib.Components.BattleState.Commands;
using System.Collections.Generic;

namespace BattleLib
{
    public interface ICommandScheduler
	{
        void AppendCommand(ICommand command);
        void ClearCommands();

        IEnumerable<ICommand> ScheduleCommands();
	}
}

