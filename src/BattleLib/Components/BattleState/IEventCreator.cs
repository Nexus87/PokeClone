using Base.Data;

namespace BattleMode.Core.Components.BattleState
{
    public interface IEventCreator : IBattleEvents
    {
        void Critical();
        void Effective(MoveEfficiency effect, PokemonWrapper target);
        void SetNewTurn();
        void SetHP(ClientIdentifier id, int hp);
        void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon);
        void SetStatus(PokemonWrapper pokemon, StatusCondition condition);
        void UsingMove(PokemonWrapper source, Base.Move move);
        void SwitchPokemon(PokemonWrapper pokemon);
    }
}
