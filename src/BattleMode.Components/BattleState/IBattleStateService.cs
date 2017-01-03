using System;
using Base;
using BattleMode.Shared;

namespace BattleMode.Components.BattleState
{
    public interface IBattleStateService : GameEngine.Components.IGameComponent
    {
        event EventHandler<StateChangedEventArgs> StateChanged;

        PokemonWrapper GetPokemon(ClientIdentifier id);

        void SetCharacter(ClientIdentifier id, Pokemon pkmn);

        void SetItem(ClientIdentifier id, ClientIdentifier target, Item item);

        void SetMove(ClientIdentifier id, ClientIdentifier target, Move move);
    }
}