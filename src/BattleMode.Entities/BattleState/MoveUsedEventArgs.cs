using System;
using Base;
using Base.Rules;

namespace BattleMode.Entities.BattleState
{
    public class MoveUsedEventArgs : EventArgs
    {
        public Move Move { get;}
        public PokemonEntity Source { get; }

        public MoveUsedEventArgs(Move move, PokemonEntity source)
        {
            Move = move;
            Source = source;
        }
    }
}