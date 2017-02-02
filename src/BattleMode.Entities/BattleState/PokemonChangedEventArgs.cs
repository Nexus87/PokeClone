using System;

namespace BattleMode.Entities.BattleState
{
    public class PokemonChangedEventArgs : EventArgs
    {
        public PokemonChangedEventArgs(Pokemon.Models.Pokemon pokemon)
        {
            this.Pokemon = pokemon;
        }

        public Pokemon.Models.Pokemon Pokemon { get; private set; }

        public static implicit operator PokemonChangedEventArgs(Pokemon.Models.Pokemon pokemon)
        {
            return new PokemonChangedEventArgs(pokemon);
        }
    }
}