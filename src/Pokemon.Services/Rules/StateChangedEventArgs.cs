using System;

namespace Base.Rules
{
    public class StateChangedEventArgs : EventArgs
    {
        public StateChangedEventArgs(BattleStats changedState, int modifier, PokemonEntity source)
        {
            ChangedState = changedState;
            Modifier = modifier;
            Source = source;
        }

        public PokemonEntity Source { get; }
        public BattleStats ChangedState { get; }
        public int Modifier { get; }
    }
}