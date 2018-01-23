using GameEngine.Core.ECS;
using PokemonShared.Models;

namespace BattleMode.Entities.Actions
{
    public class UseMoveAction
    {
        public readonly Move Move;
        public readonly Entity Source;
        public readonly Entity Target;

        public UseMoveAction(Move move, Entity source, Entity target)
        {
            Move = move;
            Source = source;
            Target = target;
        }
    }
}