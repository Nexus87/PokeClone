﻿using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using BattleMode.Components.BattleState;
using BattleMode.Shared;
using Microsoft.Xna.Framework;
using IGameComponent = GameEngine.Core.IGameComponent;

namespace BattleMode.Components.AI
{
    public class AiComponent : IGameComponent
    {
        private readonly IBattleStateService _state;
        private Action _nextAction;
        private readonly ClientIdentifier _id;
        private readonly ClientIdentifier _player;
        private readonly IReadOnlyList<Pokemon> _pokemons;
        private Pokemon _currentPokemon;

        public AiComponent(IBattleStateService state, Client ai, ClientIdentifier player)
        {
            _state = state;
            _id = ai.Id;
            _player = player;
            _pokemons = ai.Pokemons;
            state.StateChanged += StateChangedHandler;
        }

        private void StateChangedHandler(object sender, StateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case BattleStates.WaitForAction:
                    _nextAction = ChooseMove;
                    break;
                case BattleStates.WaitForPokemon:
                    _nextAction = ChoosePokemon;
                    break;
                default:
                    _nextAction = null;
                    break;
            }
        }

        private void ChooseMove()
        {
            if (_currentPokemon == null)
                throw new InvalidOperationException("No Pokemon is set");

            var move = (from m in _currentPokemon.Moves where m.RemainingPP > 0 select m).FirstOrDefault();

            if (move == null)
                throw new NotImplementedException();

            _state.SetMove(_id, _player, move);
        }

        private void ChoosePokemon()
        {
            var pkmn = (from p in _pokemons where !p.IsKO() select p).FirstOrDefault();

            if (pkmn == null)
                throw new NotImplementedException();

            _currentPokemon = pkmn;
            _state.SetCharacter(_id, pkmn);
        }

        public void Update(GameTime gameTime)
        {
            if (_nextAction == null)
                return;

            _nextAction();
            _nextAction = null;
        }

        public void Initialize()
        {
        }
    }
}
