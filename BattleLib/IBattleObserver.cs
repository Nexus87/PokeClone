using System;
using System.Collections.Generic;

namespace BattleLib
{
	struct ClientInfo 
	{
		int Id { get; }
		string CharName { get; }
		string ClientName { get; }
		uint Hp { get; }
		string Status { get; }
	}

	public interface IBattleObserver
	{
		IEnumerable<ClientInfo> getAllInfos();
		IEnumerable<ClientInfo> getInfo (params int[] ids);

		//void registertEventListener();
	}
}

