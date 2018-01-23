using PokemonShared.Models;

namespace BattleMode.Shared
{
    public interface IMoveEffectCalculator
    {
        void Init(Pokemon source, Move move, Pokemon target);

        bool IsHit { get; }
        bool IsCritical { get; }
        float TypeModifier { get; }
        int Damage { get; }
        StatusCondition StatusCondition { get; }
        
    }
}
