using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace BattleLib
{
    class Gen1BattleRules : IBattleRules
    {
        bool isTrainerBattle;

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

        public bool ExecChange(Pokemon oldPkmn, Pokemon newPkmn)
        {
            return true;
        }

        public bool ExecMove(Pokemon source, Move move, Pokemon target)
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

            return true;
        }

        private float CalculateDamage(int attack, int defense, Pokemon source, Move move)
        {
            float damage = (2.0f * source.Level + 10.0f) / 250.0f;
            damage *= ((float)attack) / ((float)defense);
            damage *= (float) move.Data.Damage;
            damage += 2;

            float modifier = move.Data.PkmType == source.Type1 || move.Data.PkmType == source.Type2 ?
                1.5f : 1.0f;
            // modifier *= type effectiveness
            // modifier *= critical
            // modifier *= random(0.85, 1.0)

            return damage * modifier;
        }
        private void HandleSpecialDamage(Pokemon source, Move move, Pokemon target)
        {
            throw new NotImplementedException();
        }

        private void HandleStatusDamage(Pokemon source, Move move, Pokemon target)
        {
            throw new NotImplementedException();
        }

        private void HandlePhysicalDamage(Pokemon source, Move move, Pokemon target)
        {
            throw new NotImplementedException();
        }

        public bool UseItem(Pokemon target, Item item)
        {
            throw new NotImplementedException();
        }
    }
}
