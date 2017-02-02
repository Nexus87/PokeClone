using System;
using System.Collections.Generic;
using System.Linq;
using Pokemon.Data;
using Pokemon.Models;

namespace Pokemon.Services.Rules
{
    public class PokemonEntity
    {
        private readonly Dictionary<BattleStats, int> _modifiers = Enum.GetValues(typeof(BattleStats))
            .OfType<BattleStats>()
            .ToDictionary(x => x, x => 0);

        private readonly Dictionary<BattleStats, Func<int>> _stateGetter = new Dictionary<BattleStats, Func<int>>();

        private Models.Pokemon _pokemon;

        public PokemonEntity()
        {
            _stateGetter[BattleStats.Attack] = () => Pokemon?.Atk ?? 0;
            _stateGetter[BattleStats.Accuracy] = () => 100;
            _stateGetter[BattleStats.Defense] = () => Pokemon?.Def ?? 0;
            _stateGetter[BattleStats.Evasion] = () => 100;
            _stateGetter[BattleStats.SpecialAttacks] = () => Pokemon?.SpAtk ?? 0;
            _stateGetter[BattleStats.SpecialDefense] = () => Pokemon?.SpDef ?? 0;

        }

        public event EventHandler<StateChangedEventArgs> StateChanged;
        public event EventHandler<HpChangedEventArgs> HpChanged;
        public event EventHandler<StatusConditionEventArgs> StatusChanged;
        public event EventHandler<PokemonChangedEventArgs> PokemonChanged;

        public bool HasPokemon => Pokemon != null;
        public Models.Pokemon Pokemon
        {
            private get { return _pokemon; }
            set
            {
                if(_pokemon == value)
                    return;
                _pokemon = value;
                PokemonChanged?.Invoke(this, new PokemonChangedEventArgs(this));
            }
        }
        public StatusCondition Condition {
            get { return Pokemon?.Condition ?? StatusCondition.KO; }
            set {
                if (value == Condition)
                    return;
                Pokemon.Condition = value;
                OnStatusChanged(value);
            }
        }

        public int Accuracy
        {
            get { return GetState(BattleStats.Accuracy); }
            set{ SetState(BattleStats.Accuracy, Accuracy, value);}
        }

        public int Attack
        {
            get { return GetState(BattleStats.Attack); }
            set { SetState(BattleStats.Attack, Attack, value);}
        }

        public int Defense
        {
            get { return GetState(BattleStats.Defense); }
            set { SetState(BattleStats.Defense, Defense, value); }
        }

        public int Evasion
        {
            get { return GetState(BattleStats.Evasion); }
            set { throw new NotImplementedException(); }
        }

        public int Hp
        {
            get { return Pokemon?.Hp ?? 0; }
            set
            {
                if(value == Hp)
                    return;
                var oldValue = Hp;
                Pokemon.Hp = value;

                OnHpChanged(oldValue, value);
            }
        }

        public int Level => Pokemon?.Level ?? 0;
        public int MaxHp => Pokemon?.MaxHp ?? 0;
        public string Name => Pokemon?.Name;

        public int SpecialAttack
        {
            get { return GetState(BattleStats.SpecialAttacks); }
            set { SetState(BattleStats.SpecialAttacks, SpecialAttack, value); }
        }

        public int SpecialDefense
        {
            get { return GetState(BattleStats.SpecialAttacks); }
            set { SetState(BattleStats.SpecialDefense, SpecialDefense, value); }
        }

        public PokemonType Type1 => Pokemon?.Type1 ?? PokemonType.None;
        public PokemonType Type2 => Pokemon?.Type2 ?? PokemonType.None;
        public IEnumerable<Move> Moves => Pokemon?.Moves ?? Enumerable.Empty<Move>();
        public int Id => Pokemon.Id;
        public void ResetModifier()
        {
            foreach (var keyValuePair in _modifiers)
            {
                _modifiers[keyValuePair.Key] = 0;
            }
        }

        protected virtual void OnStateChanged(BattleStats state, int modifier)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(state, modifier, this));
        }

        protected virtual void OnHpChanged(int oldHp, int newHp)
        {
            HpChanged?.Invoke(this, new HpChangedEventArgs(this, oldHp, newHp));
        }

        protected virtual void OnStatusChanged(StatusCondition condition)
        {
            StatusChanged?.Invoke(this, new StatusConditionEventArgs(condition, this));
        }

        private int GetState(BattleStats state) => _stateGetter[state]() + _modifiers[state];

        private void SetState(BattleStats state, int oldValue, int newValue)
        {
            if (oldValue == newValue)
                return;

            newValue = Math.Max(0, newValue);

            _modifiers[state] = newValue - _stateGetter[state]();

            OnStateChanged(state, newValue - oldValue);
        }
    }
}