using BattleMode.Shared;
using GameEngine.Core.ECS;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState.Commands
{
    public class ItemCommand : ICommand
    {
        public Item Item { get; }
        public Entity Target { get; }

        public ItemCommand(Item item, Entity target)
        {
            Item = item;
            Target = target;
        }

    }
}
