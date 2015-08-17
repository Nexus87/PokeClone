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

    public class ActionEventArgs : EventArgs{
        public int Source { get; set; }
        public int Target { get; set; }
    }

    public class ExitEventArgs : EventArgs
    {
        public int Id { get; set; }
    }

    public class NewCharEventArg : EventArgs
    {
        public int Id { get; set; }
    }

    public delegate void ActionEvent(object sender, ActionEventArgs e);
    public delegate void ExitEvent(object sender, ExitEventArgs e);
    public delegate void NewTurnEvent(object sender, EventArgs e);
    public delegate void NewCharEvent(object sender, NewCharEventArg e);

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

