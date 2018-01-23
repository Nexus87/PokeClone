using System;
using BattleMode.Shared;
using GameEngine.Core.ECS;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState.Commands
{
    public class ChangeCommand : ICommand
    {
        public Pokemon Pokemon { get; }
        public Entity Target { get; }

        public ChangeCommand(Pokemon newPkmn, Entity target)
        {
            Pokemon = newPkmn;
            Target = target;
        }
    }
}
