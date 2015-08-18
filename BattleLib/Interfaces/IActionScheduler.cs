using System;

using System.Collections.Generic;
using Base;
using BattleLib.Interfaces;

namespace BattleLib
{
	public interface IActionScheduler
	{
        void appendCommand(IClientCommand command);
        void appendCommand(IEnumerable<IClientCommand> commands);
        void clearCommands();

        IEnumerable<IClientCommand> scheduleCommands();
	}
}

