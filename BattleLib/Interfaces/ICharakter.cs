using System;

namespace BattleLib
{

	public interface ICharakter
	{
		string Name { get; }
		int HP { get; }
		string Status { get; }
		bool isKO();
	}
}

