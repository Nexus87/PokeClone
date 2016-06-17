using System;

namespace BattleLib.Components.BattleState
{
    public interface IBattleStateService
    {
        event EventHandler<StateChangedEventArgs> StateChanged;

        PokemonWrapper GetPokemon(BattleLib.ClientIdentifier id);

        void SetCharacter(BattleLib.ClientIdentifier id, Base.Pokemon pkmn);

        void SetItem(BattleLib.ClientIdentifier id, BattleLib.ClientIdentifier target, Base.Item item);

        void SetMove(BattleLib.ClientIdentifier id, BattleLib.ClientIdentifier target, Base.Move move);
    }
}