using Base;
using BattleLib.Components.BattleState.Commands;
using GameEngine.Utils;
using System;
using System.Collections.Generic;

namespace BattleLib.Components.BattleState
{
    internal class WaitForActionState : IBattleState
    {
        private int clientCnt;
        private Dictionary<ClientIdentifier, ICommand> commands = new Dictionary<ClientIdentifier, ICommand>();
        public BattleStateComponent BattleState { get; set; }


        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            Validate(id, pkmn, "pkmn");
            commands[id] = new ChangeCommand(id, pkmn);
            clientCnt--;
        }

        public void SetItem(ClientIdentifier id, ClientIdentifier target, Base.Item item)
        {
            Validate(id, item, "item");
            commands[id] = new ItemCommand(id, item);
            clientCnt--;
        }

        public void SetMove(ClientIdentifier id, ClientIdentifier target, Move move)
        {
            Validate(id, move, "move");
            commands[id] = new MoveCommand(id, target, move);
            clientCnt--;
        }

        public IBattleState Update(BattleData data)
        {
            if (clientCnt != 0)
                return this;

            foreach (var c in commands)
                data.SetCommand(c.Key, c.Value);
            
            commands.Clear();

            return BattleState.ExecutionState;

        }

        private void Validate(ClientIdentifier id, Object obj, string varName)
        {
            if (!commands.ContainsKey(id))
                throw new InvalidOperationException("Id " + id.Name + " not found");

            obj.CheckNull(varName);

            if (commands[id] != null)
                throw new InvalidOperationException(id.Name + " already made a move for this turn.");
        }

        public void Init(BattleData data)
        {
            commands.Clear();

            foreach (var c in data.Clients)
                commands[c] = null;

            clientCnt = commands.Count;
        }

        public BattleStates State
        {
            get { return BattleStates.WaitForAction; }
        }
    }
}