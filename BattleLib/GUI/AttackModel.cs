﻿using Base;
using BattleLib.Components.BattleState;
using GameEngine.Graphics.Views;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents.GUI
{
    internal class AttackModel : DefaultListModel<Move>
    {
        public AttackModel(PokemonWrapper pokemon) : base (4)
        {
            pokemon.PokemonChanged += PokemonChangedHandler;
        }

        private void PokemonChangedHandler(object sender, PokemonChangedEventArgs e)
        {
            for (int i = 0; i < e.Pokemon.Moves.Count; i++)
                SetData(e.Pokemon.Moves[i], i, 0);
        }

        public override int Rows { get { return 4; } }

        public override bool SetData(Move data, int row, int column)
        {
            // No resize allowed
            if (row >= 4)
                throw new ArgumentException("Index out of bound");

            return base.SetData(data, row, column);
        }

        public override string DataStringAt(int row, int column)
        {
            var data = DataAt(row, 0);
            return data != null ? data.ToString() : "----";
        }
    }
}