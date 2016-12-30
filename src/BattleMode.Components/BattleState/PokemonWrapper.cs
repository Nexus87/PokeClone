using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Base;
using Base.Data;
using Base.Rules;
using BattleMode.Shared;

namespace BattleMode.Components.BattleState
{
    public class PokemonWrapper : IBattlePokemon
    {
        private readonly Dictionary<ModifyableState, float> _modifier = new Dictionary<ModifyableState, float>();
        private Pokemon _pokemon;

        public PokemonWrapper(ClientIdentifier id)
        {
            Identifier = id;
            ResetModifier();
            Moves = new ObservableCollection<Move>(Enumerable.Range(0, 4).Select(x => (Move) null).ToList());
        }

        public event EventHandler<PokemonChangedEventArgs> PokemonChanged = delegate { };
        public event EventHandler<PokemonChangedEventArgs> PokemonValuesChanged = delegate { };

        public float Accuracy => _modifier[ModifyableState.Accuracy];
        public int Atk => (int)(Pokemon.Atk * _modifier[ModifyableState.Atk]);

        public ObservableCollection<Move> Moves;
        public StatusCondition Condition
        {
            get { return Pokemon.Condition; }
            set { Pokemon.Condition = value; }
        }

        public int Def => (int)(Pokemon.Def * _modifier[ModifyableState.Def]);
        public float Evasion => _modifier[ModifyableState.Evasion];

        public int HP
        {
            get { return Pokemon.HP; }
            set
            {
                var newHp = Math.Max(0, value);
                if (newHp == Pokemon.HP)
                    return;
                Pokemon.HP = newHp;
                PokemonValuesChanged(this, new PokemonChangedEventArgs(Pokemon));
            }
        }

        public int ID => Pokemon.Id;
        public ClientIdentifier Identifier { get; private set; }
        public int Level => Pokemon.Level;
        public int MaxHP => Pokemon.MaxHP;
        public string Name => Pokemon.Name;

        public Pokemon Pokemon
        {
            get { return _pokemon; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "null is not a valid value");
                if (value == _pokemon)
                    return;

                ResetModifier();
                _pokemon = value;
                for (var i = 0; i < Moves.Count; i++)
                {
                    Moves[i] = i < _pokemon.Moves.Count ? _pokemon.Moves[i] : null;
                }
                PokemonChanged(this, value);
            }
        }

        public int SpAtk => (int)(Pokemon.SpAtk * _modifier[ModifyableState.SpAtk]);
        public int SpDef => (int)(Pokemon.SpDef * _modifier[ModifyableState.SpDef]);

        public PokemonType Type1 => Pokemon.Type1;
        public PokemonType Type2 => Pokemon.Type2;

        public void SetModifierStage(ModifyableState state, int stage, IBattleRules rules)
        {
            _modifier[state] = rules.GetStateModifier(stage);
        }

        private void ResetModifier()
        {
            var list = (IEnumerable<ModifyableState>) Enum.GetValues(typeof(ModifyableState));
            foreach (var s in list)
                _modifier[s] = 1.0f;
        }
    }
}