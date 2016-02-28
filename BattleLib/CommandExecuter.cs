using Base;
using BattleLib.Components.BattleState;
using System;

namespace BattleLib
{
    public enum ChangeFailedReasons
    {
        blocked
    }

    public enum MoveFailedReasons
    {
        noEffect,
        missed,

    }

    public enum MoveEfficency
    {
        notEffective,
        normal,
        veryEffective
    }

    public class ChangeUsedArgs : EventArgs
    {
        bool success;
        public PokemonWrapper newPokemon;
    }


    public class OnConditionChangedArgs : EventArgs
    {
        public StatusCondition condition;
        public StatusCondition oldCondition;
        public Pokemon pkmn;
    }

    public class ItemUsedArgs : EventArgs
    {
        public Item item;
        public bool success;
    }

    public class OnActionFailedArgs : EventArgs
    {
        public bool HasMissed;
        public bool HasResistance;
    }


    public class MoveUsedArgs : EventArgs
    {
        public Move move;
        public ClientIdentifier source;
    }


    public class HPReductionArgs : EventArgs
    {
        public ClientIdentifier target;
        public int oldHP;
        public int newHP;

        public bool success;
        public MoveFailedReasons resason;

        public bool critical;
        public MoveEfficency effective;

    }

    public class ConditionChangeArgs : EventArgs
    {
        public ClientIdentifier target;
        public bool success;
        public MoveFailedReasons resason;

        public StatusCondition oldCondition;
        public StatusCondition newCondition;
    }

    public class StateChangeArgs : EventArgs
    {
        public ClientIdentifier target;
        public bool success;
        public MoveFailedReasons resason;

        public State state;
        public int oldState;
        public int newState;
    }

    public class CommandExecuter
    {
        private IBattleRules rules;
        private TypeTable table = new TypeTable();
        private Random rng = new Random();

        public CommandExecuter(IBattleRules rules)
        {
            this.rules = rules;
        }

        public event EventHandler<ChangeUsedArgs> ChangeUsed = delegate { };
        public event EventHandler<ItemUsedArgs> ItemUsed = delegate { };
        public event EventHandler<MoveUsedArgs> MoveUsed = delegate { };

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
            switch (move.Data.DamageType)
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
            float stab = rules.SameTypeAttackBonus(source, move);
            float typeModifier = rules.GetTypeModifier(source, target, move);
            bool critical = rng.NextDouble().CompareTo(rules.GetCriticalHitChance(move)) < 0;
            float criticalModifier = critical ? rules.GetCriticalHitModifier() : 1.0f;

            float totalDamage = damage * stab * typeModifier * criticalModifier * rules.GetMiscModifier(source, target, move);

            target.HP -= (int) totalDamage;
        }

        public bool TryChange(PokemonWrapper oldPkmn, Pokemon newPkmn)
        {
            return true;
        }

        public bool UseItem(PokemonWrapper target, Item item)
        {
            throw new NotImplementedException();
        }

        private float CalculateDamage(int attack, int defense, PokemonWrapper source, Move move)
        {
            float damage = (2.0f * source.Level + 10.0f) / 250.0f;
            damage *= ((float)attack) / ((float)defense);
            damage *= (float)move.Data.Damage;
            damage += 2;

            return damage;
        }

        private float CalculateModifier(PokemonWrapper source, PokemonWrapper target, Move move)
        {
            float modifier = move.Data.PkmType == source.Type1 || move.Data.PkmType == source.Type2 ?
                1.5f : 1.0f;

            float typeModifier = table.GetModifier(move.Data.PkmType, target.Type1);
            typeModifier *= table.GetModifier(move.Data.PkmType, target.Type2);

            //float critical = critical modifier *= random(0.85, 1.0)

            return modifier * typeModifier;
        }


        private void HandleStatusDamage(PokemonWrapper source, Move move, PokemonWrapper target)
        {
            throw new NotImplementedException();
        }


        public event EventHandler<HPReductionArgs> HPReduction;
        public event EventHandler<ConditionChangeArgs> ConditionChange;
        public event EventHandler<StateChangeArgs> StateChange;
    }
}