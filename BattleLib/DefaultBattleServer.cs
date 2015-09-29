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

namespace BattleLib
{
    public class DefaultBattleServer : IBattleServer, ICommandReceiver
    {

        public DefaultBattleServer(ICommandScheduler scheduler, IBattleRules rules, params AbstractClient[] clients)
        {
            if (scheduler == null)
                throw new ArgumentNullException("scheduler", "Scheduler must not be null");
            if (clients == null)
                throw new ArgumentNullException("clients", "Client must not be null");
            if (rules == null)
                throw new ArgumentNullException("rules", "Rules must not be null");

            _rules = rules;
            _scheduler = scheduler;
            appendClients(clients);
        }

        void validateClient(AbstractClient client)
        {
            if (client == null || !_clientInfo.ContainsKey(client))
                throw new ArgumentException("Invalid client");
        }

        void appendClients(params AbstractClient[] clients)
        {
            int cnt = _clientInfo.Count;
            foreach (var client in clients)
            {
                if (client == null)
                    throw new ArgumentNullException("clients", "Client must not be null");

                _clientInfo.Add(client, new ClientData { Id = cnt, Character = null });
                client.Id = cnt;
                cnt++;
            }
        }

        void requestCharakters ()
        {
            var requestChar = from info in _clientInfo
                              where (info.Value.Character == null || info.Value.Character.IsKO ())
                              select new {info.Key, info.Value};

            var toBeRemoved = new List<AbstractClient>();

            foreach (var client in requestChar)
            {
                ICharacter charakter = client.Key.RequestCharacter();
                if (charakter == null)
                    toBeRemoved.Add(client.Key);
                else
                {
                    client.Value.Character = charakter;
                    ActionExecuted(this, "New charakter!");
                }
            }

            foreach (var client in toBeRemoved)
            {
                _clientInfo.Remove(client);
                ClientQuit(this, null);
            }

        }

        void requestActions ()
        {
            foreach(var client in _clientInfo.Keys){
                _clientInfo[client].Command = client.RequestAction();
            }
        }

        void initScheduler ()
        {
            _scheduler.ClearCommands ();

            foreach (var client in _clientInfo.Values) {
                if (client.Command == null)
                    throw new InvalidOperationException("Command should not be null!");

                _scheduler.AppendCommand (client.Command);
            }
        }

        void appylActions ()
        {
            initScheduler ();

            foreach (var command in _scheduler.ScheduleCommands())
                command.Execute(this);

        }

        void updateClients()
        {
            var state = GetCurrentState();
            foreach (var client in _clientInfo.Keys)
                client.BattleState = state;
        }
        #region IBattleServer implementation
        public void Start ()
        {
            if (_clientInfo.Count < 2)
                throw new InvalidOperationException ("Server needs at least 2 clients");
            _isRunning = true;
            ServerStart(this, null);
            requestCharakters();

            while (_clientInfo.Count > 1)
            {
                NewTurn(this, null);
                requestActions ();
                appylActions ();

                requestCharakters();
                updateClients();
            }
            _isRunning = false;
            ServerEnd(this, null);
        }

        #endregion


        class ClientData {
            public int Id { get; set; }
            public ICharacter Character { get; set; }
            public IClientCommand Command { get; set; }
        }

        readonly Dictionary<AbstractClient, ClientData> _clientInfo = new Dictionary<AbstractClient, ClientData>();
        IBattleRules _rules;
        ICommandScheduler _scheduler;
        bool _isRunning;
        
        public void ClientExit(AbstractClient source)
        {
            validateClient(source);

            if (!_rules.CanEscape())
                return;

            _clientInfo.Remove(source);
            ClientQuit(this, null);
        }

        public void ExecMove(AbstractClient source, Move move, int targetId)
        {
            validateClient(source);

            var sourceChar = _clientInfo[source].Character;
            var targetChar = (from info in _clientInfo.Values
                             where info.Id == targetId
                             select info.Character).FirstOrDefault();

            _rules.ExecMove(sourceChar, move, targetChar);
        }

        public void ExecChange(AbstractClient source, ICharacter character)
        {
            validateClient(source);
            if (character == null)
                throw new ArgumentException("Character must not be null");

            if (!_rules.CanChange())
                return;

            _clientInfo[source].Character = character;
        }

        ClientInfo toInfo(AbstractClient client){
            var data = _clientInfo[client];
            return new ClientInfo {
                ClientName = client.ClientName,
                ClientId = data.Id,
                CharId = data.Character == null ? -1 : data.Character.Id,
                CharName = data.Character == null ? null : data.Character.Name,
                CurrentHP = data.Character == null ? -1 : data.Character.HP
            };
        }

        public event EventHandler ServerStart = (a, b) => { };
        public event EventHandler ServerEnd = (a, b) => { };
        public event EventHandler<string> ActionExecuted = (a, b) => { };
        public event EventHandler ClientQuit = (a, b) => { };
        public event EventHandler NewTurn = (a, b) => { };

        public void AddClient(AbstractClient client)
        {
            appendClients(client);
        }


        public IEnumerable<ClientInfo> GetCurrentState()
        {
            if (!_isRunning)
                return null;
            List<ClientInfo> result = new List<ClientInfo>(_clientInfo.Count);
            foreach (var client in _clientInfo.Keys)
                result.Add(toInfo(client));

            return result;
        }
    }
}

