using System;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState
{
    public class PokemonChangedEventArgs : EventArgs
    {
        public PokemonChangedEventArgs(Pokemon pokemon)
        {
            Pokemon = pokemon;
        }

        public Pokemon Pokemon { get; private set; }

        public static implicit operator PokemonChangedEventArgs(Pokemon pokemon)
        {
            return new PokemonChangedEventArgs(pokemon);
        }
    }
}