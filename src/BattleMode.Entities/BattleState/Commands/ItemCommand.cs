using System;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState.Commands
{
    public class ItemCommand : ICommand
    {
        public Item Item { get; private set; }
        public ItemCommand(Item item)
        {
            Item = item;
        }

    }
}
