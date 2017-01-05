using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using BattleMode.Entities.BattleState.Commands;
using BattleMode.Shared;
using GameEngine.Globals;
using GameEngine.TypeRegistry;

namespace BattleMode.Entities.BattleState.States
{
    [GameService(typeof(WaitForMoveState))]
    public class WaitForMoveState : IBattleState
    {
        private readonly Dictionary<ClientIdentifier, ICommand> _commands = new Dictionary<ClientIdentifier, ICommand>();
        public BattleStateEntity BattleState { get; set; }

        public BattleStates State => BattleStates.WaitForAction;

        public bool IsDone { get; private set; }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            ValidateInput(id, pkmn, "pkmn");
            _commands[id] = new ChangeCommand(id, pkmn);
        }

        public void SetItem(ClientIdentifier id, ClientIdentifier target, Item item)
        {
            ValidateInput(id, item, "item");
            _commands[id] = new ItemCommand(id, item);
        }

        public void SetMove(ClientIdentifier id, ClientIdentifier target, Move move)
        {
            ValidateInput(id, move, "move");
            _commands[id] = new MoveCommand(id, target, move);
        }

        public void Update(BattleData data)
        {
            if (_commands.Any(x => x.Value == null))
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
        }

        private void ValidateInput(ClientIdentifier id, object obj, string varName)
        {
            if (!_commands.ContainsKey(id))
                throw new InvalidOperationException("Id " + id.Name + " not found");

            obj.CheckNull(varName);

            if (_commands[id] != null)
                throw new InvalidOperationException(id.Name + " already made a move for this turn.");
        }
    }
}