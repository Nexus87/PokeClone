using System;
using Base.Data;
using BattleMode.Shared;

namespace BattleMode.Entities.BattleState
{
    public class ClientStatusChangedEventArgs : EventArgs
    {
        public StatusCondition Status { get; private set; }
        public ClientIdentifier Id { get; private set; }

        public ClientStatusChangedEventArgs(ClientIdentifier id, StatusCondition status)
        {
            Status = status;
            Id = id;
        }
    }
}