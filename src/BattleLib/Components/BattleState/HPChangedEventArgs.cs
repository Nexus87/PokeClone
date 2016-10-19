using System;

namespace BattleLib.Components.BattleState
{
    public class HPChangedEventArgs : EventArgs
    {
        public ClientIdentifier ID { get; private set; }
        public int HP { get; private set; }

        public HPChangedEventArgs(ClientIdentifier id, int hp)
        {
            ID = id;
            HP = hp;
        }
    }
}