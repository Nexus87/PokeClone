using System;
using Pokemon.Services.Rules;
using Pokemon.Models;

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