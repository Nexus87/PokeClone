using PokemonShared.Models;

namespace Pokemon.Services.Rules
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
