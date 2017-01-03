using System;
using BattleMode.Shared;

namespace BattleMode.Entities.BattleState
{
    public class ClientPokemonChangedEventArgs : EventArgs
    {
        public PokemonWrapper Pokemon { get; private set; }
        public ClientIdentifier Id { get; private set; }

        public ClientPokemonChangedEventArgs(ClientIdentifier id, PokemonWrapper pokemon)
        {
            Id = id;
            Pokemon = pokemon;
        }
    }
}