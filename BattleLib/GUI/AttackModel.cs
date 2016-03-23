using Base;
using BattleLib.Components.BattleState;
using GameEngine.Graphics.Views;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents.GUI
{
    internal class AttackModel : DefaultTableModel<Move>
    {
        public AttackModel(PokemonWrapper pokemon) : base(4, 1)
        {
            pokemon.PokemonChanged += PokemonChangedHandler;
        }

        private void PokemonChangedHandler(object sender, PokemonChangedEventArgs e)
        {
            for (int i = 0; i < e.Pokemon.Moves.Count; i++)
                SetData(e.Pokemon.Moves[i], i, 0);
        }


        public override int Rows { get { return 4; } }
        public override int Columns { get { return 1; } }
    }
}