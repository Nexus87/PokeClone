using System.Collections.Generic;

namespace BattleMode.Shared
{
    public interface ICommandScheduler
	{
        void AppendCommand(ICommand command);
        void AppendCommands(IEnumerable<ICommand> commands);
        void ClearCommands();

        IEnumerable<ICommand> ScheduleCommands();
	}
}

