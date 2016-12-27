using System;
using System.Collections.Generic;
using Base;
using BattleMode.Components.BattleState.Commands;
using BattleMode.Shared;
using GameEngine.GUI.Utils;
using GameEngine.TypeRegistry;

namespace BattleMode.Components.BattleState
{
    [GameType]
    public class WaitForActionState : IBattleState
    {
        private int _clientCnt;
        private readonly Dictionary<ClientIdentifier, ICommand> _commands = new Dictionary<ClientIdentifier, ICommand>();
        public BattleStateComponent BattleState { get; set; }

        public BattleStates State => BattleStates.WaitForAction;

        public bool IsDone { get; private set; }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            ValidateInput(id, pkmn, "pkmn");
            _commands[id] = new ChangeCommand(id, pkmn);
            _clientCnt--;
        }

        public void SetItem(ClientIdentifier id, ClientIdentifier target, Item item)
        {
            ValidateInput(id, item, "item");
            _commands[id] = new ItemCommand(id, item);
            _clientCnt--;
        }

        public void SetMove(ClientIdentifier id, ClientIdentifier target, Move move)
        {
            ValidateInput(id, move, "move");
            _commands[id] = new MoveCommand(id, target, move);
            _clientCnt--;
        }

        public void Update(BattleData data)
        {
            if (_clientCnt != 0)
                return;

            foreach (var c in _commands)
                data.SetCommand(c.Key, c.Value);

            _commands.Clear();

            IsDone = true;
        }

        public void Init(BattleData data)
        {
            _commands.Clear();
            IsDone = false;
            foreach (var c in data.Clients)
                _commands[c] = null;

            _clientCnt = _commands.Count;
        }

        private void ValidateInput(ClientIdentifier id, Object obj, string varName)
        {
            if (!_commands.ContainsKey(id))
                throw new InvalidOperationException("Id " + id.Name + " not found");

            obj.CheckNull(varName);

            if (_commands[id] != null)
                throw new InvalidOperationException(id.Name + " already made a move for this turn.");
        }
    }
}