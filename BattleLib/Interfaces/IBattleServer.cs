using BattleLib.Interfaces;
using System;
using System.Collections.Generic;
namespace BattleLib
{
    public class ClientInfo
    {
        public String ClientName { get; internal set; }
        public int ClientId { get; internal set; }
        public int CharId { get; internal set; }
        public int CurrentHP { get; internal set; }
        public String CharName { get; internal set; }
    }

	public interface IBattleServer
	{
        event EventHandler ServerStart;
        event EventHandler NewTurn;
        event EventHandler ServerEnd;
        event EventHandler<String> ActionExecuted;
        event EventHandler ClientQuit;

        void AddClient(AbstractClient client);
        
		void Start();
        IEnumerable<ClientInfo> GetCurrentState();
	}
}

