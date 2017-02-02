using System;
using BattleMode.Shared;
using GameEngine.Entities;
using Pokemon.Services.Rules;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState
{
    public interface IBattleStateService : IGameEntity
    {
        event EventHandler<StateChangedEventArgs> StateChanged;

        PokemonEntity GetPokemon(ClientIdentifier id);

        void SetCharacter(ClientIdentifier id, PokemonShared.Models.Pokemon pkmn);

        void SetItem(ClientIdentifier id, ClientIdentifier target, Item item);

        void SetMove(ClientIdentifier id, ClientIdentifier target, Move move);
    }
}