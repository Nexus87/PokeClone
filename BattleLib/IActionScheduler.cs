using System;

using System.Collections.Generic;

namespace BattleLib
{
	public interface IActionScheduler
	{
		void appendAction(IAction action);
		void appendAction(IEnumerable<IAction> actions);
		void clearActions();

		IEnumerable<IAction> schedulActions();
	}
}

