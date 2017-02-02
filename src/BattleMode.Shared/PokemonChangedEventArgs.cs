using System;

namespace BattleMode.Shared
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