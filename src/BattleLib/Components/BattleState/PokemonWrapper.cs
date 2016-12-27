using System;
using System.Collections.Generic;
using Base;
using Base.Data;
using Base.Rules;

namespace BattleMode.Core.Components.BattleState
{
    public class PokemonWrapper : IBattlePokemon
    {
        private readonly Dictionary<ModifyableState, float> modifier = new Dictionary<ModifyableState, float>();
        private Pokemon pokemon;

        public PokemonWrapper(ClientIdentifier id)
        {
            Identifier = id;
            ResetModifier();
        }

        public event EventHandler<PokemonChangedEventArgs> PokemonChanged = delegate { };
        public event EventHandler<PokemonChangedEventArgs> PokemonValuesChanged = delegate { };

        public float Accuracy { get { return modifier[ModifyableState.Accuracy]; } }
        public int Atk { get { return (int)(Pokemon.Atk * modifier[ModifyableState.Atk]); } }

        public StatusCondition Condition
        {
            get { return Pokemon.Condition; }
            set { Pokemon.Condition = value; }
        }

        public int Def { get { return (int)(Pokemon.Def * modifier[ModifyableState.Def]); } }
        public float Evasion { get { return modifier[ModifyableState.Evasion]; } }

        public int HP
        {
            get { return Pokemon.HP; }
            set
            {
                var newHP = Math.Max(0, value);
                if (newHP == Pokemon.HP)
                    return;
                Pokemon.HP = newHP;
                PokemonValuesChanged(this, new PokemonChangedEventArgs(Pokemon));
            }
        }

        public int ID { get { return Pokemon.Id; } }
        public ClientIdentifier Identifier { get; private set; }
        public int Level { get { return Pokemon.Level; } }
        public int MaxHP { get { return Pokemon.MaxHP; } }
        public string Name { get { return Pokemon.Name; } }

        public Pokemon Pokemon
        {
            get { return pokemon; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value", "null is not a valid value");
                if (value == pokemon)
                    return;

                ResetModifier();
                pokemon = value;

                PokemonChanged(this, value);
            }
        }

        public int SpAtk { get { return (int)(Pokemon.SpAtk * modifier[ModifyableState.SpAtk]); } }
        public int SpDef { get { return (int)(Pokemon.SpDef * modifier[ModifyableState.SpDef]); } }

        public PokemonType Type1 { get { return Pokemon.Type1; } }
        public PokemonType Type2 { get { return Pokemon.Type2; } }

        public void SetModifierStage(ModifyableState state, int stage, IBattleRules rules)
        {
            modifier[state] = rules.GetStateModifier(stage);
        }

        private void ResetModifier()
        {
            var list = (IEnumerable<ModifyableState>)Enum.GetValues(typeof(ModifyableState));
            foreach (var s in list)
                modifier[s] = 1.0f;
        }
    }
}