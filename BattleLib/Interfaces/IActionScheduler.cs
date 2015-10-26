
using BattleLib.Components.BattleState.Commands;
using BattleLib.Interfaces;
using System.Collections.Generic;

namespace BattleLib
{
    public interface ICommandScheduler
	{
        void AppendCommand(IClientCommand command);
        void AppendCommand(ICommand command);
        void AppendCommand(IEnumerable<IClientCommand> commands);
        void ClearCommands();

        IEnumerable<IClientCommand> ScheduleCommands();

        IEnumerable<ICommand> Schedule();
	}
}

