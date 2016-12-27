using System;
using Base;

namespace BattleMode.Core
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