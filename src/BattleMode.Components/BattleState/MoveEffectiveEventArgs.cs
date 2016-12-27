using System;
using BattleMode.Shared;

namespace BattleMode.Components.BattleState
{
    public class MoveEffectiveEventArgs : EventArgs
    {
        public MoveEfficiency Effect { get; private set; }
        public PokemonWrapper Target { get; private set; }

        public MoveEffectiveEventArgs(MoveEfficiency effect, PokemonWrapper target)
        {
            Effect = effect;
            Target = target;
        }
    }
}