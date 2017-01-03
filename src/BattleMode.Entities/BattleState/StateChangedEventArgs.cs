using System;

namespace BattleMode.Entities.BattleState
{
    public class StateChangedEventArgs : EventArgs
    {
        public StateChangedEventArgs(BattleStates newState)
        {
            NewState = newState;
        }

        public BattleStates NewState { get; private set; }
    }
}