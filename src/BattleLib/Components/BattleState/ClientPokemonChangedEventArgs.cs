using System;

namespace BattleLib.Components.BattleState
{
    public class ClientPokemonChangedEventArgs : EventArgs
    {
        public PokemonWrapper Pokemon { get; private set; }
        public ClientIdentifier ID { get; private set; }

        public ClientPokemonChangedEventArgs(ClientIdentifier id, PokemonWrapper pokemon)
        {
            ID = id;
            Pokemon = pokemon;
        }
    }
}