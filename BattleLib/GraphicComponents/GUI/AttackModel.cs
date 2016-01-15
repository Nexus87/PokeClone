using Base;
using GameEngine.Graphics.Views;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents.GUI
{
    internal class AttackModel : DefaultListModel<Move>
    {
        public AttackModel() : base (4)
        {
        }
        public override int Rows { get { return 4; } }

        public override bool SetData(Move data, int row, int column)
        {
            // No resize allowed
            if (row >= 4)
                throw new ArgumentException("Index out of bound");

            return base.SetData(data, row, column);
        }

        public override Move DataAt(int row, int column)
        {
            if (row >= items.Count)
                return null;
            return items[row];
        }

        public override string DataStringAt(int row, int column)
        {
            return items[row] != null ? items[row].ToString() : "----";
        }
    }
}