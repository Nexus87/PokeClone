using Base;
using Base.Rules;
using BattleMode.Shared;

namespace BattleMode.Entities.BattleState
{
    public interface IEventCreator : IBattleEvents
    {
        void Critical();
        void Effective(MoveEfficiency effect, PokemonEntity target);
        void SetNewTurn();
        void UsingMove(PokemonEntity source, Move move);
    }
}
