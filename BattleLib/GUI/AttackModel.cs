using Base;
using BattleLib.Components.BattleState;
using BattleLib.GUI;
using GameEngine.Graphics.Views;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents.GUI
{
    internal class AttackModel : SingleDimensionTableModel<Move>
    {
        public AttackModel(PokemonWrapper pokemon)
        {
            items = new List<Move>{null, null, null, null};
            
            pokemon.PokemonChanged += PokemonChangedHandler;
            if (pokemon.Pokemon != null && pokemon.Pokemon.Moves != null)
                SetMoves(pokemon.Pokemon.Moves);
        }

        private void SetMoves(IReadOnlyList<Move> moves)
        {
            // The override SetData methods throws an exception.
            // Therefore we call the base method.
            int i = 0;
            for (; i < moves.Count; i++)
                base.SetData(moves[i], i, 0);
            for (; i < Rows; i++)
                base.SetData(null, i, 0);
        }

        private void PokemonChangedHandler(object sender, PokemonChangedEventArgs e)
        {
            SetMoves(e.Pokemon.Moves);
        }

        public override bool SetData(Move data, int row, int column)
        {
            throw new InvalidOperationException("Model is read only");
        }
    }
}