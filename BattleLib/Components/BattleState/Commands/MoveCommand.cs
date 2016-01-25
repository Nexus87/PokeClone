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
        Move move;
        ClientIdentifier id;

        public MoveCommand(ClientIdentifier id, Move move)
        {
            this.move = move;
            this.id = id;
        }

        public CommandType Type
        {
            get { return CommandType.Move; }
        }

        public int Priority
        {
            get { return move.Data.Priority; }
        }

        public void Execute(IBattleRules rules, BattleData data)
        {
            PokemonWrapper source = null;
            PokemonWrapper target = null;

            if(id == data.player)
            {
                source = data.PlayerPkmn;
                target = move.Data.TargetMode == Target.Enemy ? data.AIPkmn : source;
            }
            else if (id == data.ai)
            {
                source = data.AIPkmn;
                target = move.Data.TargetMode == Target.Enemy ? data.PlayerPkmn : source;
            }
            else
            {
                throw new InvalidOperationException("Source id is unknown.");
            }

            rules.ExecMove(source, move, target);
        }
    }
}
