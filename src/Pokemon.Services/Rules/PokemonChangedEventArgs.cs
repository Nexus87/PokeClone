using System;

namespace Base.Rules
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