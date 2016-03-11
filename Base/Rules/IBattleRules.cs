namespace Base.Rules
{
    public interface IBattleRules
    {
        float CalculateBaseDamage(IBattlePokemon source, IBattlePokemon target, Move move);

        bool CanChange();

        bool CanEscape();

        float GetCriticalHitChance(Move move);

        float GetCriticalHitModifier();

        float GetHitChance(IBattlePokemon source, IBattlePokemon target, Move move);

        float GetMiscModifier(IBattlePokemon source, IBattlePokemon target, Move move);

        float GetStateModifier(int stage);

        float GetTypeModifier(IBattlePokemon source, IBattlePokemon target, Move move);

        float SameTypeAttackBonus(IBattlePokemon source, Move move);
    }
}