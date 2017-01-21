﻿using System;
using Base;

namespace BattleMode.Entities.BattleState
{
    public class PokemonChangedEventArgs : EventArgs
    {
        public PokemonChangedEventArgs(Pokemon pokemon)
        {
            this.Pokemon = pokemon;
        }

        public Pokemon Pokemon { get; private set; }

        public static implicit operator PokemonChangedEventArgs(Pokemon pokemon)
        {
            return new PokemonChangedEventArgs(pokemon);
        }
    }
}