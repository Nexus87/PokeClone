using System;

namespace BattleLib.Components.BattleState
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