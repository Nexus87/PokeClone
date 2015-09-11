using System;
using Base;
using BattleLib.Interfaces;

namespace BattleLib
{
	public interface IBattleClient
	{
		string ClientName { get; }
		IClientCommand requestAction();
		ICharakter requestCharakter();
	}
}

