using BattleMode.Shared;
using GameEngine.Core.ECS;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState.Commands
{
    public class MoveCommand : ICommand
    {
        public Move Move { get; }
        public Entity Target { get; }
        public Entity Source { get; }

        public MoveCommand(Move move, Entity source, Entity target)
        {
            Move = move;
            Source = source;
            Target = target;
        }
    }
}