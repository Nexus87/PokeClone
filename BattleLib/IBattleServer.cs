using System;
using System.Collections.Generic;

namespace BattleLib
{
	public interface IBattleServer
	{
		int registerClient(IBattleClient client);
		void unregisterClient(IBattleClient client);

		void setAction(int sourceId, int targetId, IAction action);

		IBattleObserver getObserver();

		IEnumerable<int> getClientList();
		int Capacity{ get; }
	}
}

