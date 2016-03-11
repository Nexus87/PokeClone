using Base;
using Base.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Rules
{
    public class DummyBattleRules : IBattleRules
    {
        public float CalculateBaseDamage(IBattlePokemon source, IBattlePokemon target, Move move)
        {
            return 1.0f;
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

        public float GetHitChance(IBattlePokemon source, IBattlePokemon target, Move move)
        {
            return 0.95f;
        }

        public float GetMiscModifier(IBattlePokemon source, IBattlePokemon target, Move move)
        {
            return 1.0f;
        }

        public float GetStateModifier(int stage)
        {
            return 1.0f;
        }

        public float GetTypeModifier(IBattlePokemon source, IBattlePokemon target, Move move)
        {
            return 1.0f;
        }

        public float SameTypeAttackBonus(IBattlePokemon source, Move move)
        {
            return 1.0f;
        }
    }
}
