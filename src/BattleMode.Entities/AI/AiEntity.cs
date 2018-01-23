//using System;
//using System.Collections.Generic;
//using System.Linq;
//using BattleMode.Entities.BattleState;
//using BattleMode.Shared;
//using GameEngine.Entities;
//using Microsoft.Xna.Framework;
//using PokemonShared.Models;
//using StateChangedEventArgs = BattleMode.Entities.BattleState.StateChangedEventArgs;

//namespace BattleMode.Entities.AI
//{
//    public class AiEntity : IGameEntity
//    {
//        private readonly IBattleStateService _state;
//        private Action _nextAction;
//        private readonly ClientIdentifier _id;
//        private readonly ClientIdentifier _player;
//        private readonly IReadOnlyList<Pokemon> _pokemons;
//        private Pokemon _currentPokemon;

//        public AiEntity(IBattleStateService state, Client ai, ClientIdentifier player)
//        {
//            _state = state;
//            _id = ai.Id;
//            _player = player;
//            _pokemons = ai.Pokemons;
//            state.StateChanged += StateChangedHandler;
//            _nextAction = ChoosePokemon;
//        }

//        private void StateChangedHandler(object sender, StateChangedEventArgs e)
//        {
//            switch (e.NewState)
//            {
//                case BattleStates.WaitForAction:
//                    _nextAction = ChooseMove;
//                    break;
//                case BattleStates.WaitForPokemon:
//                    _nextAction = ChoosePokemon;
//                    break;
//                default:
//                    _nextAction = null;
//                    break;
//            }
//        }

//        private void ChooseMove()
//        {
//            if (_currentPokemon == null)
//                throw new InvalidOperationException("No Pokemon is set");

//            var move = (from m in _currentPokemon.Moves where m.RemainingPp > 0 select m).FirstOrDefault();

//            if (move == null)
//                throw new NotImplementedException();

//            _state.SetMove(_id, _player, move);
//        }

//        private void ChoosePokemon()
//        {
//            var pkmn = (from p in _pokemons where !p.IsKo() select p).FirstOrDefault();

//            if (pkmn == null)
//                throw new NotImplementedException();

//            _currentPokemon = pkmn;
//            _state.SetCharacter(_id, pkmn);
//        }

//        public void Update(GameTime gameTime)
//        {
//            if (_nextAction == null)
//                return;

//            _nextAction();
//            _nextAction = null;
//        }
//    }
//}
