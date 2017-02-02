using PokemonShared.Models;

namespace BattleMode.Shared
{
    public interface IMoveEffectCalculator
    {
        void Init(PokemonEntity source, Move move, PokemonEntity target);

        bool IsHit { get; }
        bool IsCritical { get; }
        float TypeModifier { get; }
        int Damage { get; }
        StatusCondition StatusCondition { get; }
        
    }
}
