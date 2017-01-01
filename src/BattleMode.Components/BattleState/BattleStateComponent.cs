﻿using System;
using Base;
using BattleMode.Shared;
using GameEngine.Globals;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Components.BattleState
{
    [GameService(typeof(IBattleStateService))]
    public class BattleStateComponent : IBattleStateService
    {
        private IBattleState _currentState;

        private readonly BattleData _data;

        private readonly IEventCreator _eventCreator;

        public BattleStateComponent(BattleData data, WaitForActionState actionState, WaitForCharState characterSetState, ExecuteState executionState, IEventCreator eventCreator) :
            this(data, (IBattleState)actionState, characterSetState, executionState, eventCreator) { }
        
        internal BattleStateComponent(BattleData data, IBattleState actionState, IBattleState characterSetState, IBattleState executionState, IEventCreator eventCreator)
        {
            actionState.CheckNull(nameof(actionState));
            characterSetState.CheckNull(nameof(characterSetState));
            executionState.CheckNull(nameof(executionState));
            eventCreator.CheckNull(nameof(eventCreator));

            ActionState = actionState;
            CharacterSetState = characterSetState;
            ExecutionState = executionState;
            _eventCreator = eventCreator;
            _data = data;
            Initialize();

        }

        public event EventHandler<StateChangedEventArgs> StateChanged = delegate { };

        private IBattleState ActionState { get; }
        private IBattleState CharacterSetState { get; }
        private IBattleState ExecutionState { get; }

        private IBattleState CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
                StateChanged(this, new StateChangedEventArgs(_currentState.State));

                if (_currentState.State == BattleStates.WaitForAction)
                    _eventCreator.SetNewTurn();
            }
        }

        public PokemonWrapper GetPokemon(ClientIdentifier id)
        {
            return _data.GetPokemon(id);
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
            CurrentState.Update(_data);
        }

        public void Initialize()
        {
            CurrentState = CharacterSetState;
            CurrentState.Init(_data);
        }

        private void SetNextState()
        {
            if (!CurrentState.IsDone)
                return;

            var nextState = GetNextState(CurrentState);
            nextState.Init(_data);
            while (nextState.IsDone)
            {
                nextState = GetNextState(nextState);
                nextState.Init(_data);
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