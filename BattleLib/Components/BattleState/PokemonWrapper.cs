using Base;
using System;

namespace BattleLib.Components.BattleState
{
    public class ConditionChangedEventArgs : EventArgs
    {
        public ConditionChangedEventArgs(StatusCondition newCondition)
        {
            NewCondition = newCondition;
        }

        public StatusCondition NewCondition { get; private set; }
    }

    public class PokemonWrapper
    {
        private Pokemon pokemon;

        private Stats stateModifier = new Stats();

        public event EventHandler<ConditionChangedEventArgs> OnConditionChanged = delegate { };
        public event EventHandler OnPokemonChanged = delegate { };

        public event EventHandler<StateChangedEventArgs> OnStateChanged = delegate { };

        public int Atk { get { return Pokemon.Stats.Atk + stateModifier.Atk; } }

        public StatusCondition Condition
        {
            get { return Pokemon.Condition; }
            set
            {
                Pokemon.Condition = value;
                OnConditionChanged(this, new ConditionChangedEventArgs(value));
            }
        }

        public int Def { get { return Pokemon.Stats.Def + stateModifier.Def; } }

        public int HP { get { return Pokemon.HP; } }

        public int ID { get { return Pokemon.Id; } }

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
                pokemon = value;
                OnPokemonChanged(this, null);
            }
        }
        public PokemonType Type1 { get { return Pokemon.Type1; } }
        public PokemonType Type2 { get { return Pokemon.Type2; } }
        public Stats PokemonStates { get { return Pokemon.Stats; } }
        public int SpAtk { get { return Pokemon.Stats.SpAtk + stateModifier.SpAtk; } }
        public int SpDef { get { return Pokemon.Stats.SpDef + stateModifier.SpDef; } }

        public void ModifyStat(State state, int modifier)
        {
            switch (state)
            {
                case State.HP:
                    stateModifier.HP += modifier;
                    break;

                case State.Atk:
                    stateModifier.Atk += modifier;
                    break;

                case State.Def:
                    stateModifier.Def += modifier;
                    break;

                case State.SpAtk:
                    stateModifier.SpAtk += modifier;
                    break;

                case State.SpDef:
                    stateModifier.SpDef += modifier;
                    break;

                case State.Speed:
                    stateModifier.Speed += modifier;
                    break;
            }

            OnStateChanged(this, new StateChangedEventArgs(state, modifier));
        }
    }

    public class StateChangedEventArgs : EventArgs
    {
        public StateChangedEventArgs(State modifiedState, int modifier)
        {
            this.ModifiedState = modifiedState;
            this.Modifier = modifier;
        }

        public State ModifiedState { get; private set; }
        public int Modifier { get; private set; }
    }
}