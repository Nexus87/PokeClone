using Base;
using GameEngine.Graphics.Views;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents.GUI
{
    internal class AttackModel : DefaultListModel<Move>
    {
        public override List<Move> Items
        {
            set
            {
                if (value.Count > 4)
                    throw new InvalidOperationException("Only 4 moves are possible.");

                base.Items = value;
            }
        }

        public override int Rows { get { return 4; } }

        public override Move DataAt(int row, int column)
        {
            if (row >= items.Count)
                return null;
            return items[row];
        }

        public override string DataStringAt(int row, int column)
        {
            return row < items.Count ? items[row].ToString() : "----";
        }
    }
}