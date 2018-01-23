using PokemonShared.Models;

namespace BattleMode.Shared
{
    public class DummyBattleRules : IBattleRules
    {
        public float CalculateBaseDamage(Pokemon source, Pokemon target, Move move)
        {
            return 50.0f;
        }

        public bool CanChange()
        {
            return false;
        }

        public bool CanEscape()
        {
            return false;
        }

        public float GetCriticalHitChance(Move move)
        {
            return 0.15f;
        }

        public float GetCriticalHitModifier()
        {
            return 2.0f;
        }

        public float GetHitChance(Pokemon source, Pokemon target, Move move)
        {
            return 0.95f;
        }

        public float GetMiscModifier(Pokemon source, Pokemon target, Move move)
        {
            return 1.0f;
        }

        public float GetStateModifier(int stage)
        {
            return 1.0f;
        }

        public float GetTypeModifier(Pokemon source, Pokemon target, Move move)
        {
            return 1.0f;
        }

        public float SameTypeAttackBonus(Pokemon source, Move move)
        {
            return 1.0f;
        }
    }
}
