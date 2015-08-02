using System;

namespace BattleLib
{
	public interface IBattleClient
	{
		string ClientName { get; }

		void requestAction( IBattleState state );
		ICharakter requestCharakter();
	}
}

