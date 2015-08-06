//
//  DefautltBattleServer.cs
//
//  Author:
//       Nexxuz0 <>
//
//  Copyright (c) 2015 Nexxuz0
//
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
using System;
using System.Collections.Generic;

using System.Linq;

namespace BattleLib
{
	public class DefaultBattleServer : IBattleServer, IBattleObserver
	{

		public DefaultBattleServer(IActionScheduler scheduler, params IBattleClient[] clients) {
            if (scheduler == null)
                throw new NullReferenceException("Scheduler must not be null");
            if (clients == null)
                throw new NullReferenceException("Client must not be null");

            _scheduler = scheduler;
            appendClients(clients);
            initState();
		}

        void appendClients(params IBattleClient[] clients)
        {
            int cnt = 0;
            foreach (var client in clients)
            {
                if (client == null)
                    throw new NullReferenceException("Client must not be null");
                _clientInfo.Add(client, new ClientData { Client = client, Id = cnt, Charakter = null });
				client.Id = cnt;
                cnt++;
            }
        }

        void initState()
        {
            _state.clientActionEvent += clientActionHandler;
            _state.clientExitEvent += clientExitHandler;
        }

        void clientExitHandler(IBattleClient source)
        {
            if (source == null || !_clientInfo.ContainsKey(source))
                throw new ArgumentException("Invalid client");

            _exitRequested.Add(source);
            // No other action in this turn
            _clientInfo[source].Action = null;
        }

		void clientActionHandler(IBattleClient client, IAction action, int targetId){

            if (!_clientInfo.ContainsKey(client))
                throw new ArgumentException("Source not found");

            var target = (from info in _clientInfo
                                  where info.Value.Id == targetId
                                  select info.Key).FirstOrDefault();
			if (target == null)
				throw new ArgumentException ("Target not found");

             
            var source = _clientInfo[client];

            source.Action = action;
            source.ActionTarget = target;
		}

		void requestCharakters ()
		{
			var requestChar = from info in _clientInfo
			                  where ( info.Value.Charakter == null || info.Value.Charakter.isKO() )
			                  select info.Value;

            var toBeRemoved = new List<IBattleClient>();
			
            foreach(var client in requestChar) {
                ICharakter charakter = client.Client.requestCharakter ();
				if (charakter == null)
					toBeRemoved.Add (client.Client);
				else {
					client.Charakter = charakter;
					if (newCharEvent != null)
						newCharEvent.Invoke (client.Id);
				}
            }

			foreach (var client in toBeRemoved) {
				_clientInfo.Remove (client);
				if (exitEvent != null)
					exitEvent.Invoke (client.Id, "No more Chars");
			}

			_cachedInfo.Clear ();
		}

		void requestActions ()
		{
			foreach(var client in _clientInfo.Keys){
				client.requestAction (_state);
			}
		}

		void appylActions ()
		{
            foreach (var client in _exitRequested)
            {
                _clientInfo.Remove(client);
				if (exitEvent != null)
					exitEvent.Invoke (client.Id, "Escaped");
            }

            foreach(var client in _clientInfo.Values){
                if (client.Action == null)
                    continue;

                ICharakter target;
                try{
                    target = _clientInfo[client.ActionTarget].Charakter;
                }
                catch(KeyNotFoundException){
                    target = null;
                }

                _scheduler.appendAction(new ActionData{
                    Source = client.Charakter,
                    Target = target,
                    Action = client.Action
                });
            }

            foreach (var action in _scheduler.schedulActions())
            {
                if (action.Source.isKO())
                    continue;
                action.Action.applyTo(action.Target);
				// TODO find a way to get the source and target id
				if (actionEvent != null)
					actionEvent.Invoke (0, 0, "Message");
            }

			_cachedInfo.Clear ();
		}

		#region IBattleServer implementation
		public void start ()
		{
			if (_clientInfo.Count < 2)
				throw new InvalidOperationException ("Server needs at least 2 clients");

            requestCharakters();

            while (_clientInfo.Count > 1)
            {
                _state.resetState(_clientInfo.Keys);
				_scheduler.clearActions ();
                _exitRequested.Clear();

				requestActions ();
				appylActions ();
                requestCharakters();
				if (newTurnEvent != null)
					newTurnEvent.Invoke ();
			}
		}
		public IBattleObserver getObserver ()
		{
			return this;
		}
		#endregion

		#region IBattleObserver implementation

		public event ActionEvent actionEvent;
		public event ExitEvent exitEvent;
		public event NewTurnEvent newTurnEvent;
		public event NewCharEvent newCharEvent;

		void fillCache() {
			foreach(var data in _clientInfo.Values) {
				_cachedInfo.Add (new ClientInfo {
					CharName = data.Charakter.Name,
					ClientName = data.Client.ClientName,
					Hp = data.Charakter.HP,
					Id = data.Id,
					Status = data.Charakter.Status
				});
			}
		}

		public IEnumerable<ClientInfo> getAllInfos ()
		{
			if (_cachedInfo.Count == 0)
				fillCache ();
			return _cachedInfo;
		}

		public IEnumerable<ClientInfo> getInfo (params int[] ids)
		{
			if (_cachedInfo.Count == 0)
				fillCache ();

			return (from info in _cachedInfo 
				where ids.Contains(info.Id)
				select info);
		}

		#endregion

		class ClientData {
			public IBattleClient Client { get; set; }
			public int Id { get; set; }
			public ICharakter Charakter { get; set; }
            public IAction Action { get; set; }
            public IBattleClient ActionTarget { get; set; }
		}

        readonly Dictionary<IBattleClient, ClientData> _clientInfo = new Dictionary<IBattleClient, ClientData>();

        readonly List<IBattleClient> _exitRequested = new List<IBattleClient>();
		readonly DefaultBattleState _state = new DefaultBattleState();
		readonly List<ClientInfo> _cachedInfo = new List<ClientInfo>();

		IActionScheduler _scheduler;
	}
}

