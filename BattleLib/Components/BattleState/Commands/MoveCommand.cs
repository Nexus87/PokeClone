using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState.Commands
{
    public class MoveCommand : ICommand
    {
        public Move move;
        public ClientIdentifier Source { get; private set; }

        public MoveCommand(ClientIdentifier source, Move move)
        {
            this.move = move;
            this.Source = source;
        }

        public CommandType Type
        {
            get { return CommandType.Move; }
        }

        public int Priority
        {
            get { return move.Data.Priority; }
        }

        public void Execute(CommandExecuter executer, BattleData data)
        {
            PokemonWrapper source = null;
            PokemonWrapper target = null;

            if (Source == data.player)
            {
                source = data.PlayerPkmn;
                target = data.AIPkmn;
            }
            else if (Source == data.ai)
            {
                source = data.AIPkmn;
                target = data.PlayerPkmn;
            }
            else
            {
                throw new InvalidOperationException("Source id is unknown.");
            }

            executer.ExecMove(source, move, target);
        }
    }
}
