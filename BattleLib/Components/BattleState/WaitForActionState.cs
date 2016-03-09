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
        private BattleStateComponent state;

        public WaitForActionState(BattleStateComponent state)
        {
            this.state = state;
        }

        public void Init(IEnumerable<ClientIdentifier> clients)
        {
            commands.Clear();
            
            foreach (var c in clients)
                commands[c] = null;

            clientCnt = commands.Count;
        }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            Validate(id, pkmn, "pkmn");
            commands[id] = new ChangeCommand(id, pkmn);
            clientCnt--;
        }

        public void SetItem(ClientIdentifier id, Base.Item item)
        {
            Validate(id, item, "item");
            commands[id] = new ItemCommand(id, item);
            clientCnt--;
        }

        public void SetMove(ClientIdentifier id, Move move)
        {
            Validate(id, move, "move");
            commands[id] = new MoveCommand(id, move);
            clientCnt--;
        }

        public IBattleState Update(BattleData data)
        {
            if (clientCnt != 0)
                return this;

            foreach (var c in commands)
                data.SetCommand(c.Key, c.Value);
            
            commands.Clear();

            return state.exeState;

        }

        private void Validate(ClientIdentifier id, Object obj, string varName)
        {
            if (!commands.ContainsKey(id))
                throw new InvalidOperationException("Id " + id.Name + " not found");

            obj.CheckNull(varName);

            if (commands[id] != null)
                throw new InvalidOperationException(id.Name + " already made a move for this turn.");
        }

        public BattleStates State
        {
            get { return BattleStates.WaitForAction; }
        }
    }
}