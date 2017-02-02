using System;
using PokemonShared.Models;

namespace BattleMode.Shared
{
    public class ItemUsedEventArgs : EventArgs
    {
        public int Index { get; private set; }
        public Item Item { get; private set; }

        public ItemUsedEventArgs(int index, Item item)
        {
            Index = index;
            Item = item;
        }
    }
}