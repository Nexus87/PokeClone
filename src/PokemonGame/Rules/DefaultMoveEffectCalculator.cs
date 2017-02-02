using System;
using GameEngine.Globals;
using GameEngine.TypeRegistry;
using Pokemon.Services.Rules;
using Pokemon.Models;

namespace PokemonGame.Rules
{
    [GameService(typeof(IMoveEffectCalculator))]
    public class DefaultMoveEffectCalculator : IMoveEffectCalculator
    {
        private PokemonEntity _source;
        private PokemonEntity _target;
        private Move _move;
        private readonly Random _random = new Random();
        private readonly IBattleRules _rules;

        public DefaultMoveEffectCalculator(IBattleRules rules)
        {
            rules.CheckNull("rules");
            _rules = rules;
        }
        public void Init(PokemonEntity source, Move move, PokemonEntity target)
        {
            _source = source;
            _move = move;
            _target = target;

            CalculateHit();
            CalculateCritical();
            CalculateTypeModifier();
            CalculateDamage();

            StatusCondition = Damage > target.Hp ? StatusCondition.KO : StatusCondition.Normal;
        }

        private void CalculateHit()
        {
            var hitChance = _rules.GetHitChance(_source, _target, _move);
            var result = _random.NextDouble();

            IsHit = result < hitChance;
        }

        private void CalculateCritical()
        {
            var criticalChance = _rules.GetCriticalHitChance(_move);
            var result = _random.NextDouble();

            IsCritical = result < criticalChance;
        }

        private void CalculateTypeModifier()
        {
            TypeModifier = _rules.GetTypeModifier(_source, _target, _move);
        }

        private void CalculateDamage()
        {
            var damage = _rules.CalculateBaseDamage(_source, _target, _move);

            damage *= _rules.SameTypeAttackBonus(_source, _move);
            damage *= TypeModifier;
            damage *= IsCritical ? _rules.GetCriticalHitModifier() : 1.0f;
            damage *= _rules.GetMiscModifier(_source, _target, _move);

            Damage = (int)damage;
        }

        public bool IsHit { get; private set; }
        public bool IsCritical { get; private set; }
        public float TypeModifier { get; private set; }
        public int Damage { get; private set; }
        public StatusCondition StatusCondition { get; private set; }
    }
}
