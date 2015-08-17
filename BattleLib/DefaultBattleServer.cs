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

//TODO use command pattern for exit/change/action
namespace BattleLib
{
	public class DefaultBattleServer : IBattleServer, IBattleObserver
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
                _clientInfo.Add(client, new ClientData { Id = cnt, Charakter = null });
				client.Id = cnt;
                cnt++;
            }
        }

        void initState()
        {
            _state.clientActionEvent += clientActionHandler;
            _state.clientExitEvent += clientExitHandler;
			_state.clientChangeEvent += clientChangeHandler;
        }

		void clientChangeHandler(Object sender, NewCharakterArgs args)
        {
			if (args.Source == null || _clientInfo.ContainsKey (args.Source))
				throw new ArgumentException ("Invalid client");

			_changeRequested.Add (args);
			_clientInfo [args.Source].Action = null;
        }

        void clientExitHandler(Object sender, RequestExitArgs args)
        {
            if (args.Source == null || !_clientInfo.ContainsKey(args.Source))
                throw new ArgumentException("Invalid client");

            _exitRequested.Add(args.Source);
            // No other action in this turn
            _clientInfo[args.Source].Action = null;
        }

		void clientActionHandler(Object sender, ClientActionArgs args){

            if (!_clientInfo.ContainsKey(args.Source))
                throw new ArgumentException("Source not found");

            var target = (from info in _clientInfo
                                  where info.Value.Id == args.TargetId
                                  select info.Key).FirstOrDefault();
			if (target == null)
				throw new ArgumentException ("Target not found");


            var source = _clientInfo[args.Source];

            source.Action = args.Action;
            source.ActionTarget = target;
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

		void execEscapes ()
		{
			foreach (var client in _exitRequested) {
                _clientInfo.Remove(client);
                exitEvent(this, new ExitEventArgs{Id = client.Id});
            }
        }

		void initScheduler ()
		{
			_scheduler.clearActions ();

			foreach (var client in _clientInfo.Values) {
				if (client.Action == null)
					continue;
				ICharakter target;
				try {
					target = _clientInfo [client.ActionTarget].Charakter;
				}
				catch (KeyNotFoundException) {
					target = null;
				}
				_scheduler.appendAction (new ExtendedActionData {
					Source = client.Charakter,
					Target = target,
					Action = client.Action,
					SourceId = client.Id,
					TargetId = client.ActionTarget.Id
				});
			}
		}

		void appylActions ()
		{
            initScheduler ();

            foreach (var action in _scheduler.schedulActions())
            {
                if (action.Source.isKO())
                    continue;
                action.Action.applyTo(action.Target);

                var eAction = (ExtendedActionData)action;
                var args = new ActionEventArgs { Source = eAction.SourceId, Target = eAction.TargetId };
                actionEvent(this, args);
            }

		}

		#region IBattleServer implementation
		public void start ()
		{
			if (_clientInfo.Count < 2)
				throw new InvalidOperationException ("Server needs at least 2 clients");

            requestCharakters();

            while (_clientInfo.Count > 1)
            {
                _exitRequested.Clear();
                _changeRequested.Clear();
				requestActions ();

				execEscapes ();
                execChanges ();
				appylActions ();

                requestCharakters();

                newTurnEvent(this, null);
			}
		}

        private void execChanges()
        {
            foreach (var data in _changeRequested)
            {
                _clientInfo[data.Source].Charakter = data.NewCharakter;
                newCharEvent(this, new NewCharEventArg { Id = data.Source.Id });
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
            public IAction Action { get; set; }
            public IBattleClient ActionTarget { get; set; }
		}
        class ChangeData
        {
            public IBattleClient Source { get; set; }
            public ICharakter NewChar { get; set; }
        }

		class ExtendedActionData : ActionData {
			public int SourceId { get; set; }
			public int TargetId { get; set; }
		}
        readonly Dictionary<IBattleClient, ClientData> _clientInfo = new Dictionary<IBattleClient, ClientData>();

        readonly List<IBattleClient> _exitRequested = new List<IBattleClient>();
		readonly List<NewCharakterArgs> _changeRequested = new List<NewCharakterArgs>();
		readonly DefaultBattleState _state = new DefaultBattleState();

		IActionScheduler _scheduler;
	}
}

