using System.Collections.Generic;
using BattleMode.Entities.BattleState.Commands;

namespace BattleMode.Entities.BattleState
{
    public interface ICommandScheduler
	{
        void AppendCommand(ICommand command);
        void AppendCommands(IEnumerable<ICommand> commands);
        void ClearCommands();

        IEnumerable<ICommand> ScheduleCommands();
	}
}

