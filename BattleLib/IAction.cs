using System;

namespace BattleLib
{
	enum ActionType {
		immediate,
		ranked
	}
		
	public interface IAction
	{
		void applyTo(ICharakter charakter);
		ActionType Type { get; }
	}
}

