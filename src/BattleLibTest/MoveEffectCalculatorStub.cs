using Base;
using Base.Data;
using Base.Rules;

namespace BattleModeTest
{
    public class MoveEffectCalculatorStub : IMoveEffectCalculator
    {

        public void Init(IBattlePokemon source, Move move, IBattlePokemon target)
        {
        }

        public bool IsHit { get; set; }
        public bool IsCritical { get; set; }
        public float TypeModifier { get; set; }
        public int Damage { get; set; }

        public StatusCondition StatusCondition { get; set; }
    }
}