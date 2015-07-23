using System;
using System.Collections.Generic;

namespace BattleLib
{
	public interface IBattleServer
	{
		int registerClient(IBattleClient client);
		void unregisterClient(int id);

		void setAction(int sourceId, int targetId, IAction action);

		IBattleObserver getObserver();

		IEnumerable<int> getClientList();
	}
}

