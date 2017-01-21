using System;
using Base;
using Base.Rules;
using BattleMode.Shared;
using GameEngine.Entities;

namespace BattleMode.Entities.BattleState
{
    public interface IBattleStateService : IGameEntity
    {
        event EventHandler<StateChangedEventArgs> StateChanged;

        PokemonEntity GetPokemon(ClientIdentifier id);

        void SetCharacter(ClientIdentifier id, Pokemon pkmn);

        void SetItem(ClientIdentifier id, ClientIdentifier target, Item item);

        void SetMove(ClientIdentifier id, ClientIdentifier target, Move move);
    }
}