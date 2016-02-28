using Base;
using BattleLib.Components.BattleState;
using System;
using System.Collections.Generic;

namespace BattleLib
{
    public interface IBattleRules
    {
        bool CanChange();
        float CalculateBaseDamage(PokemonWrapper source, PokemonWrapper target, Move move);

        float GetCriticalHitModifier();
        float GetCriticalHitChance(Move move);

        float GetHitChance(PokemonWrapper source, PokemonWrapper target, Move move);

        float GetTypeModifier(PokemonWrapper source, PokemonWrapper target, Move move);

        float GetMiscModifier(PokemonWrapper source, PokemonWrapper target, Move move);

        bool CanEscape();

        float GetStateModifier(int stage);
        float SameTypeAttackBonus(PokemonWrapper source, Move move);
    }

}
