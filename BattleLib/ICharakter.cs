using System;

namespace BattleLib
{
	struct CharakterInfo {
		int Id { get;}
		string Name { get; }
		int HP { get; }
		string Status { get; }
	}

	public interface ICharakter
	{
		bool isKO();
		CharakterInfo getInfo();
	}
}

