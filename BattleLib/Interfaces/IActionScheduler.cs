using System;

using System.Collections.Generic;
using Base;
using BattleLib.Interfaces;

namespace BattleLib
{
	public interface IActionScheduler
	{
        void AppendCommand(IClientCommand command);
        void AppendCommand(IEnumerable<IClientCommand> commands);
        void ClearCommands();

        IEnumerable<IClientCommand> ScheduleCommands();
	}
}

