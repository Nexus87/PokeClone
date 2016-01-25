﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;
using BattleLib.Components.BattleState;

namespace BattleLib
{

    class Gen1BattleRules : IBattleRules
    {
        bool isTrainerBattle;
        TypeTable table = new TypeTable();

        public event EventHandler<OnConditionChangedArgs> OnConditionChanged;
        public event EventHandler<OnDamageTakenArgs> OnDamageTaken;
        public event EventHandler<OnStatsChangedArgs> OnStatsChanged;

        public Gen1BattleRules(bool isTrainerBattle)
        {
            this.isTrainerBattle = isTrainerBattle;
        }
        public bool CanChange()
        {
            return true;
        }

        public bool CanEscape()
        {
            return isTrainerBattle;
        }

        public bool TryChange(PokemonWrapper oldPkmn, Pokemon newPkmn)
        {
            return true;
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

        private float CalculateDamage(int attack, int defense, PokemonWrapper source, Move move)
        {
            float damage = (2.0f * source.Level + 10.0f) / 250.0f;
            damage *= ((float)attack) / ((float)defense);
            damage *= (float) move.Data.Damage;
            damage += 2;

            return damage;
        }
        private float CalculateModifier(PokemonWrapper source, PokemonWrapper target, Move move)
        {
            float modifier = move.Data.PkmType == source.Type1 || move.Data.PkmType == source.Type2 ?
                1.5f : 1.0f;
            modifier *= table.GetModifier(move.Data.PkmType, target.Type1);
            modifier *= table.GetModifier(move.Data.PkmType, target.Type2);
            // modifier *= critical
            // modifier *= random(0.85, 1.0)

            return modifier;
        }
        private void HandleSpecialDamage(PokemonWrapper source, Move move, PokemonWrapper target)
        {
            float damage = CalculateDamage(source.SpAtk, target.SpDef, source, move);
            float modifier = CalculateModifier(source, target, move);
        }

        private void HandleStatusDamage(PokemonWrapper source, Move move, PokemonWrapper target)
        {
            throw new NotImplementedException();
        }

        private void HandlePhysicalDamage(PokemonWrapper source, Move move, PokemonWrapper target)
        {
            float damage = CalculateDamage(source.Atk, target.Def, source, move);   
            float modifier = CalculateModifier(source, target, move);
        }

        public bool UseItem(PokemonWrapper target, Item item)
        {
            throw new NotImplementedException();
        }
    }
}
