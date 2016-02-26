using Base;
using BattleLib.Components.BattleState;
using System;

namespace BattleLib
{
    internal class Gen1BattleRules : IBattleRules
    {
        private bool isTrainerBattle;
        private TypeTable table = new TypeTable();

        public Gen1BattleRules(bool isTrainerBattle)
        {
            this.isTrainerBattle = isTrainerBattle;
        }

        public event EventHandler<ChangeUsedArgs> ChangeUsed = delegate { };
        public event EventHandler<ItemUsedArgs> ItemUsed = delegate { };
        public event EventHandler<MoveUsedArgs> MoveUsed = delegate { };

        public bool CanChange()
        {
            return true;
        }

        public bool CanEscape()
        {
            return !isTrainerBattle;
        }

        public void ExecMove(PokemonWrapper source, Move move, PokemonWrapper target)
        {
            switch (move.Data.DamageType)
            {
                case DamageCategory.Physical:
                    HandlePhysicalDamage(source, move, target);
                    break;

                case DamageCategory.Special:
                    HandleSpecialDamage(source, move, target);
                    break;

                case DamageCategory.Status:
                    HandleStatusDamage(source, move, target);
                    break;
            }
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

        private void HandlePhysicalDamage(PokemonWrapper source, Move move, PokemonWrapper target)
        {

            float damage = CalculateDamage(source.Atk, target.Def, source, move);
            float modifier = CalculateModifier(source, target, move);

            target.ModifyStat(State.HP, (int)(modifier * damage));
        }

        private void HandleSpecialDamage(PokemonWrapper source, Move move, PokemonWrapper target)
        {

            float damage = CalculateDamage(source.SpAtk, target.SpDef, source, move);
            float modifier = CalculateModifier(source, target, move);

            target.ModifyStat(State.HP, (int)(modifier * damage));
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