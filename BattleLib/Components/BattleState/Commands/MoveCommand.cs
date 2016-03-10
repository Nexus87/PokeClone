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
            get { return move.Priority; }
        }

        public void Execute(CommandExecuter executer, BattleData data)
        {
            PokemonWrapper source = null;
            PokemonWrapper target = null;

            if (Source == data.Player)
            {
                source = data.PlayerPokemon;
                target = data.AiPokemon;
            }
            else if (Source == data.Ai)
            {
                source = data.AiPokemon;
                target = data.PlayerPokemon;
            }
            else
            {
                throw new InvalidOperationException("Source id is unknown.");
            }

            executer.ExecMove(source, move, target);
        }
    }
}
