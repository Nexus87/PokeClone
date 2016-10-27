using Base.Data;

namespace Base.Rules
{
    public interface IMoveEffectCalculator
    {
        void Init(IBattlePokemon source, Move move, IBattlePokemon target);

        bool IsHit { get; }
        bool IsCritical { get; }
        float TypeModifier { get; }
        int Damage { get; }
        StatusCondition StatusCondition { get; }
        
    }
}
