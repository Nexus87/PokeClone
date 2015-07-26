using System;
using System.Collections.Generic;

namespace BattleLib
{
	public struct ClientInfo 
	{
		public int Id { get; set; }
		public string CharName { get; set; }
		public string ClientName { get; set; }
		public int Hp { get; set; }
		public string Status { get; set; }
	}

	public interface IBattleObserver
	{
		IEnumerable<ClientInfo> getAllInfos();
		IEnumerable<ClientInfo> getInfo (params int[] ids);

		//void registertEventListener();
	}
}

