using System;

namespace BattleMode.Entities.BattleState
{
    public class PokemonChangedEventArgs : EventArgs
    {
        public PokemonChangedEventArgs(PokemonShared.Models.Pokemon pokemon)
        {
            Pokemon = pokemon;
        }

        public PokemonShared.Models.Pokemon Pokemon { get; private set; }

        public static implicit operator PokemonChangedEventArgs(PokemonShared.Models.Pokemon pokemon)
        {
            return new PokemonChangedEventArgs(pokemon);
        }
    }
}