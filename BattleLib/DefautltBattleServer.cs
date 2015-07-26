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
using System.Threading;

namespace BattleLib
{
	public class DefautltBattleServer : IBattleServer, IBattleObserver
	{
		public delegate void Event(Object sender, EventArgs args);
		public event Event startEvent;
		public event Event newRoundEvent;
		public event Event endEvent;

		AutoResetEvent _waitHandle;
		int _numClients;
		readonly List<IBattleClient> _registeredClients = new List<IBattleClient>();
		readonly Dictionary<int, ClientData> _clients = new Dictionary<int, ClientData>();
		readonly Dictionary<int, ActionData> _actions = new Dictionary<int, ActionData>();
		IActionScheduler _scheduler;



		public DefautltBattleServer (int numClients, IActionScheduler scheduler)
		{
			_numClients = numClients;
			_scheduler = scheduler;
		}

		public DefautltBattleServer(IActionScheduler scheduler) {
			_numClients = 2;
			_scheduler = scheduler;
		}

		void ThreadMethod() {
			if (startEvent != null)
				startEvent.Invoke (this, null);

			_waitHandle = new AutoResetEvent (false);
			while(true) {
				_waitHandle.WaitOne ();

				if (_registeredClients.Count != _numClients)
					break;
				
				_scheduler.appendAction( _actions.Values );

				foreach(var action in _scheduler.schedulActions()) {

					action.Action.applyTo (action.Target);
				}

				if (newRoundEvent != null)
					newRoundEvent.Invoke (this, null);
			}

			if (endEvent != null)
				endEvent.Invoke (this, null);
		}

		void startBattle() {
			var serverThread = new Thread(ThreadMethod);
			serverThread.Start ();
		}

		void executeActions ()
		{
			if (_waitHandle == null)
				throw new InvalidOperationException ("Battle has not started");

			_waitHandle.Set ();
		}

		int appendClient(IBattleClient client) {
			_registeredClients.Add (client);

			int id = 0;
			foreach(var key in _clients.Keys){
				id = Math.Max (id, key);
			}

			_clients.Add (id, new ClientData
				{ 
					Charakter = null, 
					ClientName = client.ClientName
				}
			);

			return id;
		}

		void removeClient(IBattleClient client){
			int id = client.Id;

			_registeredClients.Remove (client);
			_clients.Remove (id);
		}

		#region IBattleServer implementation

		public int Capacity {
			get {
				return _numClients - _registeredClients.Count;
			}
		}

		public int registerClient (IBattleClient client)
		{
			if (_registeredClients.Contains (client))
				throw new ArgumentException ("Client is already registered");

			if (Capacity == 0)
				throw new InvalidOperationException ("There are already enough clients registered");
			
			int id = appendClient (client);

			if (Capacity == 0)
				startBattle ();

			return id;
		}

		public void unregisterClient (IBattleClient client)
		{
			if (!_registeredClients.Contains (client))
				throw new ArgumentException ("Client is not registered");

			removeClient (client);

			if (_waitHandle != null)
				_waitHandle.Set ();
		}

		public void setAction (int sourceId, int targetId, IAction action)
		{
			if(!_actions.ContainsKey(sourceId))
				throw new ArgumentException ("Client already placed a action");
			
			ClientData source;
			ClientData target;
			try{
				source = _clients[sourceId];
				target = _clients[targetId];
			}
			catch(KeyNotFoundException){
				throw new ArgumentException ("Target or source not found");
			}
			
			_actions.Add (sourceId, new ActionData{ Action = action, Source = source, Target = target });

			if (_actions.Count == _clients.Count)
				executeActions ();
		}

		public IBattleObserver getObserver ()
		{
			return this;
		}

		public IEnumerable<int> getClientList ()
		{
			return _clients.Keys;
		}

		#endregion

		static ClientInfo toClientInfo(int id, ClientData data) {
			var info = new ClientInfo {
				Id = id,
				CharName = data.Charakter.Name,
				ClientName = data.ClientName,
				Hp = data.Charakter.HP,
				Status = data.Charakter.Status
			};

			return info;
		}

		#region IBattleObserver implementation

		public IEnumerable<ClientInfo> getAllInfos ()
		{
			var list = new List<ClientInfo> ();
			foreach (var client in _clients) {
				list.Add (toClientInfo (client.Key, client.Value));
			}

			return list;
		}

		public IEnumerable<ClientInfo> getInfo (params int[] ids)
		{
			var list = new List<ClientInfo> ();
			foreach(int id in ids ){
				ClientData data;
				_clients.TryGetValue (id, out data);
				if (data == null)
					throw new ArgumentException ("No client with id " + id + " found");
				
				list.Add (toClientInfo (id, data));
			}

			return list;
		}

		#endregion

		class ClientData {
			public ICharakter Charakter { get; set; }
			public string ClientName { get; set; }
		}
	}
}

