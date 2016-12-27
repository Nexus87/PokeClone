using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using BattleMode.Core.Components.BattleState;
using Microsoft.Xna.Framework;
using IGameComponent = GameEngine.Core.IGameComponent;

namespace BattleMode.Core.Components.AI
{
    internal class AIComponent : IGameComponent
    {
        private IBattleStateService state;
        private Action nextAction;
        private ClientIdentifier id;
        private ClientIdentifier player;
        private IReadOnlyList<Pokemon> pokemons;
        private Pokemon currentPokemon;

        public AIComponent(IBattleStateService state, Client ai, ClientIdentifier player)
        {
            this.state = state;
            id = ai.Id;
            this.player = player;
            pokemons = ai.Pokemons;
            state.StateChanged += StateChangedHandler;
        }

        private void StateChangedHandler(object sender, StateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case BattleStates.WaitForAction:
                    nextAction = ChooseMove;
                    break;
                case BattleStates.WaitForPokemon:
                    nextAction = ChoosePokemon;
                    break;
                default:
                    nextAction = null;
                    break;
            }
        }

        private void ChooseMove()
        {
            if (currentPokemon == null)
                throw new InvalidOperationException("No Pokemon is set");

            var move = (from m in currentPokemon.Moves where m.RemainingPP > 0 select m).FirstOrDefault();

            if (move == null)
                throw new NotImplementedException();

            state.SetMove(id, player, move);
        }

        private void ChoosePokemon()
        {
            var pkmn = (from p in pokemons where !p.IsKO() select p).FirstOrDefault();

            if (pkmn == null)
                throw new NotImplementedException();

            currentPokemon = pkmn;
            state.SetCharacter(id, pkmn);
        }

        public void Update(GameTime gameTime)
        {
            if (nextAction == null)
                return;

            nextAction();
            nextAction = null;
        }

        public void Initialize()
        {
        }
    }
}
