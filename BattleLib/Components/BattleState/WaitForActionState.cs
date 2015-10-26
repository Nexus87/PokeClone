using Base;
using BattleLib.Components.BattleState.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState
{
    class WaitForActionState : IBattleState
    {
        ICommand playerCommand = null;
        ICommand aiCommand = null;

        ClientIdentifier player;
        ClientIdentifier ai;

        bool done = false;
        public WaitForActionState(ClientIdentifier player, ClientIdentifier ai)
        {
            this.player = player;
            this.ai = ai;
        }

        public void Init()
        {
            playerCommand = null;
            aiCommand = null;
            done = false;
        }

        public bool IsDone()
        {
            return done;
        }

        public void Update(BattleData data)
        {
            data.playerCommand = playerCommand;
            data.aiCommand = aiCommand;

            done = playerCommand != null && aiCommand != null;
        }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            if (id == player)
            {
                Validate(playerCommand, "Player");
                playerCommand = new BattleLib.Components.BattleState.Commands.ChangeCommand(id, pkmn);
            }
            else if (id == ai)
            {
                Validate(aiCommand, "AI");
                aiCommand = new BattleLib.Components.BattleState.Commands.ChangeCommand(id, pkmn);
            }
        }

        private void Validate(ICommand target, string Name)
        {
            if (target != null)
                throw new InvalidOperationException(Name + " already made a move for this turn.");
        }

        public void SetMove(ClientIdentifier id, Base.Move move)
        {
            if (id == player)
            {
                Validate(playerCommand, "Player");
                playerCommand = new BattleLib.Components.BattleState.Commands.MoveCommand(id, move);
            }
            else if (id == ai)
            {
                Validate(aiCommand, "AI");
                aiCommand = new BattleLib.Components.BattleState.Commands.MoveCommand(id, move);
            }
        }

        public void SetItem(ClientIdentifier id, Base.Item item)
        {
            if (id == player)
            {
                Validate(playerCommand, "Player");
                playerCommand = new BattleLib.Components.BattleState.Commands.ItemCommand(id, item);
            }
            else if (id == ai)
            {
                Validate(aiCommand, "AI");
                aiCommand = new BattleLib.Components.BattleState.Commands.ItemCommand(id, item);
            }
        }
    }
}
