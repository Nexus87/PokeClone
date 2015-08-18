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
using Base;
using BattleLib.Interfaces;

//TODO use command pattern for exit/change/action
namespace BattleLib
{
	public class DefaultBattleServer : IBattleServer, IBattleObserver, CommandReceiver
	{

		public DefaultBattleServer(IActionScheduler scheduler, params IBattleClient[] clients) {
            if (scheduler == null)
                throw new NullReferenceException("Scheduler must not be null");
            if (clients == null)
                throw new NullReferenceException("Client must not be null");

            // To avoid null checks
            actionEvent += (a, b) => { };
            exitEvent += (a, b) => { };
            newTurnEvent += (a, b) => { };
            newCharEvent += (a, b) => { };

            _scheduler = scheduler;
            _state.clientCommandEvent += clientCommandHandler;
            appendClients(clients);
		}

        void appendClients(params IBattleClient[] clients)
        {
            int cnt = 0;
            foreach (var client in clients)
            {
                if (client == null)
                    throw new NullReferenceException("Client must not be null");

                _clientInfo.Add(client, new ClientData { Id = cnt, Charakter = null });
				client.Id = cnt;
                cnt++;
            }
        }


        private void clientCommandHandler(object sender, ClientCommandArgs args)
        {
            validateClient(args.Source);
            _clientInfo[args.Source].Command = args.Command;
        }

		void requestCharakters ()
		{
			var requestChar = from info in _clientInfo
			                  where (info.Value.Charakter == null || info.Value.Charakter.isKO ())
			                  select new {info.Key, info.Value};

            var toBeRemoved = new List<IBattleClient>();

            foreach (var client in requestChar)
            {
                ICharakter charakter = client.Key.requestCharakter();
                if (charakter == null)
                    toBeRemoved.Add(client.Key);
                else
                {
                    client.Value.Charakter = charakter;
                    newCharEvent(this, new NewCharEventArg{Id = client.Value.Id});
                }
            }

            foreach (var client in toBeRemoved)
            {
                _clientInfo.Remove(client);
                exitEvent(this, new ExitEventArgs{Id = client.Id});
            }

		}

		void requestActions ()
		{
			_state.resetState(_clientInfo.Keys);

			foreach(var client in _clientInfo.Keys){
				client.requestAction (_state);
			}
		}

		void initScheduler ()
		{
			_scheduler.clearCommands ();

			foreach (var client in _clientInfo.Values) {
                if (client.Command == null)
                    throw new InvalidOperationException("Command should not be null!");

				_scheduler.appendCommand (client.Command);
			}
		}

		void appylActions ()
		{
            initScheduler ();

            foreach (var command in _scheduler.scheduleCommands())
                command.execute(this);

		}

		#region IBattleServer implementation
		public void start ()
		{
			if (_clientInfo.Count < 2)
				throw new InvalidOperationException ("Server needs at least 2 clients");

            requestCharakters();

            while (_clientInfo.Count > 1)
            {
				requestActions ();
				appylActions ();

                requestCharakters();

                newTurnEvent(this, null);
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

		List<ClientInfo> fillCache() {
            var result = new List<ClientInfo>();
			foreach(var data in _clientInfo.Values) {
                result.Add(new ClientInfo
                {
					CharName = data.Charakter.Name,
					Hp = data.Charakter.HP,
					Id = data.Id
				});
			}

            return result;
		}

		public IEnumerable<ClientInfo> getAllInfos ()
		{
            return fillCache();
		}

		public IEnumerable<ClientInfo> getInfo (params int[] ids)
		{
            var infos = getAllInfos();

            return (from info in infos 
				where ids.Contains(info.Id)
				select info);
		}

		#endregion

		class ClientData {
			public int Id { get; set; }
			public ICharakter Charakter { get; set; }
            public IBattleClient ActionTarget { get; set; }
            public IClientCommand Command { get; set; }
		}
        class ChangeData
        {
            public IBattleClient Source { get; set; }
            public ICharakter NewChar { get; set; }
        }

        readonly Dictionary<IBattleClient, ClientData> _clientInfo = new Dictionary<IBattleClient, ClientData>();
		readonly DefaultBattleState _state = new DefaultBattleState();

		IActionScheduler _scheduler;

        void validateClient(IBattleClient client)
        {
            if (client == null || !_clientInfo.ContainsKey(client))
                throw new ArgumentException("Invalid client");
        }

        public void clientExit(IBattleClient source)
        {
            validateClient(source);

            //TODO uses rules to determine if an exit is possible
            int id = _clientInfo[source].Id;
            _clientInfo.Remove(source);
            exitEvent(this, new ExitEventArgs{ Id = id });
        }

        public void execMove(IBattleClient source, Move move, int targetId)
        {
            validateClient(source);
            //TODO implement
        }

        public void execChange(IBattleClient source, ICharakter charakter)
        {
            validateClient(source);
            if (charakter == null)
                throw new ArgumentException("Character must not be null");

            //TODO uses rules to determine if a character change is possible
            _clientInfo[source].Charakter = charakter;
        }
    }
}

