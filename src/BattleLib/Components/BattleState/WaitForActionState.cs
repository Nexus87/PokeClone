using Base;
using BattleLib.Components.BattleState.Commands;
using GameEngine.Registry;
using GameEngine.Utils;
using System;
using System.Collections.Generic;

namespace BattleLib.Components.BattleState
{
    [GameType]
    public class WaitForActionState : IBattleState
    {
        private int clientCnt;
        private readonly Dictionary<ClientIdentifier, ICommand> commands = new Dictionary<ClientIdentifier, ICommand>();
        public BattleStateComponent BattleState { get; set; }

        public BattleStates State
        {
            get { return BattleStates.WaitForAction; }
        }

        public bool IsDone { get; private set; }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            ValidateInput(id, pkmn, "pkmn");
            commands[id] = new ChangeCommand(id, pkmn);
            clientCnt--;
        }

        public void SetItem(ClientIdentifier id, ClientIdentifier target, Base.Item item)
        {
            ValidateInput(id, item, "item");
            commands[id] = new ItemCommand(id, item);
            clientCnt--;
        }

        public void SetMove(ClientIdentifier id, ClientIdentifier target, Move move)
        {
            ValidateInput(id, move, "move");
            commands[id] = new MoveCommand(id, target, move);
            clientCnt--;
        }

        public void Update(BattleData data)
        {
            if (clientCnt != 0)
                return;

            foreach (var c in commands)
                data.SetCommand(c.Key, c.Value);

            commands.Clear();

            IsDone = true;
        }

        public void Init(BattleData data)
        {
            commands.Clear();
            IsDone = false;
            foreach (var c in data.Clients)
                commands[c] = null;

            clientCnt = commands.Count;
        }

        private void ValidateInput(ClientIdentifier id, Object obj, string varName)
        {
            if (!commands.ContainsKey(id))
                throw new InvalidOperationException("Id " + id.Name + " not found");

            obj.CheckNull(varName);

            if (commands[id] != null)
                throw new InvalidOperationException(id.Name + " already made a move for this turn.");
        }
    }
}