using System;
using BattleMode.Shared;
using Pokemon.Services.Rules;

namespace BattleMode.Entities.BattleState
{
    public class MoveEffectiveEventArgs : EventArgs
    {
        public MoveEfficiency Effect { get; }
        public PokemonEntity Target { get; }

        public MoveEffectiveEventArgs(MoveEfficiency effect, PokemonEntity target)
        {
            Effect = effect;
            Target = target;
        }
    }
}