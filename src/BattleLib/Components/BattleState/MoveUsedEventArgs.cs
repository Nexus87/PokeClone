﻿using System;
using Base;

namespace BattleMode.Core.Components.BattleState
{
    public class MoveUsedEventArgs : EventArgs
    {
        public Move Move { get; private set; }
        public PokemonWrapper Source { get; private set; }

        public MoveUsedEventArgs(Move move, PokemonWrapper source)
        {
            Move = move;
            Source = source;
        }
    }
}