using GameEngine.Core.ECS;
using PokemonShared.Models;

namespace BattleMode.Entities.Actions
{
    public class UseItemAction
    {
        public readonly Item Item;
        public readonly Entity Target;

        public UseItemAction(Item item, Entity target)
        {
            Item = item;
            Target = target;
        }
    }
}