using System;
using GameEngine.Core;

namespace BattleMode.Core.Components.BattleState
{
    public interface IBattleStateService : IGameComponent
    {
        event EventHandler<StateChangedEventArgs> StateChanged;

        PokemonWrapper GetPokemon(ClientIdentifier id);

        void SetCharacter(ClientIdentifier id, Base.Pokemon pkmn);

        void SetItem(ClientIdentifier id, ClientIdentifier target, Base.Item item);

        void SetMove(ClientIdentifier id, ClientIdentifier target, Base.Move move);
    }
}