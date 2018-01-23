using PokemonShared.Models;

namespace BattleMode.Shared
{
    public interface IBattleRules
    {
        float CalculateBaseDamage(Pokemon source, Pokemon target, Move move);

        bool CanChange();

        bool CanEscape();

        float GetCriticalHitChance(Move move);

        float GetCriticalHitModifier();

        float GetHitChance(Pokemon source, Pokemon target, Move move);

        float GetMiscModifier(Pokemon source, Pokemon target, Move move);

        float GetStateModifier(int stage);

        float GetTypeModifier(Pokemon source, Pokemon target, Move move);

        float SameTypeAttackBonus(Pokemon source, Move move);
    }
}