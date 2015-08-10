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

	public delegate void ActionEvent(int source, int target, String message);
	public delegate void ExitEvent(int id, String message);
	public delegate void NewTurnEvent();
	public delegate void NewCharEvent(int id);

	public interface IBattleObserver
	{
		event ActionEvent actionEvent;
		event ExitEvent exitEvent;
		event NewTurnEvent newTurnEvent;
		event NewCharEvent newCharEvent;

		IEnumerable<ClientInfo> getAllInfos();
		IEnumerable<ClientInfo> getInfo (params int[] ids);

		//void registertEventListener();
	}
}

