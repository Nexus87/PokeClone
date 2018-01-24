using System;
using System.Collections.Generic;
using System.Linq;
using BattleMode.Entities.Actions;
using BattleMode.Entities.BattleState;
using BattleMode.Entities.BattleState.Commands;
using BattleMode.Shared;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS;
using Microsoft.Xna.Framework;
using PokemonShared.Models;

namespace BattleMode.Entities.AI
{
    public class AiSystem
    {
        private readonly Entity _aiEntity;
        private readonly Entity _player;
        private readonly IMessageBus _messageBus;

        public AiSystem(Entity aiEntity, Entity player, IMessageBus messageBus)
        {
            _aiEntity = aiEntity;
            _player = player;
            _messageBus = messageBus;
        }

        public void StartNewTurn(StartNewTurnAction action, IEntityManager entityManager)
        {
            var pokemon = entityManager.GetComponentByTypeAndEntity<PokemonComponent>(_aiEntity).First().Pokemon;
            var move = ChooseMove(pokemon);
            _messageBus.SendAction(new SetCommandAction(new MoveCommand(move, _aiEntity, _player), _aiEntity));
        }

        private Move ChooseMove(Pokemon pokemon)
        {
            if (pokemon == null)
                throw new InvalidOperationException("No Pokemon is set");

            var move = (from m in pokemon.Moves where m.RemainingPp > 0 select m).FirstOrDefault();

            if (move == null)
                throw new NotImplementedException();

            return move;
        }
    }
}
