using System;

using System.Collections.Generic;
using Base;

namespace BattleLib
{
	public class ActionData{
		public ICharakter Target { get; set; }
		public ICharakter Source { get; set; }
		public IAction Action { get; set; }
	}

	public interface IActionScheduler
	{
		void appendAction(ActionData action);
		void appendAction(IEnumerable<ActionData> actions);
		void clearActions();

		IEnumerable<ActionData> schedulActions();
	}
}

