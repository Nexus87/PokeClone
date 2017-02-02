using System;

namespace Pokemon.Services.Rules
{
    public class PokemonChangedEventArgs : EventArgs
    {
        public PokemonChangedEventArgs(PokemonEntity source)
        {
            Source = source;
        }

        public PokemonEntity Source { get; }
    }
}