using System;
using Base.Data;

namespace BattleMode.Core.Components.BattleState
{
    public class ClientStatusChangedEventArgs : EventArgs
    {
        public StatusCondition Status { get; private set; }
        public ClientIdentifier ID { get; private set; }

        public ClientStatusChangedEventArgs(ClientIdentifier id, StatusCondition status)
        {
            Status = status;
            ID = id;
        }
    }
}