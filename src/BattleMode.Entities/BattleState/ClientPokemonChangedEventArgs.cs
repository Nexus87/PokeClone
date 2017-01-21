using System;
using Base.Rules;
using BattleMode.Shared;

namespace BattleMode.Entities.BattleState
{
    public class ClientPokemonChangedEventArgs : EventArgs
    {
        public PokemonEntity Pokemon { get; }
        public ClientIdentifier Id { get; }

        public ClientPokemonChangedEventArgs(ClientIdentifier id, PokemonEntity pokemon)
        {
            Id = id;
            Pokemon = pokemon;
        }
    }
}