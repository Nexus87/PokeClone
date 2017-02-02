using Pokemon.Services.Rules;
using PokemonShared.Models;

namespace BattleModeTest
{
    public class MoveEffectCalculatorStub : IMoveEffectCalculator
    {

        public void Init(PokemonEntity source, Move move, PokemonEntity target)
        {
        }

        public bool IsHit { get; set; }
        public bool IsCritical { get; set; }
        public float TypeModifier { get; set; }
        public int Damage { get; set; }

        public StatusCondition StatusCondition { get; set; }
    }
}