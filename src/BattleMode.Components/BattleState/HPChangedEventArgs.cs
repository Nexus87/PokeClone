using System;
using BattleMode.Shared;

namespace BattleMode.Components.BattleState
{
    public class HpChangedEventArgs : EventArgs
    {
        public ClientIdentifier Id { get; private set; }
        public int Hp { get; private set; }

        public HpChangedEventArgs(ClientIdentifier id, int hp)
        {
            Id = id;
            Hp = hp;
        }
    }
}