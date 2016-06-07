using Base;
using Base.Rules;
using GameEngine;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace BattleLib.Components.BattleState
{
    public enum BattleStates 
    {
        WaitForPokemon,
        WaitForAction,
        Execute
    }

    public class BattleStateComponent : GameEngine.IGameComponent
    {
        public event EventHandler<StateChangedEventArgs> StateChanged = delegate { };

        public IBattleState ActionState { get; private set; }
        public IBattleState CharacterSetState { get; private set; }
        public IBattleState ExecutionState { get; private set; }

        private IBattleState currentState;

        private IBattleState CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
                StateChanged(this, new StateChangedEventArgs(currentState.State));

                if (currentState.State == BattleStates.WaitForAction)
                    eventCreator.SetNewTurn();
            }
        }

        private BattleData data;
        private IEventCreator eventCreator;

        public PokemonWrapper GetPokemon(ClientIdentifier id)
        {
            return data.GetPokemon(id);
        }

        
        public BattleStateComponent(ClientIdentifier player, ClientIdentifier ai, IBattleState actionState, IBattleState characterSetState, IBattleState executionState, IEventCreator eventCreator)
        {
            actionState.CheckNull("actionState");
            characterSetState.CheckNull("characterSetState");
            executionState.CheckNull("executionState");
            eventCreator.CheckNull("eventCreator");

            ActionState = actionState;
            CharacterSetState = characterSetState;
            ExecutionState = executionState;
            this.eventCreator = eventCreator;
            data = new BattleData(player, ai);

            ActionState.BattleState = this;
            CharacterSetState.BattleState = this;
            ExecutionState.BattleState = this;
        }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            CurrentState.SetCharacter(id, pkmn);
        }

        public void SetItem(ClientIdentifier id, ClientIdentifier target, Item item)
        {
            CurrentState.SetItem(id, target, item);
        }

        public void SetMove(ClientIdentifier id, ClientIdentifier target, Move move)
        {
            CurrentState.SetMove(id, target, move);
        }

        public void Update(GameTime gameTime)
        {
            SetNextState();
            CurrentState.Update(data);
        }

        private void SetNextState()
        {
            if (!CurrentState.IsDone)
                return;

            var nextState = GetNextState(CurrentState);
            nextState.Init(data);
            while (nextState.IsDone)
            {
                nextState = GetNextState(nextState);
                nextState.Init(data);
            }

            CurrentState = nextState;
        }


        IBattleState GetNextState(IBattleState current)
        {
            if (current == ActionState)
                return ExecutionState;
            else if (current == CharacterSetState)
                return ActionState;
            else if (current == ExecutionState)
                return CharacterSetState;
            
            throw new InvalidOperationException("Current state is unkown");
        }

        public void Initialize()
        {
            CurrentState = CharacterSetState;
        }
    }

    public class StateChangedEventArgs : EventArgs
    {
        public BattleStates NewState { get; private set; }
        public StateChangedEventArgs(BattleStates newState)
        {
            NewState = newState;
        }
    }

}