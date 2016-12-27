using Base;
using Base.Data;
using BattleMode.Shared;

namespace BattleMode.Components.BattleState
{
    public interface IEventCreator : IBattleEvents
    {
        void Critical();
        void Effective(MoveEfficiency effect, PokemonWrapper target);
        void SetNewTurn();
        void SetHp(ClientIdentifier id, int hp);
        void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon);
        void SetStatus(PokemonWrapper pokemon, StatusCondition condition);
        void UsingMove(PokemonWrapper source, Move move);
        void SwitchPokemon(PokemonWrapper pokemon);
    }
}
