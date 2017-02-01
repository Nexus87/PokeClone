namespace Base.Rules
{
    public interface IBattleRules
    {
        float CalculateBaseDamage(PokemonEntity source, PokemonEntity target, Move move);

        bool CanChange();

        bool CanEscape();

        float GetCriticalHitChance(Move move);

        float GetCriticalHitModifier();

        float GetHitChance(PokemonEntity source, PokemonEntity target, Move move);

        float GetMiscModifier(PokemonEntity source, PokemonEntity target, Move move);

        float GetStateModifier(int stage);

        float GetTypeModifier(PokemonEntity source, PokemonEntity target, Move move);

        float SameTypeAttackBonus(PokemonEntity source, Move move);
    }
}