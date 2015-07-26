using System;

using System.Collections.Generic;

namespace BattleLib
{
	public struct ActionData{
		public ICharakter Target { get; set; }
		public ICharakter Source { get; set; }
		public IAction Action { get; set; }
	}
	public interface IActionScheduler
	{
		void appendAction(IAction action);
		void appendAction(IEnumerable<ActionData> actions);
		void clearActions();

		IEnumerable<ActionData> schedulActions();
	}
}

