using Base;
using Base.Data;
using Base.Rules;
using System;
using GameEngine.GUI.Utils;
using GameEngine.TypeRegistry;

namespace PokemonGame.Rules
{
    [GameService(typeof(IMoveEffectCalculator))]
    public class DefaultMoveEffectCalculator : IMoveEffectCalculator
    {
        private IBattlePokemon source;
        private IBattlePokemon target;
        private Move move;
        private Random random = new Random();
        private IBattleRules rules;

        public DefaultMoveEffectCalculator(IBattleRules rules)
        {
            rules.CheckNull("rules");
            this.rules = rules;
        }
        public void Init(IBattlePokemon source, Move move, IBattlePokemon target)
        {
            this.source = source;
            this.move = move;
            this.target = target;

            CalculateHit();
            CalculateCritical();
            CalculateTypeModifier();
            CalculateDamage();

            if (Damage > target.HP)
                StatusCondition = StatusCondition.KO;
            else
                StatusCondition = StatusCondition.Normal;
        }

        private void CalculateHit()
        {
            float hitChance = rules.GetHitChance(source, target, move);
            double result = random.NextDouble();

            IsHit = result < hitChance;
        }

        private void CalculateCritical()
        {
            float criticalChance = rules.GetCriticalHitChance(move);
            double result = random.NextDouble();

            IsCritical = result < criticalChance;
        }

        private void CalculateTypeModifier()
        {
            TypeModifier = rules.GetTypeModifier(source, target, move);
        }

        private void CalculateDamage()
        {
            float damage = rules.CalculateBaseDamage(source, target, move);

            damage *= rules.SameTypeAttackBonus(source, move);
            damage *= TypeModifier;
            damage *= IsCritical ? rules.GetCriticalHitModifier() : 1.0f;
            damage *= rules.GetMiscModifier(source, target, move);

            Damage = (int)damage;
        }

        public bool IsHit { get; private set; }
        public bool IsCritical { get; private set; }
        public float TypeModifier { get; private set; }
        public int Damage { get; private set; }
        public StatusCondition StatusCondition { get; private set; }
    }
}
