using Base;
using Base.Data;
using Base.Rules;
using BattleLib.Components.BattleState;
using System;

namespace BattleLib
{
    public enum MoveEfficiency
    {
        NoEffect,
        NotEffective,
        Normal,
        VeryEffective
    }

    public class CommandExecuter
    {
        private IBattleRules rules;
        private ITypeTable table;
        private Random rng = new Random();
        private EventCreator eventCreator;

        public CommandExecuter(IBattleRules rules, EventCreator eventCreator, ITypeTable table)
        {
            this.rules = rules;
            this.eventCreator = eventCreator;
            this.table = table;
        }

        public bool CanChange()
        {
            return rules.CanChange();
        }

        public bool CanEscape()
        {
            return rules.CanEscape();
        }

        public void ExecMove(PokemonWrapper source, Move move, PokemonWrapper target)
        {
            eventCreator.UsingMove(source, move);

            switch (move.DamageType)
            {
                case DamageCategory.Physical:
                case DamageCategory.Special:
                    HandleDamage(source, target, move);
                    break;
                case DamageCategory.Status:
                    throw new NotImplementedException();
            }
        }

        private void HandleDamage(PokemonWrapper source, PokemonWrapper target, Move move)
        {
            float damage = rules.CalculateBaseDamage(source, target, move);
            float typeModifier = rules.GetTypeModifier(source, target, move);

            MoveEfficiency effect = GetEffect(typeModifier);
            bool critical = rng.NextDouble().CompareTo(rules.GetCriticalHitChance(move)) < 0;

            damage *= rules.SameTypeAttackBonus(source, move);
            damage *= typeModifier;
            damage *= critical ? rules.GetCriticalHitModifier() : 1.0f;
            damage *= rules.GetMiscModifier(source, target, move);

            target.HP -= (int) damage;

            eventCreator.SetHP(target.Identifier, target.HP);
            eventCreator.Effective(effect, target);
            if (critical)
                eventCreator.Critical();

            if (target.Condition == StatusCondition.KO)
                eventCreator.SetStatus(StatusCondition.KO);
        }

        private static MoveEfficiency GetEffect(float typeModifier)
        {
            if (typeModifier.CompareTo(0) == 0)
                return MoveEfficiency.NoEffect;

            int result = typeModifier.CompareTo(1.0f);

            if (result > 0)
                return MoveEfficiency.VeryEffective;
            else if (result < 0)
                return MoveEfficiency.NotEffective;

            return MoveEfficiency.Normal;
        }

        public void ChangePokemon(PokemonWrapper oldPokemon, Pokemon newPokemon)
        {
            throw new NotImplementedException();
        }

        public bool UseItem(PokemonWrapper target, Item item)
        {
            throw new NotImplementedException();
        }

        private void HandleStatusDamage(PokemonWrapper source, Move move, PokemonWrapper target)
        {
            throw new NotImplementedException();
        }

    }
}