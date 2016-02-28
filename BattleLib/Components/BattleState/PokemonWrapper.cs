using Base;
using System;
using System.Collections.Generic;

namespace BattleLib.Components.BattleState
{
    public enum ModifyableState
    {
        Atk,
        Def,
        SpAtk,
        SpDef,
        Speed,
        Accuracy,
        Evasion
    }

    public class PokemonWrapper
    {
        private Dictionary<ModifyableState, float> modifier = new Dictionary<ModifyableState, float>();

        private Pokemon pokemon;

        public PokemonWrapper(ClientIdentifier id)
        {
            Identifier = id;
            ResetModifier();
        }

        private void ResetModifier()
        {
            var list = (IEnumerable<ModifyableState>)Enum.GetValues(typeof(ModifyableState));
            foreach (var s in list)
                modifier[s] = 1.0f;
        }

        public float Accuracy { get { return modifier[ModifyableState.Accuracy]; } }
        public int Atk { get { return (int)(Pokemon.Stats.Atk * modifier[ModifyableState.Atk]); } }

        public StatusCondition Condition
        {
            get { return Pokemon.Condition; }
            set { Pokemon.Condition = value; }
        }

        public int Def { get { return (int)(Pokemon.Stats.Def * modifier[ModifyableState.Def]); } }

        public float Evasion { get { return modifier[ModifyableState.Evasion]; } }
        public int HP {
            get { return Pokemon.HP; }
            set { Pokemon.HP = Math.Max(0, value);
                if (Pokemon.HP == 0)
                    Pokemon.Condition = StatusCondition.KO;
            }
        }

        public int ID { get { return Pokemon.Id; } }
        public ClientIdentifier Identifier { get; private set; }
        public int Level { get { return Pokemon.Level; } }
        public int MaxHP { get { return Pokemon.Stats.HP; } }
        public string Name { get { return Pokemon.Name; } }

        public Pokemon Pokemon
        {
            private get { return pokemon; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("null is not a valid value");
                ResetModifier();
                pokemon = value;
            }
        }

        public int SpAtk { get { return (int)(Pokemon.Stats.SpAtk * modifier[ModifyableState.SpAtk]); } }
        public int SpDef { get { return (int)(Pokemon.Stats.SpDef * modifier[ModifyableState.SpDef]); } }
        public PokemonType Type1 { get { return Pokemon.Type1; } }
        public PokemonType Type2 { get { return Pokemon.Type2; } }

        public void SetModifierStage(ModifyableState state, int stage, IBattleRules rules)
        {
            modifier[state] = rules.GetStateModifier(stage);
        }
    }
}