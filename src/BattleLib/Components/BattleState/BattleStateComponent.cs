﻿using Base;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace BattleLib.Components.BattleState
{
    [GameService(typeof(IBattleStateService))]
    public class BattleStateComponent : GameEngine.IGameComponent, IBattleStateService
    {
        private IBattleState currentState;

        private BattleData data;

        private IEventCreator eventCreator;

        public BattleStateComponent(BattleData data, WaitForActionState actionState, WaitForCharState characterSetState, ExecuteState executionState, IEventCreator eventCreator) :
            this(data, (IBattleState)actionState, characterSetState, executionState, eventCreator) { }
        
        internal BattleStateComponent(BattleData data, IBattleState actionState, IBattleState characterSetState, IBattleState executionState, IEventCreator eventCreator)
        {
            actionState.CheckNull("actionState");
            characterSetState.CheckNull("characterSetState");
            executionState.CheckNull("executionState");
            eventCreator.CheckNull("eventCreator");

            ActionState = actionState;
            CharacterSetState = characterSetState;
            ExecutionState = executionState;
            this.eventCreator = eventCreator;
            this.data = data;
        }

        public event EventHandler<StateChangedEventArgs> StateChanged = delegate { };

        private IBattleState ActionState { get; set; }
        private IBattleState CharacterSetState { get; set; }
        private IBattleState ExecutionState { get; set; }

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

        public PokemonWrapper GetPokemon(ClientIdentifier id)
        {
            return data.GetPokemon(id);
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

        public void Initialize()
        {
            CurrentState = CharacterSetState;
            CurrentState.Init(data);
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

        private IBattleState GetNextState(IBattleState current)
        {
            if (current == ActionState)
                return ExecutionState;
            else if (current == CharacterSetState)
                return ActionState;
            else if (current == ExecutionState)
                return CharacterSetState;

            throw new InvalidOperationException("Current state is unkown");
        }
    }
}