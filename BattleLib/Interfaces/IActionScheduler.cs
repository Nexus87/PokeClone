
using BattleLib.Interfaces;
using System.Collections.Generic;

namespace BattleLib
{
    public interface ICommandScheduler
	{
        void AppendCommand(IClientCommand command);
        void AppendCommand(IEnumerable<IClientCommand> commands);
        void ClearCommands();

        IEnumerable<IClientCommand> ScheduleCommands();
	}
}

