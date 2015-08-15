using System;
using Base;

namespace BattleLib
{
	public interface IBattleClient
	{
		string ClientName { get; }
		int Id { get; set; }
		void requestAction( IBattleState state );
		ICharakter requestCharakter();
	}
}

