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
            int cnt = 0;
            foreach (var client in clients)
            {
                if (client == null)
                    throw new NullReferenceException("Client must not be null");
                _clients.Add(client);
                _clientInfo.Add(new ClientData { Client = client, Id = cnt, Charakter = null });
                cnt++;
            }
		}

		void clientActionHandler(IBattleClient client, IAction action, int targetId){
			var sourceCharakter = (from info in _clientInfo
			                       where info.Client == client
			                       select info.Charakter).FirstOrDefault ();
			
			if (sourceCharakter == null)
				throw new ArgumentException ("Source not found");

			var targetCharater = (from info in _clientInfo
			                      where info.Id == targetId
			                      select info.Charakter).FirstOrDefault ();
			
			if (targetCharater == null)
				throw new ArgumentException ("Target not found");

			_scheduler.appendAction (new ActionData{ 
				Source = sourceCharakter, 
				Target = targetCharater,
				Action = action }
			);

		}

		void requestCharakters ()
		{
			var requestChar = from info in _clientInfo
			                  where info.Charakter == null
			                  select info;
			
			
			for(int i = 0; i < requestChar.Count(); ++i){
				var info = requestChar.ElementAt (i);
				ICharakter charakter = info.Client.requestCharakter ();
				if (charakter == null)
					_clients.Remove (info.Client);
				else
					info.Charakter = charakter;
			}
		}

		void requestActions ()
		{
			foreach(var client in _clients){
				client.requestAction (_state);
			}
		}

		void appylActions ()
		{
			foreach(var action in _scheduler.schedulActions()){
				action.Action.applyTo (action.Target);
			}
		}

		#region IBattleServer implementation
		public void start ()
		{
			if (_clients.Count < 2)
				throw new InvalidOperationException ("Server needs at least 2 clients");

			while(_clients.Count > 1)
            {
				_state.resetState (_clients);
				_scheduler.clearActions ();

				requestCharakters ();
                if (_clients.Count() < 2)
                    break;
				requestActions ();
				appylActions ();
			}
		}
		public IBattleObserver getObserver ()
		{
			return this;
		}
		#endregion

		#region IBattleObserver implementation

		public IEnumerable<ClientInfo> getAllInfos ()
		{
			throw new NotImplementedException ();
		}

		public IEnumerable<ClientInfo> getInfo (params int[] ids)
		{
			throw new NotImplementedException ();
		}

		#endregion

		struct ClientData {
			public IBattleClient Client { get; set; }
			public int Id { get; set; }
			public ICharakter Charakter { get; set; }
		}

		readonly List<ClientData> _clientInfo = new List<ClientData>();
		readonly DefaultBattleState _state = new DefaultBattleState();
		readonly List<IBattleClient> _clients = new List<IBattleClient>();

		IActionScheduler _scheduler;
	}
}

