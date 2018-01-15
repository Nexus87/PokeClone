using BattleMode.Shared;
using PokemonShared.Models;

namespace PokemonGame.Rules
{
    public class DummyBattleRules : IBattleRules
    {
        public float CalculateBaseDamage(PokemonEntity source, PokemonEntity target, Move move)
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

        public float GetHitChance(PokemonEntity source, PokemonEntity target, Move move)
        {
            return 0.95f;
        }

        public float GetMiscModifier(PokemonEntity source, PokemonEntity target, Move move)
        {
            return 1.0f;
        }

        public float GetStateModifier(int stage)
        {
            return 1.0f;
        }

        public float GetTypeModifier(PokemonEntity source, PokemonEntity target, Move move)
        {
            return 1.0f;
        }

        public float SameTypeAttackBonus(PokemonEntity source, Move move)
        {
            return 1.0f;
        }
    }
}
